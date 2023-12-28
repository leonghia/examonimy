// Imports
import { QuestionPreviewComponent } from "../components/question-preview.component.js";
import { fetchData } from "../helpers/ajax.helper.js";
import { ExamPaperQuestion } from "../models/exam-paper-question.model.js";

// DOM selectors
const questionListContainer = document.querySelector("#question-list-container");
// States
const examPaperId = Number(document.querySelector("#exam-paper-detail-container").dataset.examPaperId);
// Function expressions

// Event listeners

// On load
(async () => {
    const examPaperQuestions = [new ExamPaperQuestion()];   
    Object.assign(examPaperQuestions, (await fetchData(`exam-paper/${examPaperId}/question`)).data);
    examPaperQuestions.forEach(ePQ => {
        questionListContainer.insertAdjacentHTML("beforeend", `
        <div class="bg-white rounded-lg p-6" data-question-number="${ePQ.number}" data-question-id="${ePQ.question.id}">
            <div class="flex items-center justify-between mb-6">
                <p class="text-sm font-bold text-gray-900">Câu ${ePQ.number}</p>
                <button type="button" title="Nhận xét câu hỏi" class="bg-violet-50 rounded-md p-2 text-violet-600 hover:bg-violet-100 hover:text-violet-700">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-5 h-5">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M8.625 9.75a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Zm0 0H8.25m4.125 0a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Zm0 0H12m4.125 0a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Zm0 0h-.375m-13.5 3.01c0 1.6 1.123 2.994 2.707 3.227 1.087.16 2.185.283 3.293.369V21l4.184-4.183a1.14 1.14 0 0 1 .778-.332 48.294 48.294 0 0 0 5.83-.498c1.585-.233 2.708-1.626 2.708-3.228V6.741c0-1.602-1.123-2.995-2.707-3.228A48.394 48.394 0 0 0 12 3c-2.392 0-4.744.175-7.043.513C3.373 3.746 2.25 5.14 2.25 6.741v6.018Z" />
                    </svg>
                </button>
            </div>
            
            <div class="question-container"></div>
        </div>`);
        new QuestionPreviewComponent(questionListContainer.lastElementChild.lastElementChild, ePQ.question).connectedCallback();
    });
})();