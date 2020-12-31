import React, {useContext, useEffect, useState} from 'react'
import {generateRandomString} from "../utils";
import {uploadFile} from "./FileUploader";
import {LinearProgress} from "@material-ui/core";
import {SnackbarContext} from "./AppSnackBar";
import {addLesson, removeLesson} from "../api";

export interface LocalProps {
    rawVideo: File,
    courseId: string,
    updateLessons: () => void,
}

const LocalVideoUploaderElement = ({rawVideo, courseId, updateLessons}: LocalProps) => {
    const {setData: setSnackbar} = useContext(SnackbarContext);
    const [loading, setLoading] = useState(false);
    const [loadingProgress, setLoadingProgress] = useState(0);
    const [location, setLocation] = useState("");
    const [lessonId, setLessonId] = useState <null | string> (null)

    const handleRemove = async () => {
        await removeLesson(lessonId!);
        await updateLessons();
    }

    const uploadVideo = async () => {
        setLoading(true)
        const name = generateRandomString();
        let blob = rawVideo!.slice(0, rawVideo!.size, 'video/*');
        let extension = rawVideo!.name.split(".").slice(-1)[0];
        let randomString = generateRandomString();
        let renamedFile = new File([blob], `${randomString}.${extension}`, {type: 'video/' + extension});
        let location = await uploadFile(renamedFile, (progress) => {
            setLoadingProgress(progress);
        })
        let {id} = await addLesson(courseId, rawVideo!.name.split(".")[0], location);
        setLessonId(id);
        setLocation(location);
        setSnackbar({message: "Video uploaded successfully!", type: "success"});
        setLoading(false);
        updateLessons();
    }

    useEffect(() => {
        uploadVideo();
    }, [])

    return (
        <section className = "video-element">
            <div className="video">
                <p className="percentage">
                    {loadingProgress.toFixed(1)}%
                </p>
                <p className="status">
                    {loading && "Uploading ..."}
                </p>
                <LinearProgress variant="determinate" className="progress" value={loadingProgress}/>
            </div> 
            <div className = "info">
                <p className= "title">
                    {rawVideo.name.split(".")[0]}
                </p>
                <p className="subtitle">
                    Uploading: {rawVideo.name}
                </p>
            </div>
        </section>
    );
}

export interface RemoteProps {
    title: string,
    videoSrc: string,
    id: string,
    courseId: string,
    updateLessons: () => void,
}

const RemoteVideoUploaderElement = ({courseId, updateLessons, title, videoSrc, id}: RemoteProps) => {
    const {setData: setSnackbar} = useContext(SnackbarContext);

    const handleRemove = async () => {
        await removeLesson(id);
        await updateLessons();
    }

    return (
        <section className = "video-element">
            <div className="preview">
                <video src = {videoSrc} />
            </div>
            <div className = "info">
                <p className= "title">
                    {title.split(".")[0]}
                </p>
                <button className="remove" onClick={handleRemove}>Remove Video</button>
            </div>
        </section>
    );
}

export {LocalVideoUploaderElement, RemoteVideoUploaderElement}