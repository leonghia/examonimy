// Imports
import { fetchData, putData } from "../helpers/ajax.helper.js";
import { QuestionRequestParams } from "../models/request-params.model.js";
import { QuestionListPaletteComponent } from "../components/question-list-palette.component.js";
import { ExamPaperQuestion } from "../models/exam-paper-question.model.js";
import { Question } from "../models/question.model.js";
import { ExamPaperQuestionListComponent } from "../components/exam-paper-question-list.component.js";
import { ExamPaperUpdate } from "../models/exam-paper.model.js";
import { hideSpinnerForButton, showSpinnerForButton } from "../helpers/markup.helper.js";

// DOM selectors
const examPaperContainer = document.querySelector("#exam-paper-container");
const questionListPaletteContainer = document.querySelector("#question-list-palette-container");
const examPaperQuestionListContainer = document.querySelector("#exam-paper-question-list-container");
const questionBankContainer = document.querySelector("#question-bank-container");
const addEmptyQuestionButton = document.querySelector("#add-empty-question-btn");
const tabButtonContainer = document.querySelector("#tab-button-container");
const previewContainer = document.querySelector("#preview-container");
const examPaperPreviewContainer = document.querySelector("#exam-paper-preview-container");
const editorContainer = document.querySelector("#editor-container");
const buttonContainer = document.querySelector("#button-container");
const updateExamPaperButton = document.querySelector("#update-exam-paper-btn");



// States
const examPaperId = Number(examPaperContainer.dataset.examPaperId);
const courseId = Number(questionBankContainer.dataset.courseId);
let questionListPaletteComponent;
let examPaperQuestionListComponent;


// Function expressions
const deleteQuestionHandler = (questionId = 0) => {
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

tabButtonContainer.addEventListener("click", event => {
    const clickedTabButton = event.target.closest(".tab-btn");
    if (!clickedTabButton)
        return;
    Array.from(tabButtonContainer.querySelectorAll(".tab-btn")).forEach(tabButton => {
        tabButton.classList.remove(..."bg-violet-200 text-violet-800".split(" "));
        tabButton.classList.add(..."text-violet-600 hover:text-violet-800".split(" "));
    });
    clickedTabButton.classList.remove(..."text-violet-600 hover:text-violet-800".split(" "));
    clickedTabButton.classList.add(..."bg-violet-200 text-violet-800".split(" "));
    if (clickedTabButton.dataset.tab === "preview") {
        editorContainer.classList.add("hidden");
        previewContainer.classList.remove("hidden");
        examPaperPreviewContainer.innerHTML = examPaperQuestionListComponent.getMarkup();
        buttonContainer.classList.remove("hidden");
    } else {
        examPaperPreviewContainer.innerHTML = "";
        previewContainer.classList.add("hidden");
        editorContainer.classList.remove("hidden");
        buttonContainer.classList.add("hidden");
    }
});

updateExamPaperButton.addEventListener("click", async () => {
    const buttonTextElement = updateExamPaperButton.querySelector(".button-text");
    const examPaperUpdate = new ExamPaperUpdate();
    examPaperUpdate.examPaperQuestions = examPaperQuestionListComponent.getExamPaperQuestionUpdates();
    try {
        
        showSpinnerForButton(buttonTextElement, updateExamPaperButton);
        await putData("exam-paper", examPaperId, examPaperUpdate);
        hideSpinnerForButton(updateExamPaperButton, buttonTextElement);
        document.location.href = `/exam-paper/${examPaperId}`;
    } catch (err) {
        console.error(err);
    }
});

// On load
(async () => {
    try {
        let res = await fetchData(`exam-paper/${examPaperId}/question`);
        const examPaperQuestions = [new ExamPaperQuestion()];
        Object.assign(examPaperQuestions, res.data);
        examPaperQuestionListComponent = new ExamPaperQuestionListComponent(examPaperQuestionListContainer, examPaperQuestions);
        examPaperQuestionListComponent.connectedCallback();      
        examPaperQuestionListComponent.subscribe("delete", deleteQuestionHandler);
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