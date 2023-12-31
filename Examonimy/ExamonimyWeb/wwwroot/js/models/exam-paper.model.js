﻿import { Course } from "./course.model.js";
import { ExamPaperQuestionCreate, ExamPaperQuestionUpdate } from "./exam-paper-question.model.js";
import { User } from "./user.model.js";

export class ExamPaper {
    id = 0;
    examPaperCode = "";
    authorName = "";
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
    commitMessage = "";

    constructor(examPaperQuestions = [new ExamPaperQuestionUpdate()], commitMessage = "") {
        this.examPaperQuestions = examPaperQuestions;
        this.commitMessage = commitMessage;
    }
}

export class ExamPaperReviewerCreate {
    reviewerIds;

    constructor(reviewerIds = [0]) {
        this.reviewerIds = reviewerIds;
    }
}

export class ExamPaperReviewHistory {
    id = 0;
    actorName = "";
    actorProfilePicture = "";
    createdAt = "";
    operationId = 0;
}

export class ExamPaperReviewHistoryComment extends ExamPaperReviewHistory {
    comment = "";
    isAuthor = false;

    constructor() {
        super();
    }
}

export class ExamPaperReviewHistoryAddReviewer extends ExamPaperReviewHistory {
    reviewerName = "";

    constructor() {
        super();
    }
}

export class ExamPaperReviewHistoryEdit extends ExamPaperReviewHistory {
    commitMessage = "";

    constructor() {
        super();
    }
}

export class ExamPaperReviewCommentCreate {
    comment;
    constructor(comment = "") {
        this.comment = comment;
    }
}