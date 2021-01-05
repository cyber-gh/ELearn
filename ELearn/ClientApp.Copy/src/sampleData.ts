import { CourseCardData, CourseSliderElement } from "./interfaces";

const course1: CourseCardData = {
    author: "John Walker",
    duration: "1h 33m",
    imageUrl: "https://static.skillshare.com/cdn-cgi/image/width=448,quality=85,format=auto/uploads/video/thumbnails/e89b19f912dcb5604c3b88ea32ca62d2/original",
    id: "0",
    title: "Learn how to suparat people and then they hate you because yes!",
    students: 1276
}
const course2: CourseCardData = {
    author: "VANILLA JON",
    duration: "2h 35m",
    imageUrl: "https://static.skillshare.com/cdn-cgi/image/width=448,quality=85,format=auto/uploads/video/thumbnails/e89b19f912dcb5604c3b88ea32ca62d2/original",
    id: "1",
    title: "LEARN HOW TO MAKE GOD WEBSITE OK?",
    students: 69420
}
const courseSliderData: CourseSliderElement[] = [
    {
        title: "Why are you gae",
        description: "To inspire the world about gae and how it helped people learn more about themselves",
        link: "https://static.skillshare.com/assets/images/homepage/promo-banner/hallease-narvaez-bg-desktop.jpg",
        id: "0"
    },
    {
        title: "Why are you not so gae",
        description: "To inspire the world about gae and how it helped people learn more about themselves",
        link: "https://static.skillshare.com/assets/images/homepage/promo-banner/danni-fisher-shin-bg-desktop.jpg",
        id: "1"
    },
    {
        title: "Why are you  so gae",
        description: "To inspire the world about gae and how it helped people learn more about themselves",
        link: "https://static.skillshare.com/assets/images/homepage/promo-banner/olivia-wilde-bg-desktop.jpg",
        id: "2"
    },
]

export {course1, course2, courseSliderData};