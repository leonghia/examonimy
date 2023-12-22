// Imports
import { deleteData, fetchData } from "../helpers/ajax.helper.js";
import { QuestionPreviewComponent } from "../components/question-preview.component.js";
import { ExamPaperQuestion } from "../models/exam-paper-question.model.js";
import { ConfirmModalComponent } from "../components/confirm-modal.component.js";


// DOM selectors
const questionListContainer = document.querySelector("#question-list-container");
const confirmDeleteModalContainer = document.querySelector("#confirm-delete-modal-container");
const deleteButton = document.querySelector("#delete-btn");
const examPaperDetailContainer = document.querySelector("#exam-paper-detail-container");

// States
let confirmDeleteModalComponent;
const examPaperId = Number(examPaperDetailContainer.dataset.examPaperId);

// Function expressions



// Event listeners
deleteButton.addEventListener("click", () => {
    confirmDeleteModalComponent = new ConfirmModalComponent(confirmDeleteModalContainer, {
        title: "Xóa đề thi",
        description: "Bạn có chắc chắn muốn xóa đề thi này? Đề thi sau khi bị xóa sẽ không thể khôi phục lại.",
        ctaText: "Xác nhận"
    });
    confirmDeleteModalComponent.connectedCallback();
    confirmDeleteModalComponent.subscribe("cancel", () => {
        confirmDeleteModalComponent?.disconnectedCallback();
        confirmDeleteModalComponent = null;
    });
    confirmDeleteModalComponent.subscribe("confirm", async () => {
        try {
            await deleteData("exam-paper", examPaperId);
            document.location.href = "/exam-paper";
        } catch (err) {
            console.error(err);
        }
    });
});



// On load
(async () => {
    try {
        const examPaperQuestions = [new ExamPaperQuestion()];
        Object.assign(examPaperQuestions, (await fetchData(`exam-paper/${examPaperId}/question`)).data);
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