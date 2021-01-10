import React, {useState} from 'react'
import {Link} from "react-router-dom"
import {Paper} from "@material-ui/core"
import {BookmarkBorder as BookmarkEmpty, Bookmark as BookmarkFilled, Edit} from '@material-ui/icons';
import {CourseModel} from '../interfaces';

export interface Props extends CourseModel {
    edit: boolean
}

export default ({previewImageUrl, id, length, title, edit, appUser: {fullName: author}, visitors}: Props) => {
    const [bookmarked, setBookmarked] = useState(false);

    const handleClick = () => {
        setBookmarked(!bookmarked);
    }
    
    const link = edit ? `/add-lessons/${id}` : `/course/${id}`;

    return (
        <Link to = {link} className = "course-card">
            <img src = {previewImageUrl} className = "course-image" alt = "Not found"/>
            <div className = "course-content">
                <div className = "separe separe_1">
                    <p>
                        {visitors} students
                    </p>
                    <p>
                        {Math.floor(length / 60)} minutes
                    </p>
                </div>
                <p className = "course-title">{title}</p>
                <div className = "separe separe_2">
                    <p>{author}</p>
                    {!edit ? 
                        (bookmarked ? <BookmarkFilled className = "icon bookmark_active" onClick = {handleClick}/> : <BookmarkEmpty className = "bookmark" onClick = {handleClick}/>) :
                        <Edit className = "icon edit-icon" />
                    }
                </div>
            </div>
        </Link>
    );
}