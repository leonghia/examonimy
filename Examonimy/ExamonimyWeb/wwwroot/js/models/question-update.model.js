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