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
    visitors: number,
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
    quiz: QuizModel | null
}

interface CourseDetailsModel {
    id: string,
    overview: CourseModel,
    lessons: LessonModel[],
    reviews: ReviewModel[],
}

interface UserModel {
    email: string,
    fullName: string,
    id: string,
    isConfirmed: boolean
}

interface ReviewModel {
    fullName: string,
    title: string,
    comment: string,
    createdDate: string,
    recommendFor: "Beginner" | "Intermediate" | "Expert";
    user?: UserModel,
    id?: string,
}

interface QuizModel {
    id: string,
    elements: {
        id: string,
        question: string,
        answers: string[],
        correctAnswer: string
    }[]
}

export type {QuizModel,UserModel, CourseDetailsModel, CourseCardData, CourseSliderElement, RouteData, Category, CourseModel, AddCourseModel, LessonModel, ReviewModel};
