import React, { useEffect, useRef, useState } from 'react'
import {IconButton} from "@material-ui/core";
import ArrowRight from '@material-ui/icons/ArrowForwardIos';
import ArrowLeft from '@material-ui/icons/ArrowBackIos';
import {breakpoints} from "../utils";
import { cpuUsage } from 'process';
import {CourseModel} from "../interfaces";
import CourseCard from "./CourseCard";
import {getCoursesByCategory} from "../api";

export interface Props {
    category: string
}


const CourseCardCarousel = ({category}: Props) => {
    const mainContainer = useRef <any> ();
    const slide = useRef <any> ();
    const [counter, setCounter] = useState(0);
    const [view, setView] = useState(4);
    const [data, setData] = useState <CourseModel[]> ([]);

    const getData = async () => {
        let courses = await getCoursesByCategory(category);
        setData(courses);
    }
    
    const handleResize = () => {
        const w = window.innerWidth;
        const {mobile, tablet, smallScreen, largeScreen} = breakpoints;
        let view = 0;
        if (w <= mobile) {
            view = 1;
        }
        else if (w <= tablet) {
            view = 2;
        }
        else if (w <= smallScreen) {
            view = 3;
        }
        else {
            view = 4;
        }
        setView(view);
    }
    
    useEffect(() => {
        handleResize();
        window.addEventListener("resize", handleResize);
    }, [])

    const left = () => {
        if (counter === 0)
            return;
        setCounter(counter - 1);
    }

    const right = () => {
        if (counter == data.length - 1)
            return;
        setCounter(counter + 1);
    }
    
    useEffect(() => {
        getData();
    }, [])

    let displayData = data;
    if (view == 1){
        displayData = displayData.slice(0, 4);
    }
    else if (displayData.length % view !== 0){
        displayData = displayData.slice(0, Math.floor(displayData.length / view) * view)
    }


    return (
        <section ref = {mainContainer} className = "card-carousel">
            <IconButton disabled = {counter === 0} onClick = {left} className = "arrow arrow-left">
                <ArrowLeft/>
            </IconButton>
            <div className="window">
                <div className="content" style = {{transform: `translateX(${-counter * 100}%)`}}>
                    {displayData.map((x, index) => (
                        <div key = {index} className="item">
                            <CourseCard key = {index} {...x} edit = {false}/>
                        </div>
                    ))}
                </div>
            </div>
            <IconButton disabled = {(counter + 1) * view >= displayData.length - 1} onClick = {right} className = "arrow arrow-right">
                <ArrowRight/>
            </IconButton>
        </section>
    );
}

export default CourseCardCarousel;