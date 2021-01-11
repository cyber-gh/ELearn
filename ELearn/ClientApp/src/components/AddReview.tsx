import React, {useContext, useState} from 'react'
import {withFallback} from "../utils";
import {SnackbarContext} from "./AppSnackBar";
import {postReview} from "../api";

export interface Props {
    id: string,
    getData: () => void
}

const defaultValues = {
    title: "",
    recommendFor: "Beginner",
    comment: "",
}

const AddReview = ({id, getData}: Props) => {
    const {setData: setSnackbar} = useContext(SnackbarContext);
    const [open, setOpen] = useState(false);
    const [values, setValues] = useState(defaultValues);
    console.log(values);
    
    const handleChange = (id) => (e) => {
        setValues({
            ...values,
            [id]: e.target.value
        })
    }
    
    const handleSubmit = async (e) => {
        e.preventDefault();
        await withFallback(setSnackbar, async () => {
            await postReview({...values, courseId: id});
            setOpen(false);
            setSnackbar({type: "success", message: "Review added successfully"});
            getData();
        })
    }
    
    return (
        <div className="add-review">
            {!open ? 
            <button onClick = {() => setOpen(true)}>Leave a review</button> :
            <form onSubmit={handleSubmit}>
                <input onChange = {handleChange("title")} className="title" required placeholder="Review title"/>
                <p>Recommend Level</p>
                <select onChange = {handleChange("recommendFor")}>
                    {["Beginner", "Intermediate", "Expert"].map(x => (
                        <option key = {x} value={x}>{x}</option>
                    ))}
                </select>
                <textarea onChange = {handleChange("comment")} className="comment" required placeholder="Tell us more"/>
                <input type="submit" placeholder="Post Review"/>
            </form>
            }
        </div>
    );
}

export default AddReview;