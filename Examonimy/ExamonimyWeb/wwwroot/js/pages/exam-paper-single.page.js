// Imports
import { fetchData } from "../helpers/ajax.helper.js";
import { QuestionPreviewComponent } from "../components/question-preview.component.js";
import { ExamPaperQuestion } from "../models/exam-paper-question.model.js";

// DOM selectors
const questionListContainer = document.querySelector("#question-list-container");


// States



// Function expressions



// Event listeners



// On load
(async () => {
    try {
        const examPaperQuestions = [new ExamPaperQuestion()];
        Object.assign(examPaperQuestions, (await fetchData("exam-paper/3/question")).data);
        examPaperQuestions.forEach(q => {
            questionListContainer.insertAdjacentHTML("beforeend", `
            <div class="p-8 bg-white rounded-lg">
                <p class="mb-6 text-sm font-bold text-gray-900">Câu ${q.number}</p>
                <div id="question-number-${q.number}" class="">           
                </div>
            </div>           
            `);
            const questionPreview = new QuestionPreviewComponent(questionListContainer.lastElementChild.lastElementChild, q.question);
            questionPreview.connectedCallback();
        });
    } catch (err) {
        console.error(err);
    }
})();