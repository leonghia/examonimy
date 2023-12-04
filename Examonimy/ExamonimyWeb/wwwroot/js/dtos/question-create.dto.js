export class QuestionCreateDto {
    courseId = 0;
    questionTypeId = 0;
    questionLevelId = 0;
    questionContent = "";
    authorId = 0;
}

export class MultipleChoiceQuestionWithOneCorrectAnswerDto extends QuestionCreateDto {
    choiceA = "";
    choiceB = "";
    choiceC = "";
    choiceD = "";
    correctAnswer = 0;
}

export class MultipleChoiceQuestionWithMultipleCorrectAnswersCreateDto extends QuestionCreateDto {
    choiceA = "";
    choiceB = "";
    choiceC = "";
    choiceD = "";
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