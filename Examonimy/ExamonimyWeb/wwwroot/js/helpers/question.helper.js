import { MultipleChoiceQuestionWithOneCorrectAnswerDto, MultipleChoiceQuestionWithMultipleCorrectAnswersCreateDto, TrueFalseQuestionCreateDto, ShortAnswerQuestionCreateDto, FillInBlankQuestionCreateDto } from "../dtos/question-create.dto.js";

export const QuestionTypes = {
    multipleChoiceWithOneCorrectAnswer: 1,
    multipleChoiceWithMultipleCorrectAnswers: 2,
    shortAnswer: 3,
    trueFalse: 4,
    fillInBlank: 5
}

export const QuestionLevels = {
    perception: 1,
    understand: 2,
    apply: 3,
    analyze: 4
}

export const QuestionCreateDtoConstructorMappings = {
    1: new MultipleChoiceQuestionWithOneCorrectAnswerDto(),
    2: new MultipleChoiceQuestionWithMultipleCorrectAnswersCreateDto(),
    3: new TrueFalseQuestionCreateDto(),
    4: new ShortAnswerQuestionCreateDto(),
    5: new FillInBlankQuestionCreateDto()
}