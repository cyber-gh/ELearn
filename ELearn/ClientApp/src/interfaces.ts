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
    component: any,
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
    categories: Category[],
    appUser: {
        fullName: string,
    }
}

interface AddCourseModel {
    title: string,
    previewImageUrl: string | null,
    description: string,
    userLevel: string
}

interface LessonModel {
    id: string,
    title: string,
    videoSrc: string,
    duration: number,
    quiz?: object
}

interface CourseDetailsModel {
    id: string,
    overview: CourseModel,
    lessons: LessonModel[],
}

interface ReviewModel {
    username: string,
    title: string,
    description: string,
    timeAdded: string,
    recommend: "Beginner" | "Intermediate" | "Expert";
}

export type {CourseDetailsModel, CourseCardData, CourseSliderElement, RouteData, Category, CourseModel, AddCourseModel, LessonModel, ReviewModel};
