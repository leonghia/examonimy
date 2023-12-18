// Imports
import { CourseGridComponent } from "../components/course-grid.component.js";
import { changeHtmlBackgroundColorToGray, changeHtmlBackgroundColorToWhite, hideSpinnerForButton, showSpinnerForButton } from "../helpers/markup.helper.js";
import { SimplePaginationComponent } from "../components/simple-pagination.component.js";
import { StepperComponent } from "../components/stepper.component.js";
import { Course } from "../models/course.model.js";
import { ExamPaper } from "../models/exam-paper.model.js";
import { fetchData, postData } from "../helpers/ajax.helper.js";
import { QuestionListPaletteComponent } from "../components/question-list-palette.component.js";
import { QuestionPreviewComponent } from "../components/question-preview.component.js";
import { RequestParams } from "../models/request-params.model.js";
import { ExamPaperQuestionCreate } from "../models/exam-paper-question-create.model.js";
import { ExamPaperCreate } from "../models/exam-paper-create.model.js";

// DOM selectors
const courseContainer = document.querySelector("#course-container");
const paginationContainerForCourses = document.querySelector("#pagination-container"); 
const stepperContainer = document.querySelector("#stepper-container");
const numbersOfQuestionInput = document.querySelector("#numbers-of-question");
const examPaperCodeInput = document.querySelector("#exam-paper-code");
const questionPreviewListContainer = document.querySelector("#question-preview-list-container");
const questionListPaletteContainer = document.querySelector("#question-list-palette-container");
const questionSampleListPreviewContainer = document.querySelector("#question-sample-list-preview-container");
const coursePreviewElement = document.querySelector("#course-preview-el");
const examPaperCodePreviewElement = document.querySelector("#exam-paper-code-preview-el");
const numbersOfQuestionPreviewElement = document.querySelector("#numbers-of-question-preview-el");
const buttonContainer = document.querySelector("#button-container");
const createExamPaperButton = document.querySelector("#create-exam-paper-btn");
const addEmptyQuestionButton = document.querySelector("#add-empty-question-btn");

// States
const courseGridComponent = new CourseGridComponent(courseContainer);
const paginationComponentForCourses = new SimplePaginationComponent(paginationContainerForCourses);
const stepperComponent = new StepperComponent(stepperContainer, ["Chọn môn học", "Nhập thông tin", "Thêm câu hỏi", "Xem trước"])
const examPaperCreate = new ExamPaperCreate();
let courseName = "";
let courseCode = "";
const pageSizeForCourses = 12;
const pageSizeForQuestions = 10;
const questionListPaletteComponent = new QuestionListPaletteComponent(questionListPaletteContainer);

const examPaperQuestionMap = new Map();
const examPaperQuestions = [];

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

const greenEmptyPlaceholder = (emptyPlaceholder = new HTMLElement()) => {
    emptyPlaceholder.classList.remove("border-gray-300");
    emptyPlaceholder.querySelector(".empty-icon").classList.remove("text-gray-400");
    emptyPlaceholder.querySelector(".empty-text").classList.remove("text-gray-400");
    emptyPlaceholder.classList.add("border-green-500");
    emptyPlaceholder.classList.add("bg-green-50");
    emptyPlaceholder.querySelector(".empty-icon").classList.add("text-green-400");
    emptyPlaceholder.querySelector(".empty-text").classList.add("text-green-400");
}

const grayEmptyPlaceholder = (emptyPlaceholder = new HTMLElement()) => {
    emptyPlaceholder.classList.remove("bg-green-50");
    emptyPlaceholder.classList.remove("border-green-500");
    emptyPlaceholder.querySelector(".empty-icon").classList.remove("text-green-400");
    emptyPlaceholder.querySelector(".empty-text").classList.remove("text-green-400");
    emptyPlaceholder.classList.add("border-gray-300");
    emptyPlaceholder.querySelector(".empty-icon").classList.add("text-gray-400");
    emptyPlaceholder.querySelector(".empty-text").classList.add("text-gray-400");
}

