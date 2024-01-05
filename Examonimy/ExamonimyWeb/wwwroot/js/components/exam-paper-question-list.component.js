import { ExamPaperQuestion, ExamPaperQuestionUpdate, ExamPaperQuestionCreate } from "../models/exam-paper-question.model.js";
import { BaseComponent } from "./base.component.js";
import { QuestionPreviewComponent } from "./question-preview.component.js";

export class ExamPaperQuestionListComponent extends BaseComponent {
    #container;
    #examPaperQuestions;  
    #hasDeleteButton;
    _events = {
        empty: [],
        drop: [],
        delete: []
    }

    constructor(container = new HTMLElement(), examPaperQuestions = [new ExamPaperQuestion()], hasDeleteButton = true) {
        super();
        this.#container = container;
        this.#examPaperQuestions = examPaperQuestions;
        this.#hasDeleteButton = hasDeleteButton;
    }

    connectedCallback() {
        this.#container.innerHTML = this.render(); 
              

        this.#container.addEventListener("click", event => {
            const clickedToggleAnswerButton = event.target.closest(".toggle-answer-btn");
            if (clickedToggleAnswerButton) {
                clickedToggleAnswerButton.closest(".exam-paper-question-container").querySelector(".answer-container").classList.toggle("hidden");
                return;
            }

            const clickedEmptyButton = event.target.closest(".empty-btn");
            if (clickedEmptyButton) {
                const questionId = Number(clickedEmptyButton.closest(".exam-paper-question-container").dataset.questionId);
                this._trigger("empty", questionId);  
                clickedEmptyButton.closest(".exam-paper-question-container").querySelector(".question-preview-container").innerHTML = this.#renderEmptyQuestion();
                // turn off toggle and empty button
                clickedEmptyButton.classList.add("hidden");
                clickedEmptyButton.parentElement.querySelector(".toggle-answer-btn").classList.add("hidden");
                // show the delete button
                clickedEmptyButton.parentElement.querySelector(".delete-btn").classList.remove("hidden");
                return;
            }

            const clickedDeleteButton = event.target.closest(".delete-btn");
            if (clickedDeleteButton) {
                clickedDeleteButton.closest(".exam-paper-question-container").remove();
                this.#ordering();   
                return;
            }

            const addEmptyQuestionButton = event.target.closest("#add-empty-question-btn");
            if (addEmptyQuestionButton) {
                const count = Array.from(this.#container.querySelectorAll(".question-preview-container")).length;
                this.#container.querySelector("#add-empty-question-btn-container").insertAdjacentHTML("beforebegin", `
                <div data-question-number="${count + 1}" class="question-preview-container bg-white p-6 rounded-lg"></div>
                `);
                new QuestionPreviewComponent(this.#container.querySelector(`.question-preview-container[data-question-number="${count + 1}"]`), null, `Câu ${count + 1}`, true).connectedCallback();
                return;
            }
        });

        this.#container.addEventListener("dragenter", event => {
            event.preventDefault();
            if (event.target.matches(".empty-placeholder"))
                this.#greenEmptyPlaceholder(event.target);
        });

        this.#container.addEventListener("dragleave", event => {
            event.preventDefault();
            if (event.target.matches(".empty-placeholder"))
                this.#grayEmptyPlaceholder(event.target);
        });

        this.#container.addEventListener("dragover", event => {
            event.preventDefault();
            event.dataTransfer.dropEffect = "copy";
        });

        this.#container.addEventListener("drop", event => {
            event.preventDefault();
            if (event.target.matches(".empty-placeholder")) {   
                const questionId = Number(event.dataTransfer.getData("text/plain"));
                const questionNumber = Number(event.target.closest(".exam-paper-question-container").dataset.questionNumber);     
                event.target.closest(".exam-paper-question-container").dataset.questionId = questionId;
                this._trigger("drop", {questionId, questionNumber});
            }
        });
    }   

    #renderEmptyQuestion() {
        return `   
        <div class="empty-placeholder relative block w-full rounded-lg border-2 border-dashed border-gray-300 p-12 text-center hover:border-gray-400 focus:outline-none focus:ring-0 focus:ring-indigo-500 focus:ring-offset-2">
            <svg class="empty-icon pointer-events-none mx-auto h-12 w-12 text-gray-400" stroke="currentColor" fill="none" viewBox="0 0 48 48" aria-hidden="true">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 14v20c0 4.418 7.163 8 16 8 1.381 0 2.721-.087 4-.252M8 14c0 4.418 7.163 8 16 8s16-3.582 16-8M8 14c0-4.418 7.163-8 16-8s16 3.582 16 8m0 0v14m0-4c0 4.418-7.163 8-16 8S8 28.418 8 24m32 10v6m0 0v6m0-6h6m-6 0h-6"></path>
            </svg>
            <span class="empty-text pointer-events-none mt-2 block text-sm font-semibold text-gray-400">Chưa có nội dung</span>
        </div>
        `;
    }

    #greenEmptyPlaceholder(emptyPlaceholder) {
        emptyPlaceholder.classList.remove("border-gray-300");
        emptyPlaceholder.querySelector(".empty-icon").classList.remove("text-gray-400");
        emptyPlaceholder.querySelector(".empty-text").classList.remove("text-gray-400");
        emptyPlaceholder.classList.add("border-green-500");
        emptyPlaceholder.classList.add("bg-green-50");
        emptyPlaceholder.querySelector(".empty-icon").classList.add("text-green-400");
        emptyPlaceholder.querySelector(".empty-text").classList.add("text-green-400");
    }

    populateQuestion(questionNumber, question) {
        const temp = this.#container.querySelector(`.exam-paper-question-container[data-question-number="${questionNumber}"]`);
        temp.querySelector(".question-preview-container").innerHTML = new QuestionPreviewComponent(null, question).render();
        // show toggle and empty buttons
        temp.querySelector(".toggle-answer-btn").classList.remove("hidden");
        temp.querySelector(".empty-btn").classList.remove("hidden");
        // hide delete button
        temp.querySelector(".delete-btn").classList.add("hidden");
    }

    #grayEmptyPlaceholder(emptyPlaceholder) {
        emptyPlaceholder.classList.remove("bg-green-50");
        emptyPlaceholder.classList.remove("border-green-500");
        emptyPlaceholder.querySelector(".empty-icon").classList.remove("text-green-400");
        emptyPlaceholder.querySelector(".empty-text").classList.remove("text-green-400");
        emptyPlaceholder.classList.add("border-gray-300");
        emptyPlaceholder.querySelector(".empty-icon").classList.add("text-gray-400");
        emptyPlaceholder.querySelector(".empty-text").classList.add("text-gray-400");
    }

    getExamPaperQuestionCreates() {
        return Array.from(this.#container.querySelectorAll(".exam-paper-question-container")).map(v => new ExamPaperQuestionCreate(Number(v.dataset.questionId), Number(v.dataset.questionNumber)));
    }

    getExamPaperQuestionUpdates() {
        return Array.from(this.#container.querySelectorAll(".exam-paper-question-container")).map(v => new ExamPaperQuestionUpdate(Number(v.dataset.questionId), Number(v.dataset.questionNumber)));
    }

    getMarkup() {
        return this.#container.innerHTML;
    }

    #ordering() {      
        Array.from(this.#container.querySelectorAll(".exam-paper-question-container")).forEach((e, i) => {
            e.dataset.questionNumber = i + 1;
            e.querySelector(".question-number").textContent = `Câu ${i + 1}`;
        });
    }

    addEmptyQuestion() {
        this.#container.firstElementChild.insertAdjacentHTML("beforeend", `
<div class="bg-white rounded-lg p-6 exam-paper-question-container" data-question-number="${this.#container.firstElementChild.childElementCount + 1}">
    <div class="flex items-center justify-between mb-4">
        <p class="question-number text-base font-bold text-gray-900">Câu ${this.#container.firstElementChild.childElementCount + 1}</p>
        <div class="flex items-center gap-x-4">
            <button type="button" title="Ẩn/hiện đáp án" class="hidden toggle-answer-btn rounded bg-yellow-300 hover:bg-yellow-400 text-yellow-800 hover:text-yellow-900 p-2">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-4 h-4">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M12 18v-5.25m0 0a6.01 6.01 0 0 0 1.5-.189m-1.5.189a6.01 6.01 0 0 1-1.5-.189m3.75 7.478a12.06 12.06 0 0 1-4.5 0m3.75 2.383a14.406 14.406 0 0 1-3 0M14.25 18v-.192c0-.983.658-1.823 1.508-2.316a7.5 7.5 0 1 0-7.517 0c.85.493 1.509 1.333 1.509 2.316V18" />
                </svg>
            </button>
            <button type="button" title="Làm trống câu hỏi" class="hidden empty-btn rounded bg-red-300 hover:bg-red-400 text-red-800 hover:text-red-900 p-2">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-4 h-4">
                    <path stroke-linecap="round" stroke-linejoin="round" d="m14.74 9-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 0 1-2.244 2.077H8.084a2.25 2.25 0 0 1-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 0 0-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 0 1 3.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 0 0-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 0 0-7.5 0" />
                </svg>
            </button>
            <button type="button" title="Xóa câu hỏi" class="delete-btn rounded bg-red-300 hover:bg-red-400 text-red-800 hover:text-red-900 p-2">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-4 h-4">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M6 18 18 6M6 6l12 12"></path>
                </svg>
            </button>
        </div>
    </div>
    <div class="question-preview-container">
        ${this.#renderEmptyQuestion()}
    </div>
</div>
        `);
    }

    render() {
        return `
        <div class="space-y-8">
           ${this.#examPaperQuestions ? this.#examPaperQuestions.reduce((accumulator, currentValue) => {
               return accumulator + `
               <div class="bg-white rounded-lg p-6 exam-paper-question-container" data-question-number="${currentValue.number}" data-question-id="${currentValue.question.id}">
                   <div class="flex items-center justify-between mb-4">
                        <p class="question-number text-base font-bold text-gray-900">Câu ${currentValue.number}</p>
                        <div class="flex items-center gap-x-4">
                            <button type="button" title="Ẩn/hiện đáp án" class="toggle-answer-btn rounded bg-yellow-300 hover:bg-yellow-400 text-yellow-800 hover:text-yellow-900 p-2">
                                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-4 h-4">
                                    <path stroke-linecap="round" stroke-linejoin="round" d="M12 18v-5.25m0 0a6.01 6.01 0 0 0 1.5-.189m-1.5.189a6.01 6.01 0 0 1-1.5-.189m3.75 7.478a12.06 12.06 0 0 1-4.5 0m3.75 2.383a14.406 14.406 0 0 1-3 0M14.25 18v-.192c0-.983.658-1.823 1.508-2.316a7.5 7.5 0 1 0-7.517 0c.85.493 1.509 1.333 1.509 2.316V18" />
                                </svg>
                            </button>  
                            <button type="button" title="Làm trống câu hỏi" class="empty-btn rounded bg-red-300 hover:bg-red-400 text-red-800 hover:text-red-900 p-2">
                                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-4 h-4">
                                    <path stroke-linecap="round" stroke-linejoin="round" d="m14.74 9-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 0 1-2.244 2.077H8.084a2.25 2.25 0 0 1-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 0 0-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 0 1 3.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 0 0-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 0 0-7.5 0" />
                                </svg>
                            </button>     
                            <button type="button" title="Xóa câu hỏi" class="hidden delete-btn rounded bg-red-300 hover:bg-red-400 text-red-800 hover:text-red-900 p-2">
                                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-4 h-4">
                                    <path stroke-linecap="round" stroke-linejoin="round" d="M6 18 18 6M6 6l12 12"></path>
                                </svg>
                            </button>  
                        </div>
                    </div>
                    <div class="question-preview-container">
                        ${new QuestionPreviewComponent(null, currentValue.question).render()}
                    </div>
               </div>
               `;
           }, "") : `
           <div class="bg-white rounded-lg p-6 exam-paper-question-container" data-question-number="1">
                <div class="flex items-center justify-between mb-4">
                    <p class="question-number text-base font-bold text-gray-900">Câu 1</p>
                    <button type="button" title="Gỡ câu hỏi" class="delete-btn rounded bg-red-300 hover:bg-red-400 text-red-800 hover:text-red-900 p-2">
                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-4 h-4">
                            <path stroke-linecap="round" stroke-linejoin="round" d="M6 18 18 6M6 6l12 12" />
                        </svg>
                    </button>
                </div>
                <div class="question-preview-container">
                    ${new QuestionPreviewComponent(null, null).render()}
                </div>
           </div>          
           `}          
        </div>
        `;      
    }
}