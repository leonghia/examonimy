import { ChoiceValueMappings, QuestionTypeIDs } from "../helpers/question.helper.js";
import { MultipleChoiceQuestionWithMultipleCorrectAnswers, MultipleChoiceQuestionWithOneCorrectAnswer, Question, ShortAnswerQuestion, TrueFalseQuestion } from "../models/question.model.js";
export class QuestionPreviewComponent {
    
    #question = new Question();
    #container;

    constructor(container) {
        this.#container = container;        
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();
    }

    #render() {
        switch (this.#question.questionType.id) {
            case QuestionTypeIDs.MultipleChoiceWithOneCorrectAnswer:
                return this.#renderPreviewForMultipleChoiceQuestionWithOneCorrectAnswer(this.#question);
            case QuestionTypeIDs.MultipleChoiceWithMultipleCorrectAnswers:
                return this.#renderPreviewForMultipleChoiceQuestionWithMultipleCorrectAnswers(this.#question);
            case QuestionTypeIDs.ShortAnswer:
                return this.#renderPreviewForShortAnswerQuestion(this.#question);
            case QuestionTypeIDs.TrueFalse:
                return this.#renderPreviewForTrueFalseQuestion(this.#question);
            case QuestionTypeIDs.FillInBlank:
                return this.#rendePreviewForFillInBlankQuestion(this.#question);
        }
    }

    set question(value = new Question()) {
        this.#question = value;
    }

    #renderPreviewForMultipleChoiceQuestionWithOneCorrectAnswer(question = new MultipleChoiceQuestionWithOneCorrectAnswer()) {
        return `
<div class="prose prose-sm max-w-none">
    <div class="font-medium text-gray-700">
        ${question.questionContent}
    </div>
    <fieldset class="">
        <legend class="sr-only">Phương án</legend>
        <div class="space-y-5">
            <div class="relative flex items-start">
                <div class="flex h-6 items-center">
                    <input name="choices" type="radio" class="pointer-events-none h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
                </div>
                <div class="ml-3 leading-6 flex">
                    <label class="font-medium text-gray-900 mr-1">A.</label>
                    <div class="prose-p:m-0">${question.choiceA}</div>
                </div>
            </div>
            <div class="relative flex items-start">
                <div class="flex h-6 items-center">
                    <input name="choices" type="radio" class="pointer-events-none h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
                </div>
                <div class="ml-3 leading-6 flex">
                    <label class="font-medium text-gray-900 mr-1">B.</label>
                    <div class="prose-p:m-0">${question.choiceB}</div>
                </div>
            </div>
            <div class="relative flex items-start">
                <div class="flex h-6 items-center">
                    <input name="choices" type="radio" class="pointer-events-none h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
                </div>
                <div class="ml-3 leading-6 flex">
                    <label class="font-medium text-gray-900 mr-1">C.</label>
                    <div class="prose-p:m-0">${question.choiceC}</div>
                </div>
            </div>
            <div class="relative flex items-start">
                <div class="flex h-6 items-center">
                    <input name="choices" type="radio" class="pointer-events-none h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
                </div>
                <div class="ml-3 leading-6 flex">
                    <label class="font-medium text-gray-900 mr-1">D.</label>
                    <div class="prose-p:m-0">${question.choiceD}</div>
                </div>
            </div>
        </div>
    </fieldset>

    <p class="text-gray-700 font-medium">Đáp án</p>
    <div>
        <span class="flex h-8 w-8 flex-shrink-0 items-center justify-center rounded-full bg-green-600">
            <span class="text-white font-semibold">${ChoiceValueMappings[question.correctAnswer]}</span>
        </span>
    </div>
</div>
        `;
    }

    #renderPreviewForMultipleChoiceQuestionWithMultipleCorrectAnswers(question = new MultipleChoiceQuestionWithMultipleCorrectAnswers()) {
        return `

        `;
    }

    #renderPreviewForShortAnswerQuestion(question = new ShortAnswerQuestion()) {
        return `

        `;
    }

    #renderPreviewForTrueFalseQuestion(question = new TrueFalseQuestion()) {
        return `

        `;
    }

    #rendePreviewForFillInBlankQuestion(question = new Question()) {
        return `

        `;
    }
}