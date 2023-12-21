import { QuestionTypeIDs, countNumbersOfBlank, renderAnswerSheetForFillInBlankQuestion, formatFillInBlankQuestionContent } from "../helpers/question.helper.js";
import { FillInBlankQuestion, MultipleChoiceQuestionWithMultipleCorrectAnswers, MultipleChoiceQuestionWithOneCorrectAnswer, Question, ShortAnswerQuestion, TrueFalseQuestion } from "../models/question.model.js";
import { BaseComponent } from "./base.component.js";

export class QuestionPreviewComponent extends BaseComponent {
    #question = new Question();
    #container;
    #title = "";
    #isDeleteButtonNeeded = false;   

    constructor(container = new HTMLElement(), question = new Question(), title = null, isDeleteButtonNeeded = false) {
        super();
        this.#container = container;
        this.#question = question;
        this.#title = title;
        this.#isDeleteButtonNeeded = isDeleteButtonNeeded;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();       
    }

    disconnectedCallback() {
        this.#container.remove();
    }

    set title(value) {
        this.#title = value;
    }

    populateTitle() {
        this.#container.querySelector(".title").textContent = this.#title;
    }

    #render() {
        const temp = `
        <div class="mb-6 flex items-center justify-between">
            ${this.#title ? `<p class="title text-base font-bold text-gray-900">${this.#title}</p>` : ""}
            ${this.#isDeleteButtonNeeded ? `
            <button type="button" title="Gỡ" data-question-id="${this.#question?.id}" class="delete-btn rounded p-2 bg-red-50 text-red-600 hover:bg-red-100 hover:text-red-700">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" data-slot="icon" class="w-4 h-4">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M6 18 18 6M6 6l12 12" />
                </svg>
            </button>
            ` : ""}
        </div>
        `;
        if (!this.#question) {
            return temp.concat(`
            <div class="empty-placeholder relative block w-full rounded-lg border-2 border-dashed border-gray-300 p-12 text-center hover:border-gray-400 focus:outline-none focus:ring-0 focus:ring-indigo-500 focus:ring-offset-2">
                <svg class="empty-icon pointer-events-none mx-auto h-12 w-12 text-gray-400" stroke="currentColor" fill="none" viewBox="0 0 48 48" aria-hidden="true">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 14v20c0 4.418 7.163 8 16 8 1.381 0 2.721-.087 4-.252M8 14c0 4.418 7.163 8 16 8s16-3.582 16-8M8 14c0-4.418 7.163-8 16-8s16 3.582 16 8m0 0v14m0-4c0 4.418-7.163 8-16 8S8 28.418 8 24m32 10v6m0 0v6m0-6h6m-6 0h-6"></path>
                </svg>
                <span class="empty-text pointer-events-none mt-2 block text-sm font-semibold text-gray-400">Chưa có nội dung</span>
            </div>
            `);
        }
        switch (this.#question.questionType.id) {
            case QuestionTypeIDs.MultipleChoiceWithOneCorrectAnswer:
                return temp.concat(this.#renderSampleForMultipleChoiceQuestionWithOneCorrectAnswer(this.#question));
            case QuestionTypeIDs.MultipleChoiceWithMultipleCorrectAnswers:
                return temp.concat(this.#renderSampleForMultipleChoiceQuestionWithMultipleCorrectAnswers(this.#question));           
            case QuestionTypeIDs.TrueFalse:
                return temp.concat(this.#renderSampleForTrueFalseQuestion(this.#question));
            case QuestionTypeIDs.ShortAnswer:
                return temp.concat(this.#renderSampleForShortAnswerQuestion(this.#question));
            case QuestionTypeIDs.FillInBlank:
                return temp.concat(this.#renderSampleForFillInBlankQuestion(this.#question));
        }
    }

    set question(value = new Question()) {
        this.#question = value;
    }

    #renderSampleForMultipleChoiceQuestionWithOneCorrectAnswer(question = new MultipleChoiceQuestionWithOneCorrectAnswer()) {
        return `
<div class="question-sample prose prose-sm max-w-none" data-question-id="${question.id}">
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
</div>
        `;
    }

    #renderSampleForMultipleChoiceQuestionWithMultipleCorrectAnswers(question = new MultipleChoiceQuestionWithMultipleCorrectAnswers()) {
        return `
<div class="question-sample prose prose-sm max-w-none" data-question-id="${question.id}">
    <div class="font-medium text-gray-700">
        ${question.questionContent}
    </div>

    <fieldset class="mt-4">
        <legend class="sr-only">Phương án</legend>
        <div class="space-y-5">
            <div class="relative flex items-start">
                <div class="flex h-6 items-center">
                    <input name="choices" type="checkbox" class="pointer-events-none rounded h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
                </div>
                <div class="ml-3 leading-6 flex">
                    <label class="font-medium text-gray-900 mr-1">A.</label>
                    <div class="prose-p:m-0">${question.choiceA}</div>
                </div>
            </div>
            <div class="relative flex items-start">
                <div class="flex h-6 items-center">
                    <input name="choices" type="checkbox" class="pointer-events-none rounded h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
                </div>
                <div class="ml-3 leading-6 flex">
                    <label class="font-medium text-gray-900 mr-1">B.</label>
                    <div class="prose-p:m-0">${question.choiceB}</div>
                </div>
            </div>
            <div class="relative flex items-start">
                <div class="flex h-6 items-center">
                    <input name="choices" type="checkbox" class="pointer-events-none rounded h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
                </div>
                <div class="ml-3 leading-6 flex">
                    <label class="font-medium text-gray-900 mr-1">C.</label>
                    <div class="prose-p:m-0">${question.choiceC}</div>
                </div>
            </div>
            <div class="relative flex items-start">
                <div class="flex h-6 items-center">
                    <input name="choices" type="checkbox" class="pointer-events-none rounded h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
                </div>
                <div class="ml-3 leading-6 flex">
                    <label class="font-medium text-gray-900 mr-1">D.</label>
                    <div class="prose-p:m-0">${question.choiceD}</div>
                </div>
            </div>
        </div>
    </fieldset>
</div>
        `;
    }

    #renderSampleForTrueFalseQuestion(question = new TrueFalseQuestion()) {
        return `
<div class="question-sample prose prose-sm max-w-none" data-question-id="${question.id}">
    <div class="font-medium text-gray-700">
        ${question.questionContent}
    </div>
    <fieldset class="mt-4">
        <legend class="sr-only">Phương án</legend>
        <div class="space-y-4">
            <div class="relative flex items-start">
                <div class="flex h-6 items-center">
                    <input name="choices" type="radio" class="pointer-events-none h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
                </div>
                <div class="ml-3 leading-6 flex">
                    <label class="mr-1">Đúng</label>           
                </div>
            </div>
            <div class="relative flex items-start">
                <div class="flex h-6 items-center">
                    <input name="choices" type="radio" class="pointer-events-none h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
                </div>
                <div class="ml-3 leading-6 flex">
                    <label class="mr-1">Sai</label>             
                </div>
            </div>
        </div>
    </fieldset>
</div>
        `;
    }

    #renderSampleForShortAnswerQuestion(question = new ShortAnswerQuestion()) {
        return `
<div class="question-sample" data-question-id="${question.id}">
    <div class="prose prose-sm max-w-none font-medium text-gray-700">
        ${question.questionContent}
    </div>

    <div class="mt-6">      
        <div class="">
            <textarea rows="4" name="comment" id="comment" class="pointer-events-none block w-full bg-gray-100 rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-0 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-0 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" placeholder="Sinh viên nhập câu trả lời vào đây"></textarea>
        </div>
    </div>
</div>
        `;
    }

    #renderSampleForFillInBlankQuestion(question = FillInBlankQuestion()) {
        return `
<div class="question-sample" data-question-id="${question.id}">
    <div class="prose prose-sm max-w-none font-medium text-gray-700 leading-8 mb-8">
        ${formatFillInBlankQuestionContent(question.questionContent)}
    </div>
    
    <div class="space-y-6">
        ${renderAnswerSheetForFillInBlankQuestion(countNumbersOfBlank(question.questionContent))}
    </div>
</div>
        `;
    }
}