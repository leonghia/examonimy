import { LOCALES } from "../config.js";
import { DateTimeLocaleStringStyle } from "../helpers/datetime.helper.js";
import { Exam, ExamForStudent } from "../models/exam.model.js";
import { ConfirmModalComponent } from "./confirm-modal.component.js";
import { deleteData } from "../helpers/ajax.helper.js";
import { hideSpinnerForButtonWithCheckmark, hideSpinnerForButtonWithoutCheckmark, showSpinnerForButton } from "../helpers/markup.helper.js";
import { SpinnerOption } from "../models/spinner-option.model.js";

export class ExamTableComponent {
    #container;
    #exams;
    #confirmModalComponent;
    #spinnerOption = new SpinnerOption("fill-red-800");

    constructor(container = new HTMLElement(), exams = [new Exam()]) {
        this.#container = container;
        this.#exams = exams;
    }

    connectedCallback() {
        this.populate();

        this.#container.addEventListener("click", event => {
            const clickedDeleteButton = event.target.closest(".delete-btn");
            if (clickedDeleteButton) {
                this.#confirmModalComponent = new ConfirmModalComponent(clickedDeleteButton.nextElementSibling, {
                    title: "Xóa kì thi",
                    description: "Bạn có chắc chắn muốn xóa kì thi này? Kì thi sau khi bị xóa sẽ không thể khôi phục lại.",
                    ctaText: "Xác nhận"
                });
                this.#confirmModalComponent.connectedCallback();
                this.#confirmModalComponent.subscribe("confirm", async (confirmButton) => {
                    showSpinnerForButton(confirmButton, this.#spinnerOption);
                    try {
                        await deleteData("exam", Number(clickedDeleteButton.dataset.examId));
                        hideSpinnerForButtonWithCheckmark(confirmButton, this.#spinnerOption);
                        setTimeout(() => document.location.reload(), 1000);
                    } catch (err) {
                        console.error(err);
                        hideSpinnerForButtonWithoutCheckmark(confirmButton, this.#spinnerOption);
                    }
                });
                this.#confirmModalComponent.subscribe("cancel", () => {
                    this.#confirmModalComponent.disconnectedCallback();
                    this.#confirmModalComponent = undefined;
                })
            }
        });
    }

    populate() {
        this.#container.innerHTML = this.#render();
    }

    #getIconStylingForExamStatus(from = new Date(), to = new Date()) {
        var currentDate = new Date();
        if (currentDate < from)
            return "text-amber-400 bg-amber-400/10";
        if (currentDate >= from && currentDate <= to)
            return "text-green-400 bg-green-400/10";
        else
            return "text-red-400 bg-red-400/10";
    }

    #getTextContentForExamStatus(from = new Date(), to = new Date()) {
        var currentDate = new Date();
        if (currentDate < from)
            return "Chưa bắt đầu";
        if (currentDate >= from && currentDate <= to)
            return "Đang diễn ra";
        else
            return "Đã kết thúc";
    }

    #renderExams() {
        return this.#exams.reduce((prev, cur, i) => {
            return prev + `
<tr>
    <td class="whitespace-nowrap py-4 pl-4 pr-3 text-sm font-medium text-gray-900 sm:pl-0">${i + 1}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm font-medium text-gray-700">${cur.courseName}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${cur.examPaperCode}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${cur.mainClasses.join(", ")}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${cur.timeAllowedInMinutes} phút</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${new Date(cur.from).toLocaleString(LOCALES, DateTimeLocaleStringStyle)} - ${new Date(cur.to).toLocaleString(LOCALES, DateTimeLocaleStringStyle)}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
        <div class="flex items-center justify-end gap-x-2 sm:justify-start">
            <div class="flex-none rounded-full p-1 ${this.#getIconStylingForExamStatus(new Date(cur.from), new Date(cur.to))}">
                <div class="h-1.5 w-1.5 rounded-full bg-current"></div>
            </div>
            <div class="hidden text-gray-500 sm:block">${this.#getTextContentForExamStatus(new Date(cur.from), new Date(cur.to))}</div>
        </div>
    </td>
    <td class="whitespace-nowrap px-3 py-4">
        <a href="/exam/edit/${cur.id}" class="text-sm font-semibold text-green-600 hover:text-green-700">Sửa</a>
    </td>
    <td class="whitespace-nowrap px-3 py-4">
        <button type="button" data-exam-id="${cur.id}" class="delete-btn text-sm font-semibold text-red-600 hover:text-red-700">Xóa</button>
        <div class="confirm-modal-container"></div>
    </td>
</tr>
            `;
        }, "")
    }

    #renderEmpty() {
        return `
<tr>
    <td colspan="8">
        <div class="text-center py-12">         
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" class="mx-auto h-12 w-12 text-gray-400">
            <path stroke-linecap="round" stroke-linejoin="round" d="M6.75 3v2.25M17.25 3v2.25M3 18.75V7.5a2.25 2.25 0 0 1 2.25-2.25h13.5A2.25 2.25 0 0 1 21 7.5v11.25m-18 0A2.25 2.25 0 0 0 5.25 21h13.5A2.25 2.25 0 0 0 21 18.75m-18 0v-7.5A2.25 2.25 0 0 1 5.25 9h13.5A2.25 2.25 0 0 1 21 11.25v7.5m-9-6h.008v.008H12v-.008ZM12 15h.008v.008H12V15Zm0 2.25h.008v.008H12v-.008ZM9.75 15h.008v.008H9.75V15Zm0 2.25h.008v.008H9.75v-.008ZM7.5 15h.008v.008H7.5V15Zm0 2.25h.008v.008H7.5v-.008Zm6.75-4.5h.008v.008h-.008v-.008Zm0 2.25h.008v.008h-.008V15Zm0 2.25h.008v.008h-.008v-.008Zm2.25-4.5h.008v.008H16.5v-.008Zm0 2.25h.008v.008H16.5V15Z" />
          </svg>
          <h3 class="mt-2 text-sm font-semibold text-gray-900">Không có kỳ thi nào</h3>               
        </div>
    </td>
</tr>
        `;
    }

    #render() {
        return `
<div class="">
  <div class="mt-8 flow-root">
    <div class="overflow-x-auto">
      <div class="inline-block min-w-full align-middle bg-white px-6 py-2 rounded-lg">
        <table class="min-w-full divide-y divide-gray-300">
          <thead>
            <tr>
              <th scope="col" class="py-3 pl-4 pr-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500 sm:pl-0">STT</th>
              <th scope="col" class="px-3 py-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Môn học</th>
              <th scope="col" class="px-3 py-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Mã đề thi</th>
              <th scope="col" class="px-3 py-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Lớp</th>             
              <th scope="col" class="px-3 py-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Thời lượng</th>
              <th scope="col" class="px-3 py-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Thời gian mở truy cập</th>
              <th scope="col" class="px-3 py-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Trạng thái</th>   
              <th colspan="2" scope="col" class="py-3 pr-4 pl-3 text-center text-xs font-medium uppercase tracking-wide text-gray-500 sm:pl-0">Tùy chọn</th>
            </tr>
          </thead>
          <tbody class="divide-y divide-gray-200 bg-white">           
            ${this.#exams?.length ? this.#renderExams() : this.#renderEmpty()}
          </tbody>
        </table>
      </div>
    </div>
  </div>
</div>
        `;
    }
}


