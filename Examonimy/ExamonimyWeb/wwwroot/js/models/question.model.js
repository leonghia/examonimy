import { Course } from "./course.model.js";

export class Question {
    course = new Course();
    questionType = new QuestionType();
    questionLevel = new QuestionLevel();
    questionContent = "";

    constructor() { }
}

export class QuestionLevel {
    id = 0;
    name = "";

    constructor(id = 0, name = "") {
        this.id = id;
        this.name = name;
    }
}

export class QuestionType {
    id = 0;
    name = "";

    constructor(id = 0, name = "") {
        this.id = id;
        this.name = name;
    }
}