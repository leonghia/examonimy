﻿import { QuestionTypeIDs, formatFillInBlankQuestionContent, renderAnswerSheetForFillInBlankQuestion, countNumbersOfBlank } from "../helpers/question.helper.js";
import { FillInBlankQuestion, MultipleChoiceQuestionWithMultipleCorrectAnswers, MultipleChoiceQuestionWithOneCorrectAnswer, Question, ShortAnswerQuestion, TrueFalseQuestion } from "../models/question.model.js";
export class QuestionPreviewComponent {
    
    #question = new Question();
    #container;

    constructor(container, question) {
        this.#container = container;     
        this.#question = question;
    }

    connectedCallback() {
        this.#container.innerHTML = this.render();
    }

    render() {
        switch (this.#question?.questionType.id) {
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
            default:
                return this.#renderEmptyQuestion();
        }
    }

    #renderEmptyQuestion() {
        return `
<div class="empty-placeholder relative block w-full rounded-lg border-2 border-dashed border-gray-300 p-12 text-center hover:border-gray-400 focus:outline-none focus:ring-0 focus:ring-indigo-500 focus:ring-offset-2">
    <svg class="empty-icon pointer-events-none mx-auto h-12 w-12 text-gray-400" stroke="currentColor" fill="none" viewBox="0 0 48 48" aria-hidden="true">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 14v20c0 4.418 7.163 8 16 8 1.381 0 2.721-.087 4-.252M8 14c0 4.418 7.163 8 16 8s16-3.582 16-8M8 14c0-4.418 7.163-8 16-8s16 3.582 16 8m0 0v14m0-4c0 4.418-7.163 8-16 8S8 28.418 8 24m32 10v6m0 0v6m0-6h6m-6 0h-6" />
    </svg>
    <span class="empty-text pointer-events-none mt-2 block text-sm font-semibold text-gray-400">Chưa có nội dung</span>
</div>
        `;
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

    <div class="answer-container hidden">
        <p class="text-gray-700 font-medium">Đáp án</p>
        <div>
            <span class="flex h-8 w-8 flex-shrink-0 items-center justify-center rounded-full bg-green-600">
                <span class="text-white font-semibold">${question.correctAnswer}</span>
            </span>
        </div>
    </div>
</div>
        `;
    }

    #renderPreviewForMultipleChoiceQuestionWithMultipleCorrectAnswers(question = new MultipleChoiceQuestionWithMultipleCorrectAnswers()) {
        return `
<div class="prose prose-sm max-w-none">
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
    <div class="answer-container hidden">
        <p class="text-gray-700 font-medium">Đáp án</p>
        <div class="flex items-center gap-x-2">
            ${question.correctAnswers.reduce((accumulator, currentValue) => {
                return accumulator + `
                <span class="flex h-8 w-8 flex-shrink-0 items-center justify-center rounded bg-green-600">
                    <span class="text-white font-semibold">${currentValue}</span>
                </span>
                `;
            }, "")}
        </div>
    </div>
</div>
        `;
    }

    #renderPreviewForTrueFalseQuestion(question = new TrueFalseQuestion()) {
        return `
<div class="prose prose-sm max-w-none">
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
    <div class="answer-container hidden">
        <p class="text-gray-700 font-medium">Đáp án</p>
        <div>
            <span class="flex h-8 w-8 flex-shrink-0 items-center justify-center rounded-full bg-green-600">
                <span class="text-white font-semibold">${question.correctAnswer}</span>
            </span>
        </div>
    </div>
</div>
        `;
    }

    #renderPreviewForShortAnswerQuestion(question = new ShortAnswerQuestion()) {
        return `
<div class="prose prose-sm max-w-none">
    <div class="font-medium text-gray-700">
        ${question.questionContent}
    </div>
    <div class="mt-6">      
        <div class="">
            <textarea rows="4" name="comment" id="comment" class="pointer-events-none block w-full bg-gray-100 rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-0 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-0 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" placeholder="Sinh viên nhập câu trả lời vào đây"></textarea>
        </div>
    </div>
    <div class="answer-container hidden">
        <p class="text-gray-700 font-medium">Đáp án</p>
        <div class="">
            ${question.correctAnswer}
        </div>
    </div>
</div>
        `;
    } 

    #rendePreviewForFillInBlankQuestion(question = new FillInBlankQuestion()) {
        return `
<div class="prose prose-sm max-w-none">
    <div class="font-medium text-gray-700 leading-8">
        ${formatFillInBlankQuestionContent(question.questionContent)}
    </div>
    
    <div class="space-y-6">
        ${renderAnswerSheetForFillInBlankQuestion(countNumbersOfBlank(question.questionContent))}
    </div>

    <div class="answer-container hidden">
        <p class="text-gray-700 font-medium">Đáp án</p>
        <div class="space-y-4">
            ${question.correctAnswers.reduce((accumulator, currentValue, currentIndex) => {
                return accumulator + `
                <div class="relative flex items-start">
                <div class="leading-6 flex">
                    <span class="mr-2 inline-flex h-7 w-7 flex-shrink-0 items-center justify-center rounded-full bg-green-600">
                       <span class="text-white font-semibold text-xs">${currentIndex + 1}</span>
                    </span>
                    <div class="prose-p:m-0">${currentValue}</div>
                </div>
            </div>
                `;
            }, "")}
        </div>
    </div>
</div>
        `;
    }
}