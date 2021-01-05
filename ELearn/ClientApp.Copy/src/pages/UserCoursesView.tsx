import React, {useEffect, useState} from 'react'
import {CourseModel} from "../interfaces";
import {getCourses} from "../api";
import {Link} from "react-router-dom";
import CourseCard from "../components/CourseCard";

export interface Props {
    [key: string]: any
}

const UserCoursesView = (props: Props) => {
    const [data, setData] = useState <CourseModel[]> ([]);

    const getData = async () => {
        let courses = await getCourses();
        setData(courses);
    }
    
    useEffect(() => {
        getData();
    }, [])
    
    return (
        <>
            <section className = "user-classes">
                <p className="title">My Classes</p>
                <div className="courses">
                    {data.map((x, index) => (
                        <div key = {index} className="element">
                            <CourseCard {...x} edit/>
                        </div>
                    ))}
                </div>
            </section>
        </>
    );
}

export default UserCoursesView;