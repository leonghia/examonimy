import { ExamPaper } from "../models/exam-paper.model.js";

export class ExamPaperTableComponent {
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
            <th scope="col" class="px-3 py-3.5 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Tác giả</th>
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
    <td class="whitespace-nowrap px-3 py-4 text-sm font-semibold text-violet-700 hover:text-violet-600">
        <a href="/exam-paper/${examPaper.id}">${examPaper.examPaperCode}</a>
    </td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${examPaper.course.name}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${examPaper.numbersOfQuestion}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${examPaper.author.fullName}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
        <div class="flex -space-x-4 rtl:space-x-reverse">
            <img class="w-10 h-10 border-2 border-white rounded-full dark:border-gray-800 shadow-sm" src="https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg" alt="">
            <img class="w-10 h-10 border-2 border-white rounded-full dark:border-gray-800 shadow-sm" src="https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg" alt="">
            <img class="w-10 h-10 border-2 border-white rounded-full dark:border-gray-800 shadow-sm" src="https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg" alt="">
            <button type="button" class="flex items-center justify-center w-10 h-10 text-xs font-medium text-white bg-gray-700 border-2 border-white rounded-full hover:bg-gray-600 dark:border-gray-800" title="Thêm kiểm duyệt viên">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" data-slot="icon" class="w-4 h-4 text-white">
                    <path fill-rule="evenodd" d="M12 3.75a.75.75 0 0 1 .75.75v6.75h6.75a.75.75 0 0 1 0 1.5h-6.75v6.75a.75.75 0 0 1-1.5 0v-6.75H4.5a.75.75 0 0 1 0-1.5h6.75V4.5a.75.75 0 0 1 .75-.75Z" clip-rule="evenodd" />
                </svg>
            </button>
        </div>
    </td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
        <span class="inline-flex items-center gap-x-1.5 rounded-md bg-yellow-100 px-2 py-1 text-xs font-medium text-yellow-800">
            <svg class="h-1.5 w-1.5 fill-yellow-500" viewBox="0 0 6 6" aria-hidden="true">
                <circle cx="3" cy="3" r="3" />
            </svg>
            Chờ duyệt
        </span>
    </td>
    <td class="relative whitespace-nowrap py-4 px-3 text-center">
        <a href="/exam-paper/edit/${examPaper.id}" class="text-green-600 hover:text-green-800">          
            <span class="text-right text-sm font-medium">Sửa</span>
        </a>
    </td>
    <td class="relative whitespace-nowrap py-4 pr-3 pl-3 sm:pr-6 text-center">
        <button type="button" data-exam-paper-id="${examPaper.id}" class="delete-btn text-red-600 hover:text-red-800">         
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