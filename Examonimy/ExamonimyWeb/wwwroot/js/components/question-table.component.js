import { trimQuestionContentMarkup } from "../helpers/question.helper.js";
import { Question } from "../models/question.model.js";

export class QuestionTableComponent {

    #container;
    #questions = [new Question()];
    #tableBody;

    constructor(container = new HTMLElement(), question = [new Question()]) {
        this.#container = container;
        this.#questions = question;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();
        this.#tableBody = this.#container.querySelector("tbody");
        this.#populateQuestions(this.#tableBody, this.#questions);
    }

    set questions(value = [new Question()]) {
        this.#questions = value;
    }

    #populateQuestions(tableBody = new HTMLElement(), questions = [new Question()]) {
        tableBody.innerHTML = "";
        questions.forEach((q, i) => {
            tableBody.insertAdjacentHTML("beforeend", `
<tr class="">
    <td class="whitespace-nowrap py-4 pl-4 pr-3 text-sm font-medium text-gray-900 sm:pl-6">${i + 1}</td>
    <td class="whitespace-normal px-3 py-4 text-sm text-gray-500">
        <div class="font-medium text-violet-800 text-sm">${trimQuestionContentMarkup(q.questionContent)}</div>
        <div class="mt-1 text-gray-500">Đã tạo bởi <a href="#" class="font-medium text-gray-600">${q.author.fullName}</a></div>
    </td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${q.questionType.name}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${q.questionLevel.name}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${q.course.name}</td>
    <td class="relative whitespace-nowrap py-4 px-3">
        <a href="#" class="flex items-center gap-2 text-green-500 hover:text-green-800">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="w-4 h-4">
                <path d="M21.731 2.269a2.625 2.625 0 00-3.712 0l-1.157 1.157 3.712 3.712 1.157-1.157a2.625 2.625 0 000-3.712zM19.513 8.199l-3.712-3.712-8.4 8.4a5.25 5.25 0 00-1.32 2.214l-.8 2.685a.75.75 0 00.933.933l2.685-.8a5.25 5.25 0 002.214-1.32l8.4-8.4z" />
                <path d="M5.25 5.25a3 3 0 00-3 3v10.5a3 3 0 003 3h10.5a3 3 0 003-3V13.5a.75.75 0 00-1.5 0v5.25a1.5 1.5 0 01-1.5 1.5H5.25a1.5 1.5 0 01-1.5-1.5V8.25a1.5 1.5 0 011.5-1.5h5.25a.75.75 0 000-1.5H5.25z" />
            </svg>
            <span class="text-right text-sm font-medium">Sửa</span>
        </a>
    </td>
    <td class="relative whitespace-nowrap py-4 pr-3 pl-3 sm:pr-6">
        <a href="#" class="flex items-center gap-2 text-red-500 hover:text-red-800">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="w-4 h-4">
                <path fill-rule="evenodd" d="M16.5 4.478v.227a48.816 48.816 0 013.878.512.75.75 0 11-.256 1.478l-.209-.035-1.005 13.07a3 3 0 01-2.991 2.77H8.084a3 3 0 01-2.991-2.77L4.087 6.66l-.209.035a.75.75 0 01-.256-1.478A48.567 48.567 0 017.5 4.705v-.227c0-1.564 1.213-2.9 2.816-2.951a52.662 52.662 0 013.369 0c1.603.051 2.815 1.387 2.815 2.951zm-6.136-1.452a51.196 51.196 0 013.273 0C14.39 3.05 15 3.684 15 4.478v.113a49.488 49.488 0 00-6 0v-.113c0-.794.609-1.428 1.364-1.452zm-.355 5.945a.75.75 0 10-1.5.058l.347 9a.75.75 0 101.499-.058l-.346-9zm5.48.058a.75.75 0 10-1.498-.058l-.347 9a.75.75 0 001.5.058l.345-9z" clip-rule="evenodd" />
            </svg>
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