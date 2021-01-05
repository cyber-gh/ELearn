import React, {useEffect, useState} from 'react'
import { getCourseById } from '../api';
import LevelIcon from '../components/LevelIcon';
import {CourseDetailsModel, CourseModel, LessonModel, ReviewModel} from "../interfaces";
import { reviews } from '../sampleData';

export interface Props {
    match: {
        params: {
            id: string;
        }
    }
}

const About = ({title, description}) => (
    <div className="about">
        <p className="title">{title}</p>
        <p className="description">{description}</p>
    </div>
)

const Reviews = (courseId) => {
    const [data, setData] = useState <ReviewModel[]> (reviews);

    // const getData = async () => {
        
    // }

    // useEffect(() => {
    //     getData();
    // }, [])
    return (
        <div className="reviews">
            {data.map(x => (
                <div className="element">
                    <p className="username">From: {x.username}</p>
                    <div className = "info">
                        <div className = "top">
                            <p className="title">{x.title}</p>
                            <div className="recommend">
                                <LevelIcon level = {x.recommend}/>
                                <p>
                                    I recommend it for <b>{x.recommend} Levels.</b>
                                </p>
                            </div>
                        </div>
                        <p className="description">{x.description}</p>
                        <p className="time-added">{x.timeAdded}</p>
                    </div>
                </div>
            ))}
        </div>
    )
}

const CourseView = ({match: {params: {id}}}: Props) => {
    const [data, setData] = useState <CourseDetailsModel | null> (null);
    const [currentLesson, setCurrentLesson] = useState (0);
    const [currentTab, setCurrentTab] = useState(0);

    const changeCurrentTab = (next) => (e) => {
        setCurrentTab(next);
    }

    const changeCurrentLesson = (next) => (e) => {
        setCurrentLesson(next);
    }

    const getData = async () => {
        let course = await getCourseById(id);
        console.log(course);
        setData(course);
    }

    useEffect(() => {
        getData();
    }, [])

    if (!data) {
        return <div>loading lmao lol</div>
    }

    return (
        <section className="course">
            <div className="container">
                <div className = "main">
                    <p className="title">{data.overview.title}</p>
                    <p className = "author">Chris Brown</p>
                    <div className="content">
                        <div className="media">
                            {data.lessons.length > 0 && <video controls src = {data.lessons[currentLesson].videoSrc}/>}
                        </div>
                        <div className="info">
                            <p className = "text">
                                {data.lessons.length} Lessons
                            </p>
                            {data.lessons.map((x, index) => (
                                <div onClick = {changeCurrentLesson(index)} className={`lesson ${currentLesson === index ? "active": ""}`}>
                                    <p className="title">
                                        {index+1}. {x.title}
                                    </p>
                                    <p className="duration">
                                        {Math.floor(x.duration / 60)}:{x.duration % 60}
                                    </p>
                                </div>
                            ))}
                        </div>
                    </div>
                </div>
            </div>
            <div className="more">
                <div className="nav">
                    <div className="content">
                        {["About", "Reviews", "Comments", "Quiz"].map((x, index) => (
                            <p key = {index} onClick = {changeCurrentTab(index)} className = {index === currentTab ? "active" : ""}>
                                {x}
                            </p>
                        ))}
                    </div>
                </div>
                {currentTab === 0 && <About title= {data.overview.title} description = {data.overview.description}/>}
                {currentTab === 1 && <Reviews courseId={id}/>}
            </div>
        </section>
    );
}

 export default CourseView;