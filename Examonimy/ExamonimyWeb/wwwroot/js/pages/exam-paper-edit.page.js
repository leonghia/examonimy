// Imports
import { fetchData, putData } from "../helpers/ajax.helper.js";
import { QuestionRequestParams } from "../models/request-params.model.js";
import { QuestionListPaletteComponent } from "../components/question-list-palette.component.js";
import { ExamPaperQuestion } from "../models/exam-paper-question.model.js";
import { Question } from "../models/question.model.js";
import { ExamPaperQuestionListComponent } from "../components/exam-paper-question-list.component.js";
import { ExamPaperUpdate } from "../models/exam-paper.model.js";
import { CommitModalComponent } from "../components/commit-modal.component.js";

// DOM selectors
const examPaperContainer = document.querySelector("#exam-paper-container");
const questionListPaletteContainer = document.querySelector("#question-list-palette-container");
const examPaperQuestionListContainer = document.querySelector("#exam-paper-question-list-container");
const questionBankContainer = document.querySelector("#question-bank-container");
const addEmptyQuestionButton = document.querySelector("#add-empty-question-btn");
const updateExamPaperButton = document.querySelector("#update-exam-paper-btn");
const commitModalContainer = document.querySelector("#commit-modal-container");



// States
const examPaperId = Number(examPaperContainer.dataset.examPaperId);
const courseId = Number(questionBankContainer.dataset.courseId);
let questionListPaletteComponent;
let examPaperQuestionListComponent;
let commitModalComponent;


// Function expressions
const emptyQuestionHandler = (questionId = 0) => {
    questionListPaletteComponent.enableQuestion(questionId);
}

const dropQuestionHandler = (data = {questionId: 0, questionNumber: 0}) => {
    const question = questionListPaletteComponent.disableQuestion(data.questionId);
    examPaperQuestionListComponent.populateQuestion(data.questionNumber, question);
    questionListPaletteComponent.unHighlightAllQuestions();
}

// Event listeners
addEmptyQuestionButton.addEventListener("click", () => {
    examPaperQuestionListComponent.addEmptyQuestion();
});

updateExamPaperButton.addEventListener("click", async () => {
    const examPaperUpdate = new ExamPaperUpdate();
    examPaperUpdate.examPaperQuestions = examPaperQuestionListComponent.getExamPaperQuestionUpdates();
    commitModalComponent = new CommitModalComponent(commitModalContainer, { title: "Cập nhật đề thi", ctaText: "Xác nhận" });
    commitModalComponent.connectedCallback();
    commitModalComponent.subscribe("cancel", () => {
        commitModalComponent.disconnectedCallback();
        commitModalComponent = undefined;
    });
});

// On load
(async () => {
    try {
        let res = await fetchData(`exam-paper/${examPaperId}/question-with-answer`);
        const examPaperQuestions = [new ExamPaperQuestion()];
        Object.assign(examPaperQuestions, res.data);
        examPaperQuestionListComponent = new ExamPaperQuestionListComponent(examPaperQuestionListContainer, examPaperQuestions);
        examPaperQuestionListComponent.connectedCallback();      
        examPaperQuestionListComponent.subscribe("empty", emptyQuestionHandler);
        examPaperQuestionListComponent.subscribe("drop", dropQuestionHandler);


        res = await fetchData("question", new QuestionRequestParams(null, courseId));
        const questions = [new Question()];
        Object.assign(questions, res.data);
        questionListPaletteComponent = new QuestionListPaletteComponent(questionListPaletteContainer, questions);
        questionListPaletteComponent.courseName = questions[0].course.name;     
        questionListPaletteComponent.currentPage = res.paginationMetadata.currentPage;
        questionListPaletteComponent.totalPages = res.paginationMetadata.totalPages;
        questionListPaletteComponent.disabledQuestionIds = examPaperQuestions.map(ePQ => ePQ.question.id);      
        questionListPaletteComponent.connectedCallback();    

        
        
    } catch (err) {
        console.error(err);
    }
})();