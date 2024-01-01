import { Question } from "./question.model.js";

export class ExamPaperQuestion {
    number = 0;
    question = new Question();
    id = 0;
    comments = [new ExamPaperQuestionComment()];
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

export class ExamPaperQuestionCommentCreate {
    examPaperQuestionId;
    comment;

    constructor(examPaperQuestionId = 0, comment = "") {
        this.examPaperQuestionId = examPaperQuestionId;
        this.comment = comment;
    }
}

export class ExamPaperQuestionComment {
    commenterName;
    commenterProfilePicture;
    comment;
    commentedAt;

    constructor(commenterName = "", commenterProfilePicture = "", comment = "", commentedAt = "") {
        this.commenterName = commenterName;
        this.commenterProfilePicture = commenterProfilePicture;
        this.comment = comment;
        this.commentedAt = commentedAt;
    }
}