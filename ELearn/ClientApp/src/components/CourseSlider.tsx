import React, { useState, useEffect } from 'react'
import {IconButton} from "@material-ui/core";
import ArrowRight from '@material-ui/icons/ArrowForwardIos';
import ArrowLeft from '@material-ui/icons/ArrowBackIos';
import PlayArrow from '@material-ui/icons/PlayArrow';
import { cacheImages } from '../utils';
import { Link } from 'react-router-dom';
import { CourseSliderElement } from '../interfaces';
import {getCoursesByCategory} from "../api";
import {CourseModel} from "../interfaces";

export interface Props {
   [key: string]: any;
}
const CourseSlider = (props: Props) => {
    const [selected, setSelected] = useState(0);
    const [data, setData] = useState <CourseModel[] | null> (null);
    
    const getData = async () => {
        let courses = await getCoursesByCategory("recommended");
        const urls = courses.map(x => x.previewImageUrl);
        await cacheImages(urls);
        setData(courses);
    }

    const next = () => {
        if (selected + 1 !== data!.length){
            setSelected(selected + 1);
        }
        else{
            setSelected(0);
        }
    }

    const previous = () => {
        if (selected !== 0){
            setSelected(selected - 1);
        }
        else{
            setSelected(data!.length - 1);
        }
    }

    useEffect(() => {
        getData();
    }, [])
    
    useEffect(() => {
        if (data) {
            let timeout = setTimeout(next, 6000);
            return () => clearTimeout(timeout);
        }
    })

    if (!data) {
        return (
            <div className= "placeholder">
            </div>
        )
    }
    
    return (
        <div className="course-slider">
            {data.map((x, index) => (
                <div key = {x.previewImageUrl + index} className = {`banner ${index === selected ? "active": "inactive"}`} style = {{backgroundImage: `url(${x.previewImageUrl})`}}/>
            ))}
            {data.map((x, index) => (
                <div key = {x.title + x.description} className={`content ${index === selected ? "active": "inactive"}`}>
                    <p className="title">
                        {x.title}
                    </p>
                    <div className="p description">
                        {x.description}
                    </div>
                    <Link to = {`course/${x.id}`}>
                        <button>
                            <PlayArrow className = "icon"/>
                            Watch Now
                        </button>
                    </Link>
                </div> 
            ))}
            <div className="gradient"/>
            <div className="slider">
                <IconButton onClick = {previous} className = "arrow-left arrow">
                    <ArrowLeft style = {{marginLeft: "5px", width: "21px"}}/>
                </IconButton>
                {data.map((x, index) => (
                    <div key = {index} className={`circle ${index === selected ? "active" : "inactive"}`}/>
                ))}
                <IconButton onClick = {next} className = "arrow-right arrow">
                    <ArrowRight style = {{fontSize: "1.42rem"}}/>
                </IconButton>
            </div>
        </div>
    );
}

export default CourseSlider;