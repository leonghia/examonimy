const renderSingleChoiceQuestionForCreation = () => {
    console.log("render single choice question");
}

const renderMultipleChoiceQuestionForCreation = () => {
    console.log("render multiple choice question");
}

const renderEssayQuestionForCreation = () => {
    console.log("render essay question");
}

const renderTrueFalseQuestionForCreation = () => {
    console.log("render true false question");
}

const renderFillInBlankQuestionForCreation = () => {
    console.log("render fill in blank question");
}

export const QuestionTypeRenderers = {
    singleChoice: renderSingleChoiceQuestionForCreation,
    multipleChoice: renderMultipleChoiceQuestionForCreation,
    essay: renderEssayQuestionForCreation,
    trueFalse: renderTrueFalseQuestionForCreation,
    fillInBlank: renderFillInBlankQuestionForCreation
}