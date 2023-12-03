import { QuestionTypes } from "../helpers/question.helper.js";
import { Question } from "../models/question.model.js";

export class QuestionPreviewComponent {

    #questionType = 0;

    constructor(questionType) {
        this.#questionType = questionType;
    }

    render(question = new Question()) {
        switch (this.#questionType) {
            case QuestionTypes.singleChoice:
                return this.#renderSingleChoiceQuestionPreview(question);
            case QuestionTypes.multipleChoice:
                return this.#renderMultipleChoiceQuestionPreview(question);
            case QuestionTypes.essay:
                return this.#renderEssayQuestionPreview(question);
            case QuestionTypes.trueFalse:
                return this.#renderTrueFalseQuestionPreview(question);
            case QuestionTypes.fillInBlank:
                return this.#renderFillInBlankQuestionPreview(question);
        }
    }

    #renderSingleChoiceQuestionPreview(question = new Question()) {
        return ``;
    }

    #renderMultipleChoiceQuestionPreview(question = new Question()) {
        return ``;
    }

    #renderEssayQuestionPreview(question = new Question()) {
        return ``;
    }

    #renderTrueFalseQuestionPreview(question = new Question()) {
        return ``;
    }

    #renderFillInBlankQuestionPreview(question = new Question()) {
        return ``;
    }
}