// Imports
import { CourseGridComponent } from "../components/course-grid.component.js";
import { changeHtmlBackgroundColorToGray, changeHtmlBackgroundColorToWhite, hideSpinnerForButton, showSpinnerForButton } from "../helpers/markup.helper.js";
import { SimplePaginationComponent } from "../components/simple-pagination.component.js";
import { StepperComponent } from "../components/stepper.component.js";
import { Course } from "../models/course.model.js";
import { fetchData, postData } from "../helpers/ajax.helper.js";
import { QuestionListPaletteComponent } from "../components/question-list-palette.component.js";
import { QuestionRequestParams, RequestParams } from "../models/request-params.model.js";
import { ExamPaperCreate } from "../models/exam-paper-create.model.js";
import { Question } from "../models/question.model.js";
import { ExamPaperQuestionListComponent } from "../components/exam-paper-question-list.component.js";

// DOM selectors
const courseContainer = document.querySelector("#course-container");
const paginationContainerForCourses = document.querySelector("#pagination-container"); 
const stepperContainer = document.querySelector("#stepper-container");
const examPaperCodeInput = document.querySelector("#exam-paper-code");
const questionListPaletteContainer = document.querySelector("#question-list-palette-container");
const coursePreviewElement = document.querySelector("#course-preview-el");
const examPaperCodePreviewElement = document.querySelector("#exam-paper-code-preview-el");
const numbersOfQuestionPreviewElement = document.querySelector("#numbers-of-question-preview-el");
const buttonContainer = document.querySelector("#button-container");
const createExamPaperButton = document.querySelector("#create-exam-paper-btn");
const examPaperQuestionListContainer = document.querySelector("#exam-paper-question-list-container");
const addEmptyQuestionButton = document.querySelector("#add-empty-question-btn");
const examPaperQuestionListPreviewContainer = document.querySelector("#exam-paper-question-list-preview-container");

// States
const courseGridComponent = new CourseGridComponent(courseContainer);
const paginationComponentForCourses = new SimplePaginationComponent(paginationContainerForCourses);
const stepperComponent = new StepperComponent(stepperContainer, ["Chọn môn học", "Nhập thông tin", "Thêm câu hỏi", "Xem trước"])
const examPaperCreate = new ExamPaperCreate();
let courseName = "";
let courseCode = "";
const pageSizeForCourses = 12;
let questionListPaletteComponent;
let examPaperQuestionListComponent;
const questionRequestParams = new QuestionRequestParams(null, null, null, null, 10, 1);

const addedQuestions = [new Question()];
addedQuestions.pop();

// Function expressions
const clickCourseHandler = (course = new Course()) => {
    examPaperCreate.courseId = course.id;
    courseName = course.name;
    courseCode = course.courseCode;
}

const navigateCoursesHandler = async (pageNumberForCourses = 0) => {
    const coursePaginationMetadata = await fetchData("course", new RequestParams(null, pageSizeForCourses, pageNumberForCourses));
    courseGridComponent.populateCourses(coursePaginationMetadata.data);
    paginationComponentForCourses.populatePaginationInfo(coursePaginationMetadata.paginationMetadata.totalPages);
}

const populateCourseCodeForExamPaperCodeInput = (courseCode = "") => {
    document.querySelector("#course-code").textContent = courseCode;
}

const populateExamPaperDetailPreview = (examPaperCreate = new ExamPaperCreate(), courseName = "") => {
    coursePreviewElement.textContent = courseName;
    examPaperCodePreviewElement.textContent = examPaperCreate.examPaperCode;
    numbersOfQuestionPreviewElement.textContent = examPaperCreate.examPaperQuestions.length;
}

const deleteQuestionHandler = (questionId) => {
    questionListPaletteComponent.enableQuestion(questionId);
}

const dropQuestionHandler = (data = { questionId, questionNumber }) => {
    const question = questionListPaletteComponent.disableQuestion(data.questionId);
    examPaperQuestionListComponent.populateQuestion(data.questionNumber, question);
    questionListPaletteComponent.unHighlightAllQuestions();
}

const onClickStepperHandler = async (stepOrder = 0) => {
    if (stepOrder === 1)
        changeHtmlBackgroundColorToWhite();
    else
        changeHtmlBackgroundColorToGray();

    if (stepOrder === 2) {
        populateCourseCodeForExamPaperCodeInput(courseCode);        
    }
        
    if (stepOrder === 3) {

        if (!examPaperQuestionListComponent) {
            examPaperQuestionListComponent = new ExamPaperQuestionListComponent(examPaperQuestionListContainer, null);
            examPaperQuestionListComponent.connectedCallback();
            examPaperQuestionListComponent.subscribe("delete", deleteQuestionHandler);
            examPaperQuestionListComponent.subscribe("drop", dropQuestionHandler);
        }

        // update state for examPaper
        examPaperCreate.examPaperCode = courseCode + examPaperCodeInput.value;
        questionRequestParams.courseId = examPaperCreate.courseId;  
        const res = await fetchData("question", questionRequestParams);
        questionListPaletteComponent = new QuestionListPaletteComponent(questionListPaletteContainer, res.data);      
        questionListPaletteComponent.currentPage = res.paginationMetadata.currentPage;
        questionListPaletteComponent.totalPages = res.paginationMetadata.totalPages;
        questionListPaletteComponent.courseName = courseName;
        questionListPaletteComponent.connectedCallback();   
    }

    if (stepOrder === 4) {
        examPaperCreate.examPaperQuestions = examPaperQuestionListComponent.getExamPaperQuestionCreates();
        populateExamPaperDetailPreview(examPaperCreate, courseName);
        examPaperQuestionListPreviewContainer.innerHTML = examPaperQuestionListComponent.getMarkup();
        examPaperQuestionListPreviewContainer.querySelectorAll(".delete-btn").forEach(v => v.remove());
        buttonContainer.classList.remove("hidden");
    } else {
        buttonContainer.classList.add("hidden");
    }
}


const postExamPaper = async (examPaperCreate = new ExamPaperCreate()) => {
    showSpinnerForButton(createExamPaperButton.querySelector(".button-text-el"), createExamPaperButton);
    try {
        await postData("exam-paper", examPaperCreate);      
        document.location.href = "/exam-paper";
    } catch (err) {
        console.error(err);
    } finally {
        hideSpinnerForButton(createExamPaperButton, createExamPaperButton.querySelector(".button-text-el"));
    }
}


// Event listeners
addEmptyQuestionButton.addEventListener("click", () => {
    examPaperQuestionListComponent.addEmptyQuestion();
});


createExamPaperButton.addEventListener("click", () => {
    postExamPaper(examPaperCreate);
});

// On load
changeHtmlBackgroundColorToWhite();
stepperComponent.connectedCallback();
stepperComponent.subscribe("click", onClickStepperHandler);
paginationComponentForCourses.subscribe("next", navigateCoursesHandler);
paginationComponentForCourses.subscribe("prev", navigateCoursesHandler);
courseGridComponent.subscribe("click", clickCourseHandler);

(async () => {
    const coursePaginationMetadata = await fetchData("course", new RequestParams(null, pageSizeForCourses, 1));
    courseGridComponent.courses = coursePaginationMetadata.data;
    courseGridComponent.connectedCallback();
    paginationComponentForCourses.currentPage = 1;
    paginationComponentForCourses.totalPages = coursePaginationMetadata.paginationMetadata.totalPages;
    paginationComponentForCourses.connectedCallback();
})();