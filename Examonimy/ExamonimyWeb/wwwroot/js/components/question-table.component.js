import { trimQuestionContentMarkup } from "../helpers/question.helper.js";
import { Question } from "../models/question.model.js";

export class QuestionTableComponent {

    #container;
    #questions = [new Question()];
    #tableBody;
    #fromItemNumber = 1;

    constructor(container = new HTMLElement(), question = [new Question()]) {
        this.#container = container;
        this.#questions = question;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();
        this.#tableBody = this.#container.querySelector("tbody");

        if (this.#questions.length === 0) {
            this.#populateEmptyState(this.#tableBody);
        } else {
            this.#populateQuestions(this.#tableBody, this.#questions);
        }
    }

    set questions(value = [new Question()]) {
        this.#questions = value;
    }

    get questions() {
        return this.#questions;
    }

    set fromItemNumber(value = 0) {
        this.#fromItemNumber = value;
    }

    #populateEmptyState(tableBody = new HTMLElement()) {
        tableBody.innerHTML = `
<tr>
    <td colspan="6">
        <div class="text-center py-12">
          <svg class="mx-auto h-12 w-12 text-gray-400" fill="none" viewBox="0 0 24 24" stroke="currentColor" aria-hidden="true">
            <path vector-effect="non-scaling-stroke" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 13h6m-3-3v6m-9 1V7a2 2 0 012-2h6l2 2h6a2 2 0 012 2v8a2 2 0 01-2 2H5a2 2 0 01-2-2z" />
          </svg>
          <h3 class="mt-2 text-sm font-semibold text-gray-900">Không có câu hỏi nào</h3>               
        </div>
    </td>
</tr>
        `;
    }

    #populateQuestions(tableBody = new HTMLElement(), questions = [new Question()]) {
        tableBody.innerHTML = "";
        questions.forEach((q, i) => {
            tableBody.insertAdjacentHTML("beforeend", `
<tr class="">
    <td class="whitespace-nowrap py-4 pl-4 pr-3 text-sm font-medium text-gray-900 sm:pl-6">${this.#fromItemNumber + i}</td>
    <td class="whitespace-normal px-3 py-4 text-sm text-gray-500">
        <div class="font-medium text-violet-800 text-sm">${trimQuestionContentMarkup(q.questionContent)}</div>
        <div class="mt-1 text-gray-500">Đã tạo bởi <a href="#" class="font-medium text-gray-600">${q.author.fullName}</a></div>
    </td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${q.questionType.name}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${q.questionLevel.name}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${q.course.name}</td>
    <td class="relative whitespace-nowrap py-4 px-3">
        <a href="#" class="flex items-center gap-2 text-green-600 hover:text-green-800">          
            <span class="text-right text-sm font-medium">Sửa</span>
        </a>
    </td>
    <td class="relative whitespace-nowrap py-4 pr-3 pl-3 sm:pr-6">
        <a href="#" class="flex items-center gap-2 text-red-600 hover:text-red-800">         
            <span class="text-right text-sm font-medium">Xóa</span>
        </a>
    </td>
</tr>
            `);
        });
    }

    #render() { 
        return `
<table class="min-w-full">
    <thead class="bg-white">
        <tr>
            <th scope="col" class="py-3.5 pl-4 pr-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500 sm:pl-6">STT</th>
            <th scope="col" class="px-3 py-3.5 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Câu hỏi</th>
            <th scope="col" class="px-3 py-3.5 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Dạng</th>
            <th scope="col" class="px-3 py-3.5 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Mức độ</th>
            <th scope="col" class="px-3 py-3.5 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Môn học</th>
            <th scope="col" colspan="2" class="relative py-3.5 px-3 text-center text-xs font-medium uppercase tracking-wide text-gray-500">Tùy chọn</th>
        </tr>
    </thead>
    <tbody class="divide-y divide-gray-100 bg-white">
        
    </tbody>
</table>
        `;
    }
}