import { ExamPaperQuestion } from "../models/exam-paper-question.model.js";
import { Question } from "../models/question.model.js";
import { BaseComponent } from "./base.component.js";
import { QuestionPreviewComponent } from "./question-preview.component.js";

export class ExamPaperQuestionListComponent extends BaseComponent {
    #container;
    #examPaperQuestions = [new ExamPaperQuestion()];  
    _events = {
        delete: []
    }

    constructor(container = new HTMLElement(), examPaperQuestions = [new ExamPaperQuestion()]) {
        super();
        this.#container = container;
        this.#examPaperQuestions = examPaperQuestions;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();      
        Array.from(this.#container.querySelectorAll(".question-preview-container")).forEach((e, i) => {
            const questionPreviewComponent = new QuestionPreviewComponent(e, this.#examPaperQuestions[i].question, `Câu ${this.#examPaperQuestions[i].number}`, true);
            questionPreviewComponent.connectedCallback();
        });

        this.#container.addEventListener("click", event => {
            const clickedDeleteButton = event.target.closest(".delete-btn");
            if (clickedDeleteButton) {
                const questionId = Number(clickedDeleteButton.closest(".question-preview-container").dataset.questionId);
                this._trigger("delete", questionId);
                clickedDeleteButton.closest(".question-preview-container").remove();
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


    }

    #ordering() {      
        Array.from(this.#container.querySelectorAll(".question-preview-container")).forEach((e, i) => {
            e.dataset.questionNumber = i + 1;
            e.querySelector(".title").textContent = `Câu ${i + 1}`;
        });
    }

    #render() {
        return `
        <div class="space-y-8">
           ${this.#examPaperQuestions.reduce((accumulator, currentValue) => {
               return accumulator + `
               <div data-question-id="${currentValue.question.id}" data-question-number="${currentValue.number}" class="question-preview-container bg-white p-6 rounded-lg">
                    
               </div>              
               `;
           }, "")}
           <div id="add-empty-question-btn-container" class="relative">
                <div class="absolute inset-0 flex items-center" aria-hidden="true">
                    <div class="w-full border-t border-gray-300"></div>
                </div>
                <div class="relative flex justify-center">
                    <button type="button" id="add-empty-question-btn" class="inline-flex items-center gap-x-1.5 rounded-full bg-white px-3 py-1.5 text-sm font-semibold text-gray-900 shadow-sm ring-0 ring-inset ring-gray-300 hover:bg-gray-50">
                        <svg class="-ml-1 -mr-0.5 h-5 w-5 text-gray-400" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                            <path d="M10.75 4.75a.75.75 0 00-1.5 0v4.5h-4.5a.75.75 0 000 1.5h4.5v4.5a.75.75 0 001.5 0v-4.5h4.5a.75.75 0 000-1.5h-4.5v-4.5z"></path>
                        </svg>
                        Thêm câu hỏi
                    </button>
                </div>
            </div>
        </div>
        `;      
    }
}