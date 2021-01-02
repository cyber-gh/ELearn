import React, {useContext, useEffect, useRef, useState} from 'react'
import GrayBackground from "../components/GrayBackground";
import S3FileUpload from "react-s3";
import {AddCourseModel, Category} from "../interfaces";
import {assignCategory, getCategories, postCourse} from "../api";
import {
    Button, Checkbox,
    CircularProgress, Dialog, DialogActions,
    DialogContent,
    DialogTitle,
    LinearProgress,
    ListItemText,
    MenuItem,
    Snackbar
} from '@material-ui/core';
import {Alert} from "@material-ui/lab";
import {cacheImages, generateRandomString, withFallback} from "../utils";
import {SnackbarContext} from "../components/AppSnackBar";
import {bucket, uploadFile} from "../components/FileUploader";
import {stringify} from "querystring";
import CategoryCheckBox from "../components/CategoryCheckBox";


export interface Props {
    [key: string]: any
}

interface CategoryCheckModel {
    [key: string]: boolean | undefined
}

const userLevels = ["Beginner", "Intermediate", "Expert"];

const defaultFields = {
    title: "",
    previewImageUrl: "",
    description: "",
    length: 120,
    userLevel: userLevels[0]
}

const AddCourse =  (props: Props) => {
    const {setData: setSnackbar} = useContext(SnackbarContext);
    const [loading, setLoading] = useState(false);
    const [loadingProgress, setLoadingProgress] = useState(0);
    const [formLoading, setFormLoading] = useState(false);
    const [fields, setFields] = useState(defaultFields);
    const [dialogState, setDialogState] = useState(false);
    const [categories, setCategories] = useState<Category[]>([]);
    const [categoryChecked, setCategoryChecked] = useState <CategoryCheckModel> ({});
    let inputRef = useRef<HTMLInputElement | null>(null);
    
    console.log(categoryChecked);
    
    const handleCategoryCheck = (id) => (e) => {
        console.log("clicked n pula mea")
        setCategoryChecked({
            ...categoryChecked,
            [id]: !categoryChecked[id]
        })
    }
    
    const setField = (field: string) => (e) => {
        const newFields = {
            ...fields,
            [field]: e.target.value
        }
        setFields(newFields);
    }
    
    const addCourse = async (e) => {
        e.preventDefault();
        setFormLoading(true);
        const model: AddCourseModel = {
            ...fields
        }
        await withFallback(setSnackbar, async () => {
            const course = await postCourse(model);
            const ids = categories.filter(it => categoryChecked[it.id]).map(it => it.id);
            let tasks = ids.map(it => (assignCategory(course.id, it)));
            await Promise.all(tasks);
            setSnackbar({message: "Course added!", type: "success"})
            props.history.push(`/add-lessons/${course.id}`)
        })
    }
    
    const getData = async () => {
        let categories = await getCategories();
        setCategories(categories);
        setFields(fields);
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
                                <input placeholder="Click to choose course categories" readOnly={true} type = "text" value={categories.filter(x => categoryChecked[x.id]).map(x => x.name).join(', ')} onClick={() => setDialogState(true)}/>
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
                <Dialog scroll = "paper" open={dialogState} onClose={() => setDialogState(false)}>
                    <DialogTitle>Select Categories</DialogTitle>
                    <DialogContent style = {{width: "400px"}}>
                        {categories.map(x => (
                            <CategoryCheckBox onClick = {handleCategoryCheck(x.id)} key = {x.id} checked = {!!categoryChecked[x.id]} name = {x.name}/>
                        ))}
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={() => setDialogState(false)} color="primary">
                            Close
                        </Button>
                    </DialogActions>
                </Dialog>
            </>
        
    );
}

export default AddCourse;