import { MAX_PAGE_SIZE } from "../config.js";
export class RequestParams {
    pageSize = 10;
    pageNumber = 1;
    searchQuery = null;

    constructor(searchQuery = null, pageSize = 10, pageNumber = 1) {
        this.searchQuery = searchQuery;
        this.pageSize = pageSize || 10;
        this.pageNumber = pageNumber || 1;
    }
}

export class QuestionRequestParams extends RequestParams {
    courseId = 0;
    questionTypeId = 0;
    questionLevelId = 0;

    constructor(searchQuery = null, courseId = null, questionTypeId = null, questionLevelId = null, pageSize = null, pageNumber = null) {
        super(searchQuery, pageSize, pageNumber);
        this.courseId = courseId;
        this.questionTypeId = questionTypeId;
        this.questionLevelId = questionLevelId;
    }
}

export class ExamPaperRequestParams extends RequestParams {
    courseId = 0;
    status = 0;

    constructor(courseId = null, status = null, searchQuery = null, pageSize = null, pageNumber = null) {
        super(searchQuery, pageSize, pageNumber);
        this.courseId = courseId;
        this.status = status;
    }
}

export class UserRequestParams extends RequestParams {
    roleId = 0;

    constructor(roleId = null, searchQuery = null, pageSize = null, pageNumber = null) {
        super(searchQuery, pageSize, pageNumber);
        this.roleId = roleId;
    }
}