export class ExamTableForStudentComponent {
    #container;
    #exams;

    constructor(container = new HTMLElement(), exams = [new ExamForStudent()]) {
        this.#container = container;
        this.#exams = exams;
    }

    connectedCallback() {
        this.populate();
    }

    populate() {
        this.#container.innerHTML = this.#render();
    }

    #getIconStylingForExamStatus(from = new Date(), to = new Date()) {
        const currentDate = new Date();
        if (currentDate < from)
            return "text-amber-400 bg-amber-400/10";
        if (currentDate >= from && currentDate <= to)
            return "text-green-400 bg-green-400/10";
        else
            return "text-red-400 bg-red-400/10";
    }

    #getTextContentForExamStatus(from = new Date(), to = new Date()) {
        const currentDate = new Date();
        if (currentDate < from)
            return "Chưa bắt đầu";
        if (currentDate >= from && currentDate <= to)
            return "Đang diễn ra";
        else
            return "Đã kết thúc";
    }

    #getExamTakingStatus(from = new Date(), to = new Date()) {
        const currentDate = new Date();      
        return currentDate >= from && currentDate <= to;
    }

    #renderExams() {
        let from, to;
        return this.#exams.reduce((prev, cur, i) => {
            from = new Date(cur.from);
            to = new Date(cur.to);
            return prev + `
<tr>
    <td class="whitespace-nowrap py-4 pl-4 pr-3 text-sm font-medium text-gray-900 sm:pl-0">${i + 1}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-700 font-semibold">${cur.courseName}</td>   
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${cur.timeAllowedInMinutes} phút</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">Lý thuyết</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${from.toLocaleString(LOCALES, DateTimeLocaleStringStyle)} - ${to.toLocaleString(LOCALES, DateTimeLocaleStringStyle)}</td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
        <div class="flex items-center justify-end gap-x-2 sm:justify-start">
            <div class="flex-none rounded-full p-1 ${this.#getIconStylingForExamStatus(from, to)}">
                <div class="h-1.5 w-1.5 rounded-full bg-current"></div>
            </div>
            <div class="hidden text-gray-500 sm:block">${this.#getTextContentForExamStatus(from, to)}</div>
        </div>
    </td>
    <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
        <a href="/exam/${cur.id}" class="${this.#getExamTakingStatus(from, to) ? "" : "opacity-20 pointer-events-none"} px-4 py-2 bg-violet-300 rounded-md text-sm font-semibold text-violet-800 hover:text-violet-900 hover:bg-violet-400">Vào thi</a>
    </td>
</tr>
            `;
        }, "")
    }

    #renderEmpty() {
        return `
<tr>
    <td colspan="8">
        <div class="text-center py-12">         
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" class="mx-auto h-12 w-12 text-gray-400">
            <path stroke-linecap="round" stroke-linejoin="round" d="M6.75 3v2.25M17.25 3v2.25M3 18.75V7.5a2.25 2.25 0 0 1 2.25-2.25h13.5A2.25 2.25 0 0 1 21 7.5v11.25m-18 0A2.25 2.25 0 0 0 5.25 21h13.5A2.25 2.25 0 0 0 21 18.75m-18 0v-7.5A2.25 2.25 0 0 1 5.25 9h13.5A2.25 2.25 0 0 1 21 11.25v7.5m-9-6h.008v.008H12v-.008ZM12 15h.008v.008H12V15Zm0 2.25h.008v.008H12v-.008ZM9.75 15h.008v.008H9.75V15Zm0 2.25h.008v.008H9.75v-.008ZM7.5 15h.008v.008H7.5V15Zm0 2.25h.008v.008H7.5v-.008Zm6.75-4.5h.008v.008h-.008v-.008Zm0 2.25h.008v.008h-.008V15Zm0 2.25h.008v.008h-.008v-.008Zm2.25-4.5h.008v.008H16.5v-.008Zm0 2.25h.008v.008H16.5V15Z" />
          </svg>
          <h3 class="mt-2 text-sm font-semibold text-gray-900">Không có kỳ thi nào</h3>               
        </div>
    </td>
</tr>
        `;
    }

    #render() {
        return `
<div class="">
  <div class="mt-8 flow-root">
    <div class="overflow-x-auto">
      <div class="inline-block min-w-full align-middle bg-white px-6 py-2 rounded-lg">
        <table class="min-w-full divide-y divide-gray-300">
          <thead>
            <tr>
              <th scope="col" class="py-3 pl-4 pr-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500 sm:pl-0">STT</th>
              <th scope="col" class="px-3 py-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Môn học</th>                      
              <th scope="col" class="px-3 py-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Thời lượng</th>
              <th scope="col" class="px-3 py-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Dạng đề</th>
              <th scope="col" class="px-3 py-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Thời gian mở truy cập</th>
              <th scope="col" class="px-3 py-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500">Trạng thái</th>    
              <th scope="col" class="py-3 pr-4 pl-3 text-left text-xs font-medium uppercase tracking-wide text-gray-500 opacity-0">Hành động</th>  
            </tr>
          </thead>
          <tbody class="divide-y divide-gray-200 bg-white">           
            ${this.#exams?.length ? this.#renderExams() : this.#renderEmpty()}
          </tbody>
        </table>
      </div>
    </div>
  </div>
</div>
        `;
    }
}

