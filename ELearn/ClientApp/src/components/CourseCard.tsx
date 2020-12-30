import React, {useState} from 'react'
import {Link} from "react-router-dom"
import {Paper} from "@material-ui/core"
import {BookmarkBorder as BookmarkEmpty, Bookmark as BookmarkFilled} from '@material-ui/icons';
import { CourseCardData } from '../interfaces';

export default ({imageUrl, id, duration, title, author, students}: CourseCardData) => {
    const [bookmarked, setBookmarked] = useState(false);

    const handleClick = () => {
        setBookmarked(!bookmarked);
    }

    return (
        <Link to = {`/course/${id}`} className = "course-card">
            <img src = {imageUrl} className = "course-image" alt = "Not found"/>
            <div className = "course-content">
                <div className = "separe separe_1">
                    <p>
                        {students} students
                    </p>
                    <p>
                        {duration}
                    </p>
                </div>
                <p className = "course-title">{title}</p>
                <div className = "separe separe_2">
                    <p>{author}</p>
                    {bookmarked ? <BookmarkFilled className = "bookmark bookmark_active" onClick = {handleClick}/> : <BookmarkEmpty className = "bookmark" onClick = {handleClick}/>}
                </div>
            </div>
        </Link>
    );
}