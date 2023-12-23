import { ExamPaperStatusBadgeBackgroundColorMappings, ExamPaperStatusBadgeFillColorMappings, ExamPaperStatusBadgeTextColorMappings } from "../helpers/exam-paper.helper.js";
import { ExamPaper } from "../models/exam-paper.model.js";
import { ConfirmModalComponent } from "../components/confirm-modal.component.js";
import { TeacherStackedListComponent } from "./teacher-stacked-list.component.js";

export class ExamPaperTableComponent {
    #container;
    #examPapers = [new ExamPaper()];
    #tableBody;
    #fromItemNumber = 1;
    #modalComponent;
    #teacherStackedListComponent = new TeacherStackedListComponent(null);

    constructor(container = new HTMLElement(), examPapers = [new ExamPaper()]) {
        this.#container = container;
        this.#examPapers = examPapers;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();
        this.#tableBody = this.#container.querySelector("tbody");

        this.#container.addEventListener("click", event => {          
            const clickedDeleteButton = event.target.closest(".delete-btn");
            if (clickedDeleteButton) {
                const modalContainer = clickedDeleteButton.parentElement.querySelector(".modal-container");
                const examPaperId = Number(clickedDeleteButton.dataset.examPaperId);
                this.#modalComponent = new ConfirmModalComponent(modalContainer, {
                    title: "Xóa đề thi",
                    description: "Bạn có chắc chắn muốn xóa đề thi này? Đề thi sau khi bị xóa sẽ không thể khôi phục lại.",
                    ctaText: "Xác nhận"
                });
                this.#modalComponent.connectedCallback();
                this.#modalComponent.subscribe("confirm", async () => {
                    try {
                        await deleteData("exam-paper", examPaperId);
                        document.location.reload();
                    } catch (err) {
                        console.error(err);
                    }
                });
                this.#modalComponent.subscribe("cancel", () => {
                    this.#modalComponent?.disconnectedCallback();
                    this.#modalComponent = null;
                });

                return;
            }

            const clickedAddReviewerButton = event.target.closest(".add-reviewer-btn");
            if (clickedAddReviewerButton) {
                this.#teacherStackedListComponent = new TeacherStackedListComponent(clickedAddReviewerButton.parentElement.querySelector(".teacher-stacked-list-container"));
                this.#teacherStackedListComponent.connectedCallback();
                this.#teacherStackedListComponent.subscribe("close", () => {
                    this.#teacherStackedListComponent = undefined;
                });
            }
        });
    }

    populateTableBody() {
        this.#tableBody.innerHTML = this.#examPapers.length > 0 ? this.#renderExamPapers() : this.#renderEmptyState();
    }

    set examPapers(value) {
        this.#examPapers = value;
    }

    #renderEmptyState() {
        return `
<tr>
    <td colspan="8">
        <div class="text-center py-12">
          <svg class="mx-auto h-12 w-12 text-gray-400" fill="none" viewBox="0 0 24 24" stroke="currentColor" aria-hidden="true">
            <path vector-effect="non-scaling-stroke" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 13h6m-3-3v6m-9 1V7a2 2 0 012-2h6l2 2h6a2 2 0 012 2v8a2 2 0 01-2 2H5a2 2 0 01-2-2z" />
          </svg>
          <h3 class="mt-2 text-sm font-semibold text-gray-900">Không có đề thi nào</h3>               
        </div>
    </td>
</tr>
        `;
    }

    #renderExamPapers() {
        return this.#examPapers.reduce((accumulator, examPaper, currentIndex) => {
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
            <!-- <img class="w-10 h-10 border-2 border-white rounded-full dark:border-gray-800 shadow-sm" src="https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg" alt="">
            <img class="w-10 h-10 border-2 border-white rounded-full dark:border-gray-800 shadow-sm" src="https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg" alt="">
            <img class="w-10 h-10 border-2 border-white rounded-full dark:border-gray-800 shadow-sm" src="https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg" alt=""> -->
            <button type="button" class="add-reviewer-btn flex items-center justify-center w-10 h-10 text-xs font-medium text-gray-800 bg-gray-200 border-2 border-white rounded-full hover:text-gray-900 hover:bg-gray-300 dark:border-gray-800" title="Thêm kiểm duyệt viên">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" data-slot="icon" class="w-4 h-4">
                    <path fill-rule="evenodd" d="M12 3.75a.75.75 0 0 1 .75.75v6.75h6.75a.75.75 0 0 1 0 1.5h-6.75v6.75a.75.75 0 0 1-1.5 0v-6.75H4.5a.75.75 0 0 1 0-1.5h6.75V4.5a.75.75 0 0 1 .75-.75Z" clip-rule="evenodd" />
                </svg>
            </button>
            <div class="teacher-stacked-list-container"></div>
        </div>
    </td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
        <span class="inline-flex items-center gap-x-1.5 rounded-md ${ExamPaperStatusBadgeBackgroundColorMappings[examPaper.status]} px-2 py-1 text-xs font-medium ${ExamPaperStatusBadgeTextColorMappings[examPaper.status]}">
            <svg class="h-1.5 w-1.5 ${ExamPaperStatusBadgeFillColorMappings[examPaper.status]}" viewBox="0 0 6 6" aria-hidden="true">
                <circle cx="3" cy="3" r="3" />
            </svg>
            ${examPaper.statusAsString}
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
        }, "")
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
        ${this.#examPapers.length > 0 ? this.#renderExamPapers() : this.#renderEmptyState()}
    </tbody>
</table>
        `;
    }
}