const populateCourseCodeForExamPaperCodeInput = (courseCode = "") => {
    document.querySelector("#course-code").textContent = courseCode;
}

const populateQuestionListForExamPaperPreview = (examPaperQuestionMap = new Map()) => {
    for (let i = 0; i < examPaper.numbersOfQuestion; i++) {
        questionSampleListPreviewContainer.insertAdjacentHTML("beforeend", `
        <div class="bg-white rounded-lg p-6">
            <p class="font-bold text-gray-900 mb-6">Câu ${i + 1}</p>
            <div id="question-sample-preview-${i + 1}"></div>
        </div>       
        `);
        new QuestionPreviewComponent(questionSampleListPreviewContainer.querySelector(`#question-sample-preview-${i + 1}`), examPaperQuestionMap.get(i + 1)).connectedCallback();
    }
} 

const populateExamPaperDetailPreview = (examPaper = new ExamPaper()) => {
    coursePreviewElement.textContent = examPaper.course.name;
    examPaperCodePreviewElement.textContent = examPaper.examPaperCode;
    numbersOfQuestionPreviewElement.textContent = examPaper.numbersOfQuestion;
}

const onClickStepperHandler = async (stepOrder = 0) => {
    if (stepOrder === 1)
        changeHtmlBackgroundColorToWhite();
    else
        changeHtmlBackgroundColorToGray();

    if (stepOrder === 2) {
        populateCourseCodeForExamPaperCodeInput(courseCode);
        const res = await fetchData("question", new RequestParams(null, pageSizeForQuestions));
        questionListPaletteComponent.questions = res.data
        questionListPaletteComponent.currentPage = res.paginationMetadata.currentPage;
        questionListPaletteComponent.totalPages = res.paginationMetadata.totalPages;
        questionListPaletteComponent.connectedCallback();
    }
        
    if (stepOrder === 3) {
        // update state for examPaper
        examPaperCreate.examPaperCode = courseCode + examPaperCodeInput.value;
    }

    if (stepOrder === 4) {
        populateExamPaperDetailPreview(examPaper);
        populateQuestionListForExamPaperPreview(examPaperQuestionMap);
        examPaperCreate = new ExamPaperCreate(examPaper.course.id, examPaper.examPaperCode, constructExamPaperQuestionsAsArray(examPaperQuestionMap));
        buttonContainer.classList.remove("hidden");
    } else {
        buttonContainer.classList.add("hidden");
    }
}

const constructExamPaperQuestionsAsArray = (examPaperQuestionMap = new Map()) => {
    const arr = new Array(examPaperQuestionMap.size);
    const entries = Array.from(examPaperQuestionMap.entries());

    for (let i = 0; i < entries.length; i++) {
        arr[i] = new ExamPaperQuestionCreate(entries[i][1].id, entries[i][0]);
    }

    return arr;
}

const postExamPaper = async (examPaperCreate = new ExamPaperCreate()) => {
    showSpinnerForButton(createExamPaperButton.querySelector(".button-text-el"), createExamPaperButton);
    try {
        await postData("exam-paper", examPaperCreate);      
    } catch (err) {
        console.error(err);
    } finally {
        hideSpinnerForButton(createExamPaperButton, createExamPaperButton.querySelector(".button-text-el"));
    }
}


// Event listeners
questionPreviewListContainer.addEventListener("dragenter", event => {
    event.preventDefault();
    if (event.target.matches(".empty-placeholder")) {
        greenEmptyPlaceholder(event.target);
    }
});

questionPreviewListContainer.addEventListener("dragleave", event => {
    event.preventDefault();
    if (event.target.matches(".empty-placeholder")) {
        grayEmptyPlaceholder(event.target);
    }
});

questionPreviewListContainer.addEventListener("dragover", event => {
    event.preventDefault();
    event.dataTransfer.dropEffect = "copy";
});

