import { QuestionTypeIDs, countNumbersOfBlank, renderAnswerSheetForFillInBlankQuestion, formatFillInBlankQuestionContent } from "../helpers/question.helper.js";
import { FillInBlankQuestion, MultipleChoiceQuestionWithMultipleCorrectAnswers, MultipleChoiceQuestionWithOneCorrectAnswer, Question, ShortAnswerQuestion, TrueFalseQuestion } from "../models/question.model.js";

export class QuestionSampleComponent {
    #question = new Question();
    #container;

    constructor(container = new HTMLElement(), question = new Question()) {
        this.#container = container;
        this.#question = question;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();
    }

    #render() {
        switch (this.#question.questionType.id) {
            case QuestionTypeIDs.MultipleChoiceWithOneCorrectAnswer:
                return this.#renderSampleForMultipleChoiceQuestionWithOneCorrectAnswer(this.#question);
            case QuestionTypeIDs.MultipleChoiceWithMultipleCorrectAnswers:
                return this.#renderSampleForMultipleChoiceQuestionWithMultipleCorrectAnswers(this.#question);           
            case QuestionTypeIDs.TrueFalse:
                return this.#renderSampleForTrueFalseQuestion(this.#question);
            case QuestionTypeIDs.ShortAnswer:
                return this.#renderSampleForShortAnswerQuestion(this.#question);
            case QuestionTypeIDs.FillInBlank:
                return this.#renderSampleForFillInBlankQuestion(this.#question);
        }
    }

    set question(value = new Question()) {
        this.#question = value;
    }

    #renderSampleForMultipleChoiceQuestionWithOneCorrectAnswer(question = new MultipleChoiceQuestionWithOneCorrectAnswer()) {
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
</div>
        `;
    }

    #renderSampleForMultipleChoiceQuestionWithMultipleCorrectAnswers(question = new MultipleChoiceQuestionWithMultipleCorrectAnswers()) {
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
</div>
        `;
    }

    #renderSampleForTrueFalseQuestion(question = new TrueFalseQuestion()) {
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
</div>
        `;
    }

    #renderSampleForShortAnswerQuestion(question = new ShortAnswerQuestion()) {
        return `
<div class="">
    <div class="prose prose-sm max-w-none font-medium text-gray-700">
        ${question.questionContent}
    </div>

    <div class="mt-6">      
        <div class="">
            <textarea rows="4" name="comment" id="comment" class="pointer-events-none block w-full bg-gray-100 rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-0 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-0 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" placeholder="Nhập câu trả lời của bạn vào đây"></textarea>
        </div>
    </div>
</div>
        `;
    }

    #renderSampleForFillInBlankQuestion(question = FillInBlankQuestion()) {
        return `
<div class="">
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