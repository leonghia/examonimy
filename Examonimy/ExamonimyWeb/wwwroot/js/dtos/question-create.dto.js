export class QuestionCreateDto {
    courseId = 0;
    questionTypeId = 0;
    questionLevelId = 0;
    questionContent = "";
    authorId = 0;
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