questionPreviewListContainer.addEventListener("drop", event => {
    event.preventDefault();
    if (event.target.matches(".empty-placeholder")) {
        const questionId = Number(event.dataTransfer.getData("text/plain"));
        var question = questionListPaletteComponent.questions.find(q => q.id === questionId);
        const questionSampleComponent = new QuestionPreviewComponent(event.target.parentElement.querySelector(".question-sample-placeholder"), question);
        questionSampleComponent.connectedCallback();
        event.target.classList.add("hidden");
        questionListPaletteComponent.unHighlightAllQuestions();
        questionListPaletteComponent.addQuestionIdToDisabledListThenDisableIt(questionId);

        // add the question to the map
        const questionNumber = Number(event.target.parentElement.dataset.questionNumber);
        examPaperQuestionMap.set(questionNumber, question);

        // show the clear button
        event.target.parentElement.querySelector(".clear-btn").classList.remove("hidden");
    }
});

questionPreviewListContainer.addEventListener("click", event => {
    if (event.target.closest(".clear-btn")) {
        const questionId = Number(event.target.closest(".empty-question").querySelector(".question-sample").dataset.questionId);
        questionListPaletteComponent.removeQuestionIdFromDisabledListThenEnableIt(questionId);
        event.target.closest(".empty-question").querySelector(".question-sample-placeholder").innerHTML = "";
        event.target.closest(".empty-question").querySelector(".empty-placeholder").classList.remove("hidden");
        event.target.closest(".clear-btn").classList.add("hidden");

        // unfocus the empty placeholder
        grayEmptyPlaceholder(event.target.closest(".empty-question").querySelector(".empty-placeholder"));

        // remove the question from the map
        examPaperQuestionMap.delete(questionId);
    }
});

createExamPaperButton.addEventListener("click", () => {
    postExamPaper(examPaperCreate);
});

addEmptyQuestionButton.addEventListener("click", () => {
    const currentNumbersOfQuestions = Array.from(questionPreviewListContainer.querySelectorAll(".empty-question")).length;
    questionPreviewListContainer.insertAdjacentHTML("beforeend", `
<div data-question-number="${currentNumbersOfQuestions + 1}" class="empty-question bg-white rounded-lg p-6">
    <div class="flex items-center justify-between mb-4">
        <p class="font-bold text-base text-gray-900">Câu ${currentNumbersOfQuestions + 1}</p>
        <button type="button" class="hidden clear-btn rounded bg-red-50 hover:bg-red-100 px-2 py-1" title="Gỡ câu hỏi">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="w-5 h-5 text-red-600">
                <path fill-rule="evenodd" d="M5.47 5.47a.75.75 0 011.06 0L12 10.94l5.47-5.47a.75.75 0 111.06 1.06L13.06 12l5.47 5.47a.75.75 0 11-1.06 1.06L12 13.06l-5.47 5.47a.75.75 0 01-1.06-1.06L10.94 12 5.47 6.53a.75.75 0 010-1.06z" clip-rule="evenodd" />
            </svg>
        </button>
    </div>
    <div class="empty-placeholder relative block w-full rounded-lg border-2 border-dashed border-gray-300 p-12 text-center hover:border-gray-400 focus:outline-none focus:ring-0 focus:ring-indigo-500 focus:ring-offset-2">
        <svg class="empty-icon pointer-events-none mx-auto h-12 w-12 text-gray-400" stroke="currentColor" fill="none" viewBox="0 0 48 48" aria-hidden="true">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 14v20c0 4.418 7.163 8 16 8 1.381 0 2.721-.087 4-.252M8 14c0 4.418 7.163 8 16 8s16-3.582 16-8M8 14c0-4.418 7.163-8 16-8s16 3.582 16 8m0 0v14m0-4c0 4.418-7.163 8-16 8S8 28.418 8 24m32 10v6m0 0v6m0-6h6m-6 0h-6" />
        </svg>
        <span class="empty-text pointer-events-none mt-2 block text-sm font-semibold text-gray-400">Chưa có nội dung</span>
    </div>
    <div class="question-sample-placeholder">
    </div>
</div>
    `);
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