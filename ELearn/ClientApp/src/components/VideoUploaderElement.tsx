import React, {useContext, useEffect, useState} from 'react'
import {generateRandomString, withFallback} from "../utils";
import {uploadFile} from "../FileUploader";
import {LinearProgress} from "@material-ui/core";
import {SnackbarContext} from "./AppSnackBar";
import {addLesson, removeLesson, updateLesson} from "../api";
import {CheckBox} from "@material-ui/icons";
import { useRef } from 'react';

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
    const videoRef = useRef <HTMLVideoElement | null> (null);

    const handleRemove = async () => {
        await removeLesson(lessonId!);
        await updateLessons();
    }

    const uploadVideo = async () => {
        setLoading(true);
        const name = generateRandomString();
        let blob = rawVideo!.slice(0, rawVideo!.size, 'video/*');
        let extension = rawVideo!.name.split(".").slice(-1)[0];
        let randomString = generateRandomString();
        let renamedFile = new File([blob], `${randomString}.${extension}`, {type: 'video/' + extension});
        let location = await uploadFile(renamedFile, (progress) => {
            setLoadingProgress(progress);
        })
        let {id} = await addLesson(courseId, rawVideo!.name.split(".")[0], location, Math.floor(videoRef.current!.duration));
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
        <>
            <video ref = {videoRef} src={URL.createObjectURL(rawVideo)} style = {{display: "none"}}/>
            <section className = "video-element loading">
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
        </>
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
    const [value, setValue] = useState(title);
    const [tempValue, setTempValue] = useState(value);
    const [inputState, setInputState] = useState(false);
    
    const handleClick = async () => {
        if (inputState) {
            await withFallback(setSnackbar, async () => {
                await updateLesson(id, tempValue);
                setValue(tempValue);
                setSnackbar({message: "Lesson title updated successfully", type: "success"});
            });
        }
        setInputState(!inputState);
    }

    const handleRemove = async () => {
        await withFallback(setSnackbar, async () => {
            await removeLesson(id);
            await updateLessons();
            setSnackbar({message: "Video Removed Successfully", type: "success"});
        });
    }

    return (
        <section className = "video-element">
            <div className="preview">
                <video src = {videoSrc} />
            </div>
            <div className = "info">
                {!inputState ? 
                    <p onClick={handleClick} className="title">
                        {value.split(".")[0]}
                    </p> :
                    <div className="info-input">
                        <input value = {tempValue} onChange={e => {
                            setTempValue(e.target.value)
                        }} />
                        <CheckBox className="checkbox" onClick={handleClick}/>
                    </div>
                }
                <button className={"remove"} onClick={handleRemove}>Remove Video</button>
            </div>
        </section>
    );
}

export {LocalVideoUploaderElement, RemoteVideoUploaderElement}