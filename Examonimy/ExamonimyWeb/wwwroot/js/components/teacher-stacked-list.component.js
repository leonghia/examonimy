import { postData } from "../helpers/ajax.helper.js";
import { hideSpinnerForButtonWithCheckmark, showSpinnerForButton, hideSpinnerForButtonWithoutCheckmark } from "../helpers/markup.helper.js";
import { mapByFullName } from "../helpers/user.helper.js";
import { ExamPaperReviewerCreate } from "../models/exam-paper.model.js";
import { SpinnerOption } from "../models/spinner-option.model.js";
import { User } from "../models/user.model.js";
import { BaseComponent } from "./base.component.js";


export class TeacherStackedListComponent extends BaseComponent {
    #examPaperId;
    #container;
    #teachers;
    #reviewerIds;
    #closeButton;
    _events = {
        close: [],
        confirm: []
    }
    #confirmButton;
    #spinnerOption = new SpinnerOption("w-5", "h-5");

    constructor(container = new HTMLElement(), teachers = [new User()], examPaperId = 0, reviewerIds = [0]) {
        super();
        this.#container = container;
        this.#teachers = teachers;
        this.#examPaperId = examPaperId;
        this.#reviewerIds = reviewerIds;
    }

    connectedCallback() {
        this.#container.innerHTML = this.render();
        this.#markReviewersAsChecked(this.#reviewerIds);

        this.#closeButton = this.#container.querySelector("#close-btn");
        this.#confirmButton = this.#container.querySelector("#confirm-btn");

        this.#closeButton.addEventListener("click", () => {
            this.#container.innerHTML = "";
            this._trigger("close");
        });

        this.#confirmButton.addEventListener("click", async () => {
            showSpinnerForButton(this.#confirmButton);
            const teacherIds = Array.from(this.#container.querySelectorAll(".teacher")).filter(e => e.checked).map(e => Number(e.value));
            try {               
                const examPaperReviewerCreate = new ExamPaperReviewerCreate(teacherIds);
                await postData(`exam-paper/${this.#examPaperId}/reviewer`, examPaperReviewerCreate);
                hideSpinnerForButtonWithCheckmark(this.#confirmButton);
                this.#confirmButton.innerHTML = `
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="w-5 h-5">
                    <path fill-rule="evenodd" d="M19.916 4.626a.75.75 0 0 1 .208 1.04l-9 13.5a.75.75 0 0 1-1.154.114l-6-6a.75.75 0 0 1 1.06-1.06l5.353 5.353 8.493-12.74a.75.75 0 0 1 1.04-.207Z" clip-rule="evenodd" />
                </svg>
                `;
                this._trigger("confirm", { examPaperId: this.#examPaperId, teacherIds });
                setTimeout(() => {
                    this.#container.innerHTML = "";
                }, 1500);
            } catch (err) {
                hideSpinnerForButtonWithoutCheckmark(this.#confirmButton, "Xác nhận");
                console.error(err);
            }
        });
    }

    #markReviewersAsChecked(reviewerIds = [0]) {
        Array.from(this.#container.querySelectorAll(".teacher")).forEach(v => {
            if (reviewerIds.indexOf(Number(v.value)) > -1) {
                v.checked = true;
            }
        });
    }

    #renderListMarkup(teacherMap = new Map([["", [new User()]]])) {
        const keys = Array.from(teacherMap.keys());
        return keys.reduce((accumulator, key) => {
            const teacherArr = teacherMap.get(key);
            return accumulator + `
<div class="relative">
    <div class="sticky top-0 z-10 border-y border-b-gray-200 border-t-gray-100 bg-gray-50 px-3 py-1.5 text-sm font-semibold leading-6 text-gray-900">
        <h3>${key}</h3>
    </div>
    <ul role="list" class="divide-y divide-gray-100">
        ${teacherArr.reduce((accumulator, teacher) => {
            return accumulator + `
            <li class="flex items-center justify-between p-4">
                <div class="flex gap-x-4">
                    <img class="h-12 w-12 flex-none rounded-full bg-gray-50" src="${teacher.profilePicture}" alt="profile image">
                    <div class="min-w-0">
                        <p class="text-sm font-semibold leading-6 text-gray-900">${teacher.fullName}</p>
                        <p class="mt-1 truncate text-xs leading-5 text-gray-500">${teacher.email}</p>
                    </div>
                </div>
                <input id="${teacher.id}" type="checkbox" value="${teacher.id}" name="teacher" class="teacher w-5 h-5 text-teal-500 bg-gray-300 border-none rounded focus:ring-0 focus:ring-offset-0">
            </li>
            `;
        }, "")}
    </ul>
</div>
            `;
        }, "");
    }

    render() {
        return `
<div class="relative z-[999]" aria-labelledby="modal-title" role="dialog" aria-modal="true">
    <!--
      Background backdrop, show/hide based on modal state.

      Entering: "ease-out duration-300"
        From: "opacity-0"
        To: "opacity-100"
      Leaving: "ease-in duration-200"
        From: "opacity-100"
        To: "opacity-0"
    -->
    <div class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity"></div>

    <div class="fixed inset-0 z-10 w-screen overflow-y-auto">
        <div class="flex min-h-full items-end justify-center p-4 sm:items-center sm:p-0">
            <!--
              Modal panel, show/hide based on modal state.

              Entering: "ease-out duration-300"
                From: "opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
                To: "opacity-100 translate-y-0 sm:scale-100"
              Leaving: "ease-in duration-200"
                From: "opacity-100 translate-y-0 sm:scale-100"
                To: "opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
            -->
            <div class="w-[30rem] bg-white rounded-lg">
                <div class="w-full p-4 flex items-center justify-between">
                    <div class="space-y-2">
                        <h6 class="text-gray-700 font-semibold text-sm">Thêm kiểm duyệt viên</h6>
                        <p class="text-gray-500 text-xs italic">*Bạn có thể thêm tối đa 3 kiểm duyệt viên</p>
                    </div>                  
                    <button type="button" title="Đóng" id="close-btn" class="p-2 bg-gray-200 text-gray-800 hover:bg-gray-300 hover:text-gray-900 rounded">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="w-4 h-4">
                            <path fill-rule="evenodd" d="M5.47 5.47a.75.75 0 0 1 1.06 0L12 10.94l5.47-5.47a.75.75 0 1 1 1.06 1.06L13.06 12l5.47 5.47a.75.75 0 1 1-1.06 1.06L12 13.06l-5.47 5.47a.75.75 0 0 1-1.06-1.06L10.94 12 5.47 6.53a.75.75 0 0 1 0-1.06Z" clip-rule="evenodd" />
                        </svg>
                    </button>
                </div>
                <nav class="max-h-96 w-full overflow-y-auto">
                    ${this.#renderListMarkup(mapByFullName(this.#teachers))}                               
                </nav>
                <div class="w-full p-4 flex items-center justify-end">
                    <button type="button" id="confirm-btn" class="w-24 flex items-center justify-center text-sm font-semibold bg-violet-300 hover:bg-violet-400 text-violet-800 hover:text-violet-900 rounded-md py-3 px-4"><span class="button-text">Xác nhận</span></button>
                </div>
            </div>
            
        </div>
    </div>
</div>
        `;
    }
}