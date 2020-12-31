import React, {useContext, useEffect, useState} from 'react'
import {generateRandomString} from "../utils";
import {uploadFile} from "./FileUploader";
import {LinearProgress} from "@material-ui/core";
import {SnackbarContext} from "./AppSnackBar";

export interface Props {
    rawVideo: File
}

const VideoUploaderElement = ({rawVideo}: Props) => {
    const {setData: setSnackbar} = useContext(SnackbarContext);
    const [loading, setLoading] = useState(false);
    const [loadingProgress, setLoadingProgress] = useState(0);
    const [location, setLocation] = useState("");
    
    const uploadVideo = async () => {
        setLoading(true);
        const name = generateRandomString();
        let blob = rawVideo.slice(0, rawVideo.size, 'video/*');
        let extension = rawVideo.name.split(".").slice(-1)[0];
        let randomString = generateRandomString();
        let renamedFile = new File([blob], `${randomString}.${extension}`, {type: 'video/' + extension});
        let location = await uploadFile(renamedFile, (progress) => {
            setLoadingProgress(progress);
        })
        setLocation(location);
        setSnackbar({message: "Video uploaded successfully!", type: "success"});
        setLoading(false);
    }
    
    useEffect(() => {
        uploadVideo();    
    }, [])
    
    return (
        <section className = "video-element">
            {loading ?
                <div className="video">
                    <p className="percentage">
                        {loadingProgress.toFixed(1)}%
                    </p>
                    <p className="status">
                        {loading && "Uploading ..."}
                    </p>
                    <LinearProgress variant="determinate" className="progress" value={loadingProgress}/>
                </div> :
                <div className="preview">
                    <video src = {location} />
                </div>
            }
            <div className = "info">
                <p className= "title">
                    {rawVideo.name.split(".")[0]}
                </p>
                {loading ?
                    <p className="subtitle">
                        Uploading: {rawVideo.name}
                    </p> :
                    <button className="remove" onClick={() => console.log("nob")}>Remove Video</button>
                }
            </div>
        </section>
    );
}

export default VideoUploaderElement;