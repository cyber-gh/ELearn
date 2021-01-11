import React, {useContext, useEffect, useState} from 'react'
import {CourseModel} from "../interfaces";
import {getCourses, getMyCourses, getUserClasses} from "../api";
import {Link} from "react-router-dom";
import CourseCard from "../components/CourseCard";
import {AuthContext, AuthProvider} from "../components/AuthProvider";

export interface Props {
    [key: string]: any
}

const UserCoursesView = (props: Props) => {
    const [data, setData] = useState <CourseModel[]> ([]);
    const {user} = useContext(AuthContext)
    console.log(user);

    const getData = async () => {
        let courses = await getUserClasses();
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