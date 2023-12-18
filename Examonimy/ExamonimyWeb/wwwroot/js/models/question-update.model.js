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