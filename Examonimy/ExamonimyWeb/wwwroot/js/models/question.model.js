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
    correctAnswers = [""];

    constructor() {
        super();
    }
}

export class TrueFalseQuestion extends Question {
    correctAnswer = "";

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

export class QuestionCreateDto {
    courseId = 0;
    questionTypeId = 0;
    questionLevelId = 0;
    questionContent = "";
}

export class MultipleChoiceQuestionCreateDto extends QuestionCreateDto {
    choiceA = "";
    choiceB = "";
    choiceC = "";
    choiceD = "";
}

export class MultipleChoiceQuestionWithOneCorrectAnswerCreateDto extends MultipleChoiceQuestionCreateDto {
    correctAnswer = 0;
}

export class MultipleChoiceQuestionWithMultipleCorrectAnswersCreateDto extends MultipleChoiceQuestionCreateDto {
    correctAnswers = "";
}

export class TrueFalseQuestionCreateDto extends QuestionCreateDto {
    correctAnswer = false;
}

export class ShortAnswerQuestionCreateDto {
    correctAnswer = "";
}

export class FillInBlankQuestionCreateDto extends QuestionCreateDto {
    correctAnswers = "";
}

export class QuestionUpdate {
    questionLevelId = 0;
    questionContent = "";
}

export class MultipleChoiceQuestionWithOneCorrectAnswerUpdate extends QuestionUpdate {
    choiceA = "";
    choiceB = "";
    choiceC = "";
    choiceD = "";
    correctAnswer = "";
}

export class MultipleChoiceQuestionWithMultipleCorrectAnswersUpdate extends QuestionUpdate {
    choiceA = "";
    choiceB = "";
    choiceC = "";
    choiceD = "";
    correctAnswers = [""];
}

export class TrueFalseQuestionUpdate extends QuestionUpdate {
    correctAnswer = "";
}

export class ShortAnswerQuestionUpdate extends QuestionUpdate {
    correctAnswer = "";
}

export class FillInBlankQuestionUpdate extends QuestionUpdate {
    correctAnswers = [""];
}