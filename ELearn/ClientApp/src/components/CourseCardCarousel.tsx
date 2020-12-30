import React, { useEffect, useRef, useState } from 'react'
import {IconButton} from "@material-ui/core";
import ArrowRight from '@material-ui/icons/ArrowForwardIos';
import ArrowLeft from '@material-ui/icons/ArrowBackIos';
import {breakpoints} from "../utils";
import { cpuUsage } from 'process';

export interface Props {
    [key: string]: any
}


const CourseCardCarousel = ({children}: Props) => {
    const mainContainer = useRef <any> ();
    const slide = useRef <any> ();
    const [counter, setCounter] = useState(0);
    const [view, setView] = useState(4);


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
        if (counter == children.length - 1)
            return;
        setCounter(counter + 1);
    }

    let data = children;
    if (view == 1){
        data = data.slice(0, 4);
    }
    else if (data.length % view !== 0){
        data = data.slice(0, Math.floor(data.length / view) * view)
    }


    return (
        <section ref = {mainContainer} className = "card-carousel">
            <IconButton disabled = {counter === 0} onClick = {left} className = "arrow arrow-left">
                <ArrowLeft/>
            </IconButton>
            <div className="window">
                <div className="content" style = {{transform: `translateX(${-counter * 100}%)`}}>
                    {data.map((x, index) => (
                        <div key = {index} className="item">
                            {x}
                        </div>
                    ))}
                </div>
            </div>
            <IconButton disabled = {(counter + 1) * view >= data.length - 1} onClick = {right} className = "arrow arrow-right">
                <ArrowRight/>
            </IconButton>
        </section>
    );
}

export default CourseCardCarousel;