import history from "../history";
import React, {useEffect, useState} from 'react'
import {Category, CourseModel} from "../interfaces";
import {getCategories, searchCourse} from "../api";
import {Link} from "react-router-dom";

export interface Props {
    open: boolean,
    searchKey: string,
    className?: string,
    close: () => void,
}

const SearchContext = ({open, searchKey, close, className: classes}: Props) => {
    let key = searchKey.trim();
    const [data, setData] = useState <null | CourseModel[]> (null);
    
    const getData = async () => {
        const courses = await searchCourse(key);
        setData(courses);
    }
    
    useEffect(() => {
        if (key.length > 0)
            getData();
    }, [key])
    
    const goTo = (link) => (e) => {
        close();
        history.push(link);
    }
    
    if (!data || !open) return null;
    
    return (
        <div className={"menu-context " + (classes ? classes: "")}>
            <p className="title">
                Courses:
            </p>
            <div className="content">
                {(data?.length === 0) ? 
                    <p className="not-found">No courses found...</p> :
                    data.map(x => (
                        <div onClick={goTo(`/course/${x.id}`)} key = {x.id} className = "element">
                            {x.title}
                        </div>
                    ))
                }
            </div>
        </div>
    );
}

export default SearchContext;