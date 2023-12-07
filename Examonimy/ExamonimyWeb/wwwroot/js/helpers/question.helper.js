import { MultipleChoiceQuestionWithOneCorrectAnswerCreateDto, MultipleChoiceQuestionWithMultipleCorrectAnswersCreateDto, TrueFalseQuestionCreateDto, ShortAnswerQuestionCreateDto, FillInBlankQuestionCreateDto } from "../dtos/question-create.dto.js";

export const QuestionTypeIDs = {
    MultipleChoiceWithOneCorrectAnswer: 1,
    MultipleChoiceWithMultipleCorrectAnswers: 2,
    TrueFalse: 3,
    ShortAnswer: 4,
    FillInBlank: 5
}

export const QuestionTypeIdQuestionCreationEndpointMappings = {
    1: "question/multiplechoicewithonecorrectanswer",
    2: "question/multiplechoicewithmultiplecorrectanswers",
    3: "question/truefalse",
    4: "question/shortanswer",
    5: "question/fillinblank"
}

export const QuestionLevelIDs = {
    Perception: 1,
    Understand: 2,
    Apply: 3,
    Analyze: 4
}

export const QuestionTypeIdQuestionCreateDtoConstructorMappings = {
    1: new MultipleChoiceQuestionWithOneCorrectAnswerCreateDto(),
    2: new MultipleChoiceQuestionWithMultipleCorrectAnswersCreateDto(),
    3: new TrueFalseQuestionCreateDto(),
    4: new ShortAnswerQuestionCreateDto(),
    5: new FillInBlankQuestionCreateDto()
}

export const ChoiceValueMappings = {
    0: "A",
    1: "B",
    2: "C",
    3: "D"
}