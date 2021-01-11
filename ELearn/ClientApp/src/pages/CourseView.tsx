import React, {useEffect, useState} from 'react'
import {getCourseById, getReviews} from '../api';
import LevelIcon from '../components/LevelIcon';
import {CourseDetailsModel, CourseModel, LessonModel, QuizModel, ReviewModel} from "../interfaces";
import Avatar from "../components/Avarat";
import AddReview from "../components/AddReview";
import {FormControlLabel, Radio, RadioGroup} from "@material-ui/core";

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

interface ReviewProps {
    reviews: ReviewModel[],
    courseId: string,
    getData: () => void
}

interface QuizProps {
    quiz: QuizModel
}

const Quiz = ({quiz}: QuizProps) => {
    const [answers, setAnswers] = useState({});
    const [show, setShow] = useState(false);
    
    const handleChange = (id) => (e) => {
        setAnswers({...answers, [id]: e.target.value})
    }
    
    const submit = () => {
        setShow(true);
    }
    
    const retry = () => {
        setAnswers({});
        setShow(false);
    }
    
    const right = quiz.elements.filter(x => answers[x.id] === x.correctAnswer).length;
    
    return (
        <div className="quiz">
            {quiz.elements.map(x => (
                !show ? 
                    <div key = {x.id} className="question">
                        <p>{x.question}</p>
                        <RadioGroup value = {answers[x.id] ? answers[x.id] : ""} onChange={handleChange(x.id)} name = {x.id}>
                            {x.answers.map((answer, index) => (
                                <div key = {index} className = "answer">
                                    <FormControlLabel value = {answer} control={<Radio color = "primary"/>} label={""}/>
                                    <p>{answer}</p>
                                </div>
                            ))}
                        </RadioGroup>
                    </div> :
                    <div key = {x.id} className="question">
                        <p>{x.question} ({answers[x.id] === x.correctAnswer ? "correct" : "wrong"})</p>
                        <RadioGroup name = {x.id}>
                            {x.answers.map((answer, index) => (
                                <div key = {index} className = {`answer ${(answer === answers[x.id] && answers[x.id] === x.correctAnswer) ? "correct" : ""} ${(answer === answers[x.id] && answers[x.id] !== x.correctAnswer) ? "wrong" : ""}`}>
                                    <FormControlLabel value = {answer} control={<Radio color = "primary"/>} label={""}/>
                                    <p>{answer}</p>
                                </div>
                            ))}
                        </RadioGroup>
                    </div>
            ))}
            {!show ?
                <button className="submit" onClick={submit}>Submit Answers</button> :
                <>
                    <p>Result: You got {right} out {quiz.elements.length} right!</p>
                    <button className="submit" onClick = {retry}>Retry quiz</button>
                </>
            }
        </div>
    )
}

const Reviews = ({reviews, courseId, getData}: ReviewProps) => {
    return (
        <div className="reviews">
            <AddReview getData={getData} id = {courseId}/>
            {reviews.map((x,index) => (
                <div key={index} className="element">
                    <div className="user-data">
                        <Avatar letter={x.user!.fullName[0]}/>
                        <p className="username">From: {x.user!.fullName}</p>
                    </div>
                    <div className = "info">
                        <div className = "top">
                            <p className="title">{x.title}</p>
                            <div className="recommend">
                                <LevelIcon level = {x.recommendFor}/>
                                <p>
                                    I recommend it for <b>{x.recommendFor} Levels.</b>
                                </p>
                            </div>
                        </div>
                        <p className="description">{x.comment}</p>
                        <p className="time-added">{new Date(x.createdDate).toDateString()}</p>
                    </div>
                </div>
            ))}
            {reviews.length === 0 && <p className = "no-review">No reviews for this course </p>}
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
    }, [id])

    if (!data) {
        return <div/>
    }
    
    const tabs = ["About", "Reviews"];
    if (data.lessons[currentLesson].quiz) tabs.push("Quiz");

    return (
        <section className="course">
            <div className="container">
                <div className = "main">
                    <p className="title">{data.overview.title}</p>
                    <p className = "author">{data.overview.appUser.fullName}</p>
                    <div className="content">
                        <div className="media">
                            {data.lessons.length > 0 && <video controls src = {data.lessons[currentLesson].videoSrc}/>}
                        </div>
                        <div className="info">
                            <p className = "text">
                                {data.lessons.length} Lessons
                            </p>
                            {data.lessons.map((x, index) => (
                                <div key={x.id} onClick = {changeCurrentLesson(index)} className={`lesson ${currentLesson === index ? "active": ""}`}>
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
                        {tabs.map((x, index) => (
                            <p key = {index} onClick = {changeCurrentTab(index)} className = {index === currentTab ? "active" : ""}>
                                {x}
                            </p>
                        ))}
                    </div>
                </div>
                {currentTab === 0 && <About title= {data.overview.title} description = {data.overview.description}/>}
                {currentTab === 1 && <Reviews getData={getData} courseId={id} reviews = {data.reviews}/>}
                {(currentTab === 2 && data.lessons[currentLesson].quiz) && <Quiz quiz={data.lessons[currentLesson].quiz!}/>}
            </div>
        </section>
    );
}

 export default CourseView;