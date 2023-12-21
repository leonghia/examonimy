import { ExamPaperQuestionCreate } from "../models/exam-paper-question-create.model.js";
import { ExamPaperQuestion } from "../models/exam-paper-question.model.js";
import { BaseComponent } from "./base.component.js";
import { QuestionPreviewComponent } from "./question-preview.component.js";

export class ExamPaperQuestionListComponent extends BaseComponent {
    #container;
    #examPaperQuestions;  
    #hasDeleteButton;
    _events = {
        delete: [],
        drop: []
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
            const clickedDeleteButton = event.target.closest(".delete-btn");
            if (clickedDeleteButton) {
                const questionId = Number(clickedDeleteButton.closest(".exam-paper-question-container").dataset.questionId);
                this._trigger("delete", questionId);
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
        this.#container.querySelector(`.exam-paper-question-container[data-question-number="${questionNumber}"]`).querySelector(".question-preview-container").innerHTML = new QuestionPreviewComponent(null, question).render();
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
        <button type="button" title="Gỡ câu hỏi" class="delete-btn rounded bg-red-50 hover:bg-red-100 text-red-600 hover:text-red-700 p-2">
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-4 h-4">
                <path stroke-linecap="round" stroke-linejoin="round" d="M6 18 18 6M6 6l12 12"></path>
            </svg>
        </button>
    </div>
    <div class="question-preview-container">

        <div class="empty-placeholder relative block w-full rounded-lg border-2 border-dashed border-gray-300 p-12 text-center hover:border-gray-400 focus:outline-none focus:ring-0 focus:ring-indigo-500 focus:ring-offset-2">
            <svg class="empty-icon pointer-events-none mx-auto h-12 w-12 text-gray-400" stroke="currentColor" fill="none" viewBox="0 0 48 48" aria-hidden="true">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 14v20c0 4.418 7.163 8 16 8 1.381 0 2.721-.087 4-.252M8 14c0 4.418 7.163 8 16 8s16-3.582 16-8M8 14c0-4.418 7.163-8 16-8s16 3.582 16 8m0 0v14m0-4c0 4.418-7.163 8-16 8S8 28.418 8 24m32 10v6m0 0v6m0-6h6m-6 0h-6"></path>
            </svg>
            <span class="empty-text pointer-events-none mt-2 block text-sm font-semibold text-gray-400">Chưa có nội dung</span>
        </div>

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
                        ${this.#hasDeleteButton ? `
                        <button type="button" title="Gỡ câu hỏi" class="delete-btn rounded bg-red-50 hover:bg-red-100 text-red-600 hover:text-red-700 p-2">
                            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-4 h-4">
                                <path stroke-linecap="round" stroke-linejoin="round" d="M6 18 18 6M6 6l12 12" />
                            </svg>
                        </button>
                        ` : ``}
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
                    <button type="button" title="Gỡ câu hỏi" class="delete-btn rounded bg-red-50 hover:bg-red-100 text-red-600 hover:text-red-700 p-2">
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