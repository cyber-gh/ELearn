import React, {useEffect, useState} from 'react'
import {CourseModel} from "../interfaces";
import {getCoursesByCategory, getMyCourses} from "../api";
import CourseCard from "../components/CourseCard";

export interface Props {
    [key: string]: any
}

const CoursesCategoryView = (props: Props) => {
    const id = props.match.params.id;
    const [data, setData] = useState <CourseModel[]> ([]);
    console.log(data);

    const getData = async () => {
        let courses = await getCoursesByCategory(id);
        console.log(courses);
        setData(courses);
    }

    useEffect(() => {
        getData();
    }, [id])

    return (
        <>
            <section className = "user-classes">
                <p className="title">{id}</p>
                <div className="courses">
                    {data.map((x, index) => (
                        <div key = {index} className="element">
                            <CourseCard {...x} edit={false}/>
                        </div>
                    ))}
                </div>
            </section>
        </>
    );
}

export default CoursesCategoryView;