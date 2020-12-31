import React, {useRef, useState} from 'react'
import GrayBackground from "../components/GrayBackground";
import {CircularProgress, LinearProgress} from "@material-ui/core";
import VideoUploaderElement from "../components/VideoUploaderElement";
import {uploadFile} from "../components/FileUploader";
import {cacheImages, generateRandomString} from "../utils";

export interface Props {
    [key: string]: any
}

// const file = input.files[0];
// let blob = file.slice(0, file.size, 'image/*');
// let extension = file.name.split(".").slice(-1)[0];
// let randomString = generateRandomString();
// let renamedFile = new File([blob], `${randomString}.${extension}`, {type: 'video/' + extension});

const AddLessonsView = (props: Props) => {
    const inputRef = useRef <HTMLInputElement | null>  (null);
    const [videos, setVideos] = useState <any[]> ([]);
    
    const handleUpload = () => {
        inputRef.current?.click();
    }
    
    const handleChange = () => {
        let input = inputRef.current as HTMLInputElement;
        if (input.files?.length && input.files?.length > 0){
            let file = input.files[0];
            setVideos([...videos, file]);
        }
    }
    
    return (
        <>
            <GrayBackground/>
            <section className="add-lessons">
                <p className="title">Video Lessons</p>
                <p className="description">
                    Skillshare classes average 20-60 minutes total running time, divided into short video lessons of 2-5 minutes each.
                    To publish your class, the combined length of all your videos must total at least 10 minutes. <br/> <br/>
                    Teachers may upload a maximum of 1 class per week.
                </p>
                <hr/>
                <div className = "uploader">
                    <div className = "videos">
                        {videos.length === 0 ?
                            <p className="empty">
                                There are no videos here, yet.
                            </p>
                            :
                            videos.map(x => <VideoUploaderElement key = {x} rawVideo={x}/>)
                        }
                    </div>
                    <hr/>
                    <div>
                        <button type="button" className="upload" onClick={handleUpload}>Upload Video</button>
                    </div>
                </div>
                <input multiple={false} onChange={handleChange} type = "file" ref = {inputRef}  accept = "video/*"/>
            </section>
        </>
    );
}

export default AddLessonsView;