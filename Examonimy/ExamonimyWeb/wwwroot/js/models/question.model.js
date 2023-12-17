import { Course } from "./course.model.js";

export class Question {
    id = 0;
    course = new Course();
    questionType = new QuestionType();
    questionLevel = new QuestionLevel();
    questionContent = "";
    author;

    constructor() { }
}

export class MultipleChoiceQuestionWithOneCorrectAnswer extends Question {
    choiceA = "";
    choiceB = "";
    choiceC = "";
    choiceD = "";
    correctAnswer = "";

    constructor() {
        super();
    }
}

export class MultipleChoiceQuestionWithMultipleCorrectAnswers extends Question {
    choiceA = "";
    choiceB = "";
    choiceC = "";
    choiceD = "";
    correctAnswers = "";

    constructor() {
        super();
    }
}

export class TrueFalseQuestion extends Question {
    correctAnswer = false;

    constructor() {
        super();
    }
}

export class ShortAnswerQuestion extends Question {
    correctAnswer = "";

    constructor() {
        super();
    }
}

export class FillInBlankQuestion extends Question {

    correctAnswers = [""];

    constructor() {
        super();
    }
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