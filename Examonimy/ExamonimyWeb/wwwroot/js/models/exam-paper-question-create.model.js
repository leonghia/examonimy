export class ExamPaperQuestionCreate {
    
    questionId = 0;
    number = 0;

    constructor(questionId = 0, number = 0) {       
        this.questionId = questionId;
        this.number = number;
    }
}