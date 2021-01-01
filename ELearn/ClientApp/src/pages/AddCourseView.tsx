import React, {useContext, useEffect, useRef, useState} from 'react'
import GrayBackground from "../components/GrayBackground";
import S3FileUpload from "react-s3";
import {AddCourseModel, Category} from "../interfaces";
import {assignCategory, getCategories, postCourse} from "../api";
import {CircularProgress, LinearProgress, Snackbar} from '@material-ui/core';
import {Alert} from "@material-ui/lab";
import {cacheImages, generateRandomString} from "../utils";
import {SnackbarContext} from "../components/AppSnackBar";
import {bucket, uploadFile} from "../components/FileUploader";
import {stringify} from "querystring";


export interface Props {
    [key: string]: any
}

const userLevels = ["Beginner", "Intermediate", "Expert"];

const defaultFields = {
    title: "",
    previewImageUrl: "",
    description: "",
    length: 120,
    category: "",
    userLevel: userLevels[0]
}

const AddCourse =  (props: Props) => {
    const {setData: setSnackbar} = useContext(SnackbarContext);
    const [loading, setLoading] = useState(false);
    const [loadingProgress, setLoadingProgress] = useState(0);
    const [formLoading, setFormLoading] = useState(false);
    const [fields, setFields] = useState(defaultFields);
    const [categories, setCategories] = useState<Category[]>([]);
    let inputRef = useRef<HTMLInputElement | null>(null);
    
    const setField = (field: string) => (e) => {
        const newFields = {
            ...fields,
            [field]: e.target.value
        }
        setFields(newFields);
    }
    
    const withFallback = async (action) => {
        try {
            await action()
        } catch (e) {
            // console.log(e);
            console.log(e.message);
            setSnackbar({
                message: e.message,
                type: "error"
            })
        }
    }
    
    const addCourse = async (e) => {
        e.preventDefault();
        setFormLoading(true);
        const model: AddCourseModel = {
            ...fields
        }
        await withFallback(async () => {
            const course = await postCourse(model);
            const categoryId = fields.category;
            await assignCategory(course.id, categoryId);
            setSnackbar({message: "Course added!", type: "success"})
            props.history.push(`/add-lessons/${course.id}`)
        })
        
        setFormLoading(false);
    }
    
    const getData = async () => {
        let categories = await getCategories();
        setCategories(categories);
        setFields({
            ...fields,
            category: categories[0].id
        })
        console.log(categories);
    }
    
    useEffect(() => {
        getData();
    }, []);
    
    const handleClick = () => {
        inputRef.current?.click();
    }
    
    const handleUpload = async () => {
        let input = inputRef.current as HTMLInputElement;
        if (input.files?.length && input.files?.length > 0){
            setLoading(true);
            const file = input.files[0];
            let blob = file.slice(0, file.size, 'image/*');
            let extension = file.name.split(".").slice(-1)[0];
            let randomString = generateRandomString();
            let renamedFile = new File([blob], `${randomString}.${extension}`, {type: 'image/' + extension});
            // let response = await S3FileUpload.uploadFile(renamedFile, config);
            let location = await uploadFile(renamedFile, (progress) => {
                setLoadingProgress(progress);
            })
            await cacheImages([location]);
            setLoading(false);
            setFields({
                ...fields,
                previewImageUrl: location
            })
        }
    }
    
    return (
            <>
                <GrayBackground/>
                <section className="course-overview">
                    <p className="title">Course Overview</p>
                    <p className="description">Add class details to help students discover and learn about your class.</p>
                    <hr/>
    
                    <form className="form" onSubmit={addCourse}>
                        <div className="element">
                            <p className="label">Course Title</p>
                            <input value={fields.title} required type="text" placeholder="Write a course title" onChange={setField("title")}/>
                        </div>
    
                        <div className="element">
                            <p className="label">Description</p>
                            <textarea value={fields.description} required rows={6} placeholder="Write a course description" onChange={setField("description")}/>
                        </div>
                        
                        <div className="image-uploader">
                            {loading && <LinearProgress className="loader" variant="determinate" value={loadingProgress} />}
                            {fields.previewImageUrl !== null && <img src={fields.previewImageUrl! }/>}
                            <input onChange={handleUpload} ref = {inputRef} type = "file" accept = "image/*" multiple = {false}/>
                            {!loading && <button type="button" onClick={handleClick} className={fields.previewImageUrl ? "absolute" : ""}>Upload Image</button>}
                        </div>
                        
                        <div className="row">
                            <div className="element first">
                                <p className="label">Category</p>
                                <select value={fields.category} onChange={setField("category")}>
                                    {categories.map((it) => (
                                        <option key={it.id} value={it.id}>{it.name}</option>
                                    ))}
                                </select>

                            </div>

                            <div className="element">
                                <p className="label">User Level</p>
                                <select value={fields.userLevel} onChange={setField("userLevel")}>
                                    {userLevels.map((it) => (
                                        <option key={it} value={it}>{it}</option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        {formLoading ? 
                            <div className="circular-progress">
                                <CircularProgress/>
                            </div> : 
                            <input type="submit" className="create" value="Create"/>
                        }
                    </form>
                </section>
            </>
        
    );
}

export default AddCourse;