import {AddCourseModel, Category, CourseDetailsModel, CourseModel, LessonModel} from "./interfaces";
import {useEffect} from "react";

type Method = "GET" | "POST" | "DELETE" | "PUT";

const getCourseById = async (id: string): Promise<CourseDetailsModel> => {
    const url = "api/coursedetails";
    return (await makeNetworkCall(url, [["id", id]], "GET", null)) as CourseDetailsModel;
}

const getCoursesByCategory = async (category: string): Promise<CourseModel[]> => {
    const url = "/api/courselist/category";
    return (await makeNetworkCall(url, [["name", category]], "GET", null)) as CourseModel[];
}

const getCourses = async (): Promise<CourseModel[]> => {
    const url = "/api/courselist";
    return (await makeNetworkCall(url, [], "GET", null)) as CourseModel[];
}

const getCategories = async () :Promise<Category[]> => {
    const url = "/api/categories/all";
    return (await makeNetworkCall(url, [], "GET", null)) as Category[];
}

const postCourse = async (data: AddCourseModel) : Promise<CourseModel> => {
    const url = "/api/createcourse";
    return (await makeNetworkCall(url, [], "POST", data) ) as CourseModel;
}

const assignCategory = async (courseId: string, categoryId: string): Promise<object> => {
    const url = "/api/createcourse/category"
    return (await makeNetworkCall(url, [], "POST", {
        courseId,
        categoryId,
    })) 
}

const addLesson = async (courseId: string, title: string, videoSrc: string, duration: number): Promise<LessonModel> => {
    const url = "api/createcourse/lesson"
    return (await makeNetworkCall(url, [], "POST", {
        courseId,
        title,
        videoSrc,
        duration
    })) as LessonModel;
}

const updateLesson = async (lessonId: string, title: string): Promise<LessonModel> => {
    const url = "api/createcourse/lesson"
    return (await makeNetworkCall(url, [], "PUT", {
        lessonId: lessonId,
        title: title
    })) as LessonModel;
}

const getLessons = async (courseId: string): Promise<LessonModel[]> => {
    const url = "api/coursedetails/lessons"
    return (await makeNetworkCall(url, [["id", courseId]], "GET", null)) as LessonModel[];
}

const removeLesson = async (lessonId: string): Promise <object> => {
    const url = "/api/CreateCourse/lesson";
    return (await makeNetworkCall(url, [["lessonId", lessonId]], "DELETE", null));
}

const makeNetworkCall = async (url: string, params: [string, string][] = [], method: Method = "GET", data: any | null = null): Promise<object> => {
    const link = `${url}?${params.map(it => it[0] + '=' + it[1]).join(",")}`
    const options = {
        method: method,
        headers: { 'Content-Type': 'application/json' },
        body: data === null ? undefined : JSON.stringify(data)
    }
    const response = await fetch(link, options);
    const raw = await response.text();
    if (response.status >= 400) {
        const json = JSON.parse(raw);
        throw new Error(json.title)
    }
    if (raw === "") return {};
    else return JSON.parse(raw);
}



export {getCategories, postCourse, assignCategory, addLesson, getLessons, removeLesson, getCoursesByCategory, getCourseById, updateLesson, getCourses};