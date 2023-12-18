import { ExamPaper } from "../models/exam-paper.model.js";

export class ExamPaperComponent {
    #container;
    #examPapers = [new ExamPaper()];
    #tableBody;
    #fromItemNumber = 1;

    constructor(container = new HTMLElement(), examPapers = [new ExamPaper()]) {
        this.#container = container;
        this.#examPapers = examPapers;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();
    }

    

    #render() {
        return `
<table class="min-w-full">
    <thead class="bg-white">
        <tr>
            <th scope="col" class="py-3.5 pl-4 pr-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500 sm:pl-6">STT</th>
            <th scope="col" class="px-3 py-3.5 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Mã đề thi</th>
            <th scope="col" class="px-3 py-3.5 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Môn học</th>
            <th scope="col" class="px-3 py-3.5 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Số câu hỏi</th>
            <th scope="col" class="px-3 py-3.5 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Kiểm duyệt viên</th>
            <th scope="col" class="px-3 py-3.5 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Trạng thái</th>
            <th scope="col" colspan="2" class="relative py-3.5 px-3 text-center text-xs font-medium uppercase tracking-wide text-gray-500">Tùy chọn</th>
        </tr>
    </thead>
    <tbody class="divide-y divide-gray-100 bg-white">
        ${this.#examPapers.reduce((accumulator, examPaper, currentIndex) => {
            return accumulator + `
<tr class="">
    <td class="whitespace-nowrap py-4 pl-4 pr-3 text-sm font-medium text-gray-900 sm:pl-6">${this.#fromItemNumber + currentIndex}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${examPaper.examPaperCode}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${examPaper.course.name}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${examPaper.numbersOfQuestion}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
        <div class="flex -space-x-4 rtl:space-x-reverse">
            <img class="w-10 h-10 border-2 border-white rounded-full dark:border-gray-800" src="/docs/images/people/profile-picture-5.jpg" alt="">
            <img class="w-10 h-10 border-2 border-white rounded-full dark:border-gray-800" src="/docs/images/people/profile-picture-2.jpg" alt="">
            <img class="w-10 h-10 border-2 border-white rounded-full dark:border-gray-800" src="/docs/images/people/profile-picture-3.jpg" alt="">
            <a class="flex items-center justify-center w-10 h-10 text-xs font-medium text-white bg-gray-700 border-2 border-white rounded-full hover:bg-gray-600 dark:border-gray-800" href="#">+99</a>
        </div>
    </td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
        <span class="inline-flex items-center gap-x-1.5 rounded-md bg-yellow-100 px-1.5 py-0.5 text-xs font-medium text-yellow-800">
            <svg class="h-1.5 w-1.5 fill-yellow-500" viewBox="0 0 6 6" aria-hidden="true">
            <circle cx="3" cy="3" r="3" />
            </svg>
            Badge
        </span>
    </td>
    <td class="relative whitespace-nowrap py-4 px-3">
        <a href="/exam-paper/edit/${examPaper.id}" class="flex items-center gap-2 text-green-600 hover:text-green-800">          
            <span class="text-right text-sm font-medium">Sửa</span>
        </a>
    </td>
    <td class="relative whitespace-nowrap py-4 pr-3 pl-3 sm:pr-6">
        <button type="button" data-exam-paper-id="${examPaper.id}" class="delete-btn flex items-center gap-2 text-red-600 hover:text-red-800">         
            <span class="text-right text-sm font-medium">Xóa</span>
        </button>
        <div class="modal-container whitespace-normal"></div>
    </td>
</tr>
            `;
        }, "")}
    </tbody>
</table>
        `;
    }
}