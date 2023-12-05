import { Course } from "./course.model.js";
import { QuestionLevel } from "./question-level.model.js";
import { QuestionType } from "./question-type.model.js";

export class Question {
    course = new Course();
    questionType = new QuestionType();
    questionLevel = new QuestionLevel();
    questionContent = "";

    constructor() { }
}