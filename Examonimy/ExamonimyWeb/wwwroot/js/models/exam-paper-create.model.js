import { ExamPaperQuestionCreate } from "./exam-paper-question-create.model.js";

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