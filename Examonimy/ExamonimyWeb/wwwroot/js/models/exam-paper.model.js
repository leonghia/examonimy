import { Course } from "./course.model.js";

export class ExamPaper {
    id = 0;
    course = new Course();
    examPaperCode = "";
    numbersOfQuestion = 0;
    author;
    status = 0;
    statusAsString = "";
}