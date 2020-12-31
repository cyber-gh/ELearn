interface CourseSliderElement {
    link: string,
    title: string,
    description: string,
    id: string
}

interface CourseCardData {
    imageUrl: string,
    duration: string,
    title: string,
    author: string,
    id: string,
    students: number
}

interface RouteData {
    path: string,
    component: (params: any) => JSX.Element,
    condition?: boolean,
    redirect?: string,
    props?: object
}

interface Category {
    id: string,
    name: string
}

interface CourseModel {
    id: string,
    title: string,
    previewImageUrl: string,
    description: string,
    length: number,
    userLevel: string,
    categories: Category[]
}

interface AddCourseModel {
    title: string,
    previewImageUrl: string | null,
    description: string,
    length: number,
    userLevel: string
}

export type {CourseCardData, CourseSliderElement, RouteData, Category, CourseModel, AddCourseModel};
