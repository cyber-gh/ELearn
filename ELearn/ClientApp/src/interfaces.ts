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

export type {CourseCardData, CourseSliderElement, RouteData};
