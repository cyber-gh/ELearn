import React, {useRef, useState, useEffect} from 'react'
import GrayBackground from "../components/GrayBackground";
import {LocalVideoUploaderElement, RemoteVideoUploaderElement} from "../components/VideoUploaderElement";
import {getLessons} from "../api";

export interface Props {
    [key: string]: any
}

const AddLessonsView = (props: Props) => {
    const id = props.match.params.id;
    const inputRef = useRef <HTMLInputElement | null>  (null);
    const [lessons, setLessons] = useState <any[]> ([]);
    
    const getData = async () => {
        let lessons = await getLessons(id);
        lessons = lessons.map (x => ({...x, type: "remote"}))
        setLessons(lessons);
    }
    
    const handleUpload = () => {
        inputRef.current?.click();
    }
    
    const handleChange = () => {
        let input = inputRef.current as HTMLInputElement;
        if (input.files?.length && input.files?.length > 0){
            let file = input.files[0];
            setLessons([...lessons, {type: "local", file}]);
        }
    }
    
    useEffect(() => {
        getData()
    }, [])
    
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
                        {lessons.length === 0 ?
                            <p className="empty">
                                There are no videos here, yet.
                            </p>
                            :
                            lessons.map((x, i) => x.type === "local" ? 
                                <LocalVideoUploaderElement key = {x.file.name} updateLessons = {getData} rawVideo={x.file} courseId={id}/> :
                                <RemoteVideoUploaderElement key = {x.id} courseId={id} updateLessons={getData} {...x}/>
                            )
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