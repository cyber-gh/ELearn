import {AddCourseModel, Category, CourseModel} from "./interfaces";
import {useEffect} from "react";

type Method = "GET" | "POST" | "DELETE" | "PUT";

const getCategories = async () :Promise<Category[]> => {
    const url = "/api/categories/all";
    return (await makeNetworkCall(url, [], "GET", null)) as Category[];
}

const postCourse = async (data: AddCourseModel) : Promise<CourseModel> => {
    const url = "/api/createcourse"
    
    return (await makeNetworkCall(url, [], "POST", data) ) as CourseModel;
}

const assignCategory = async (courseId: string, categoryId: string): Promise<object> => {
    const url = "/api/createcourse/category"
    return (await makeNetworkCall(url, [], "POST", {
        courseId: courseId,
        categoryId: categoryId
    })) 
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
    if (raw === "") return {};
    else return JSON.parse(raw);
}

export {getCategories, postCourse, assignCategory};