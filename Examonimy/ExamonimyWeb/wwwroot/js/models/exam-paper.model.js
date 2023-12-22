import { Course } from "./course.model.js";
import { ExamPaperQuestionCreate, ExamPaperQuestionUpdate } from "./exam-paper-question.model.js";

export class ExamPaper {
    id = 0;
    course = new Course();
    examPaperCode = "";
    numbersOfQuestion = 0;
    author;
    status = 0;
    statusAsString = "";
}

export class ExamPaperCreate {
    examPaperCode = "";
    courseId = 0;
    examPaperQuestions = [new ExamPaperQuestionCreate()];

    constructor(courseId = 0, examPaperCode = "", examPaperQuestions = [new ExamPaperQuestionCreate()]) {
        this.courseId = courseId;
        this.examPaperCode = examPaperCode;
        this.examPaperQuestions = examPaperQuestions;
    }
}

export class ExamPaperUpdate {
    examPaperQuestions = [new ExamPaperQuestionUpdate()];
}