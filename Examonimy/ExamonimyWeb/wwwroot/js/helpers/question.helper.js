import { MultipleChoiceQuestionWithOneCorrectAnswerCreateDto, MultipleChoiceQuestionWithMultipleCorrectAnswersCreateDto, TrueFalseQuestionCreateDto, ShortAnswerQuestionCreateDto, FillInBlankQuestionCreateDto } from "../dtos/question-create.dto.js";

export const QuestionTypeIDs = {
    MultipleChoiceWithOneCorrectAnswer: 1,
    MultipleChoiceWithMultipleCorrectAnswers: 2,
    TrueFalse: 3,
    ShortAnswer: 4,
    FillInBlank: 5
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