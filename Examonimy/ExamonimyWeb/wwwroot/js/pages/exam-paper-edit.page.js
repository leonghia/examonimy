// Imports
import { fetchData } from "../helpers/ajax.helper.js";
import { QuestionRequestParams, RequestParams } from "../models/request-params.model.js";
import { QuestionListPaletteComponent } from "../components/question-list-palette.component.js";
import { ExamPaperQuestion } from "../models/exam-paper-question.model.js";
import { Question } from "../models/question.model.js";
import { ExamPaperQuestionListComponent } from "../components/exam-paper-question-list.component.js";

// DOM selectors
const examPaperContainer = document.querySelector("#exam-paper-container");
const questionListPaletteContainer = document.querySelector("#question-list-palette-container");
const examPaperQuestionListContainer = document.querySelector("#exam-paper-question-list-container");
const questionBankContainer = document.querySelector("#question-bank-container");

// States
const examPaperId = Number(examPaperContainer.dataset.examPaperId);
const courseId = Number(questionBankContainer.dataset.courseId);
let questionListPaletteComponent;
let examPaperQuestionListComponent;


// Function expressions
const deleteQuestionHandler = (questionId = 0) => {
    questionListPaletteComponent.removeQuestionIdFromDisabledListThenEnableIt(questionId);
}



// Event listeners


// On load
(async () => {
    try {
        let res = await fetchData(`exam-paper/${examPaperId}/question`);
        const examPaperQuestions = [new ExamPaperQuestion()];
        Object.assign(examPaperQuestions, res.data);
        examPaperQuestionListComponent = new ExamPaperQuestionListComponent(examPaperQuestionListContainer, examPaperQuestions);
        examPaperQuestionListComponent.connectedCallback();      
        examPaperQuestionListComponent.subscribe("delete", deleteQuestionHandler);


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