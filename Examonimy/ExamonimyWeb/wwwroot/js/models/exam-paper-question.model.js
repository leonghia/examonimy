import { Question } from "./question.model.js";

export class ExamPaperQuestion {
    number = 0;
    question = new Question();
}

export class ExamPaperQuestionCreate {

    questionId = 0;
    number = 0;

    constructor(questionId = 0, number = 0) {
        this.questionId = questionId;
        this.number = number;
    }
}

export class ExamPaperQuestionUpdate {
    questionId = 0;
    number = 0;

    constructor(questionId = 0, number = 0) {
        this.questionId = questionId;
        this.number = number;
    }
}