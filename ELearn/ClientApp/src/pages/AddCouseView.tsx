import React, {useRef, useState} from 'react'
import GrayBackground from "../components/GrayBackground";
import S3FileUpload from "react-s3";
import config from "../components/s3config";

export interface Props {
    [key: string]: any
}

const AddCourse =  (props: Props) => {
    const [image, setImage] = useState<string | null>(null);
    let inputRef = useRef<HTMLInputElement | null>(null);
    
    const handleClick = () => {
        inputRef.current?.click();
    }
    
    const handleUpload = async () => {
        let input = inputRef.current as HTMLInputElement;
        if (input.files?.length && input.files?.length > 0){

            
            const file = input.files[0];
            let blob = file.slice(0, file.size, 'image/*');
            
            let extension = file.name.split(".").slice(-1)[0];
            let randomString = Math.random().toString(36).substring(7);
            let renamedFile = new File([blob], `${randomString}.${extension}`, {type: 'image/' + extension});
            let response = await S3FileUpload.uploadFile(renamedFile, config);
            console.log(response);
            setImage(response.location);
        }
        
    }
    
    return (
            <>
                <GrayBackground/>
                <section className="course-overview">
                    <p className="title">Course Overview</p>
                    <p className="description">Add class details to help students discover and learn about your class.</p>
                    <hr/>
    
                    <div className="form">
                        <div className="element">
                            <p className="label">Course Title</p>
                            <input type="text" placeholder="Write a course title"/>
                        </div>
    
                        <div className="element">
                            <p className="label">Description</p>
                            <textarea rows={6}/>
                        </div>
                        
                        <div className="image-uploader">
                            {image && <img src={image}/>}
                            <input onChange={handleUpload} ref = {inputRef} type = "file" accept = "image/*" multiple = {false}/>
                            <button onClick={handleClick} className={image ? "absolute" : ""}>Upload Image</button>
                        </div>
                        
                        <div className="row">
                            <div className="element first">
                                <p className="label">Category</p>
                                <select>
                                    <option value="1">Trending</option>
                                    <option value="2">Popular</option>
                                    <option value="3">Just good</option>
                                </select>

                            </div>

                            <div className="element">
                                <p className="label">User Level</p>
                                <select>
                                    <option value="1">Trending</option>
                                    <option value="2">Popular</option>
                                    <option value="3">Just good</option>
                                </select>
                            </div>
                        </div>
    
                        <input type="submit" className="create" value="Create"/>
    
                    </div>
                </section>
            </>
        
    );
}

export default AddCourse;