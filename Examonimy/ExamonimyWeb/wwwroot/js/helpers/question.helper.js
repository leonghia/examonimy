import { MultipleChoiceQuestionWithOneCorrectAnswerCreateDto, MultipleChoiceQuestionWithMultipleCorrectAnswersCreateDto, TrueFalseQuestionCreateDto, ShortAnswerQuestionCreateDto, FillInBlankQuestionCreateDto } from "../models/question-create.model.js";

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

export const splitCorrectAnswersForMultipleChoiceQuestion = (correctAnswers = "") => correctAnswers.split("|").map(str => Number(str)).sort((a, b) => a - b);

export const splitCorrectAnswersForFillInBlankQuestion = (correctAnswers = "") => correctAnswers.split("|");

export const formatFillInBlankQuestionContent = (content = "") => {
    let i = 0;
    return content.replaceAll("__", () => {
        return `
<span class="mx-1 inline-flex h-6 w-6 flex-shrink-0 items-center justify-center rounded-full bg-gray-700">
    <span class="text-white font-semibold text-xs">${++i}</span>
</span>
<span class="mr-2 inline-flex w-20 border-b-2 border-gray-300"></span>
        `;
    });
}