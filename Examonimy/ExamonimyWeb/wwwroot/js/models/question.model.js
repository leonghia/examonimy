import { Course } from "./course.model.js";

export class Question {
    course = new Course();
    type = 0;
    level = 0;
    title = "";

    constructor() {

    }
}