import { Course } from "./course.model.js";

export class ExamPaper {
    id = 0;
    course = new Course();
    examPaperCode = "";
    numbersOfQuestion = 0;
}