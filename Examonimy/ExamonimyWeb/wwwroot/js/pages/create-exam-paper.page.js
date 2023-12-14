// Imports
import { CourseGridComponent } from "../components/course-grid.component.js";
import { changeHtmlBackgroundColorToGray, changeHtmlBackgroundColorToWhite } from "../helpers/markup.helper.js";
import { SimplePaginationComponent } from "../components/simple-pagination.component.js";
import { StepperComponent } from "../components/stepper.component.js";
import { Course } from "../models/course.model.js";
import { ExamPaper } from "../models/exam-paper.model.js";
import { fetchData } from "../helpers/ajax.helper.js";
import { QuestionListPaletteComponent } from "../components/question-list-palette.component.js";
import { QuestionSampleComponent } from "../components/question-sample.component.js";
import { RequestParams } from "../models/request-params.model.js";
import { ExamPaperQuestionCreate } from "../models/exam-paper-question-create.model.js";
import { ExamPaperCreate } from "../models/exam-paper-create.model.js";

// DOM selectors
const courseContainer = document.querySelector("#course-container");
const paginationContainerForCourses = document.querySelector("#pagination-container"); 
const stepperContainer = document.querySelector("#stepper-container");
const numbersOfQuestionInput = document.querySelector("#numbers-of-question");
const examPaperCodeInput = document.querySelector("#exam-paper-code");
const questionSampleListContainer = document.querySelector("#question-sample-list-container");
const questionListPaletteContainer = document.querySelector("#question-list-palette-container");
const questionSampleListPreviewContainer = document.querySelector("#question-sample-list-preview-container");
const coursePreviewElement = document.querySelector("#course-preview-el");
const examPaperCodePreviewElement = document.querySelector("#exam-paper-code-preview-el");
const numbersOfQuestionPreviewElement = document.querySelector("#numbers-of-question-preview-el");

// States
const courseGridComponent = new CourseGridComponent(courseContainer);
const paginationComponentForCourses = new SimplePaginationComponent(paginationContainerForCourses);
const stepperComponent = new StepperComponent(stepperContainer, ["Chọn môn học", "Nhập thông tin", "Thêm câu hỏi", "Xem trước"])
const examPaper = new ExamPaper();
let examPaperCreate;
const pageSizeForCourses = 12;
const pageSizeForQuestions = 10;
const questionListPaletteComponent = new QuestionListPaletteComponent(questionListPaletteContainer);

const examPaperQuestionMap = new Map();

// Function expressions
const clickCourseHandler = (course = new Course()) => {
    examPaper.course = course;
}

const navigateCoursesHandler = async (pageNumberForCourses = 0) => {
    const coursePaginationMetadata = await fetchData("course", new RequestParams(null, pageSizeForCourses, pageNumberForCourses));
    courseGridComponent.populateCourses(coursePaginationMetadata.data);
    paginationComponentForCourses.populatePaginationInfo(coursePaginationMetadata.paginationMetadata.totalPages);
}

const greenEmptyPlaceholder = (emptyPlaceholder = new HTMLElement()) => {
    emptyPlaceholder.classList.remove("border-gray-300");
    emptyPlaceholder.classList.add("border-green-500");
    emptyPlaceholder.classList.add("bg-green-50");
}

const grayEmptyPlaceholder = (emptyPlaceholder = new HTMLElement()) => {
    emptyPlaceholder.classList.remove("bg-green-50");
    emptyPlaceholder.classList.remove("border-green-500");
    emptyPlaceholder.classList.add("border-gray-300");
}

questionSampleListContainer.addEventListener("dragenter", event => {
    event.preventDefault();
    if (event.target.matches(".empty-placeholder")) {
        greenEmptyPlaceholder(event.target);
    }
});

questionSampleListContainer.addEventListener("dragleave", event => {
    event.preventDefault();
    if (event.target.matches(".empty-placeholder")) {
        grayEmptyPlaceholder(event.target);
    }
});

questionSampleListContainer.addEventListener("dragover", event => {
    event.preventDefault();
    event.dataTransfer.dropEffect = "copy";
});

questionSampleListContainer.addEventListener("drop", event => {
    event.preventDefault();
    if (event.target.matches(".empty-placeholder")) {
        const questionId = Number(event.dataTransfer.getData("text/plain"));
        var question = questionListPaletteComponent.questions.find(q => q.id === questionId);
        const questionSampleComponent = new QuestionSampleComponent(event.target.parentElement.querySelector(".question-sample-placeholder"), question);
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

questionSampleListContainer.addEventListener("click", event => {
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

const populateEmptyQuestions = (numbersOfQuestion = 0) => {
    questionSampleListContainer.innerHTML = "";
    for (let i = 0; i < numbersOfQuestion; i++) {
        questionSampleListContainer.insertAdjacentHTML("beforeend", `
<div data-question-number="${i + 1}" class="empty-question bg-white rounded-lg p-6">
    <div class="flex items-center justify-between mb-4">
        <p class="font-bold text-base text-gray-900">Câu ${i + 1}</p>
        <button type="button" class="hidden clear-btn rounded bg-red-50 px-2 py-1" title="Gỡ câu hỏi">
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-5 h-5 text-red-600">
                <path stroke-linecap="round" stroke-linejoin="round" d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0" />
            </svg>
        </button>
    </div>
    <div class="empty-placeholder relative block w-full rounded-lg border-2 border-dashed border-gray-300 p-12 text-center hover:border-gray-400 focus:outline-none focus:ring-0 focus:ring-indigo-500 focus:ring-offset-2">
        <svg class="empty-icon pointer-events-none mx-auto h-12 w-12 text-gray-400" stroke="currentColor" fill="none" viewBox="0 0 48 48" aria-hidden="true">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 14v20c0 4.418 7.163 8 16 8 1.381 0 2.721-.087 4-.252M8 14c0 4.418 7.163 8 16 8s16-3.582 16-8M8 14c0-4.418 7.163-8 16-8s16 3.582 16 8m0 0v14m0-4c0 4.418-7.163 8-16 8S8 28.418 8 24m32 10v6m0 0v6m0-6h6m-6 0h-6" />
        </svg>
        <span class="empty-text pointer-events-none mt-2 block text-sm font-semibold text-gray-400">Chưa có câu hỏi</span>
    </div>
    <div class="question-sample-placeholder">

    </div>
</div>
        `);
    }
}

const populateCourseCodeForExamPaperCodeInput = (courseCode = "") => {
    document.querySelector("#course-code").textContent = courseCode;
}

const populateQuestionSampleListPreview = (examPaperQuestionMap = new Map()) => {
    for (let i = 0; i < examPaper.numbersOfQuestion; i++) {
        questionSampleListPreviewContainer.insertAdjacentHTML("beforeend", `
        <div class="bg-white rounded-lg p-6">
            <p class="font-bold text-gray-900 mb-6">Câu ${i + 1}</p>
            <div id="question-sample-preview-${i + 1}"></div>
        </div>       
        `);
        new QuestionSampleComponent(questionSampleListPreviewContainer.querySelector(`#question-sample-preview-${i + 1}`), examPaperQuestionMap.get(i + 1)).connectedCallback();
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
        populateCourseCodeForExamPaperCodeInput(examPaper.course.courseCode);
        const res = await fetchData("question", new RequestParams(null, pageSizeForQuestions));
        questionListPaletteComponent.questions = res.data
        questionListPaletteComponent.currentPage = res.paginationMetadata.currentPage;
        questionListPaletteComponent.totalPages = res.paginationMetadata.totalPages;
        questionListPaletteComponent.connectedCallback();
    }
        
    if (stepOrder === 3) {
        // update state for examPaper
        examPaper.examPaperCode = examPaper.course.courseCode + examPaperCodeInput.value;
        examPaper.numbersOfQuestion = Number(numbersOfQuestionInput.value);   
        populateEmptyQuestions(examPaper.numbersOfQuestion);
    }

    if (stepOrder === 4) {     
        populateExamPaperDetailPreview(examPaper);
        populateQuestionSampleListPreview(examPaperQuestionMap);
        examPaperCreate = new ExamPaperCreate(examPaper.course.id, examPaper.examPaperCode, constructExamPaperQuestionsAsArray(examPaperQuestionMap));      
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

// Event listeners


// On load
changeHtmlBackgroundColorToWhite();
stepperComponent.connectedCallback();
stepperComponent.subscribe("onClick", onClickStepperHandler);
paginationComponentForCourses.subscribe("onNext", navigateCoursesHandler);
paginationComponentForCourses.subscribe("onPrev", navigateCoursesHandler);
courseGridComponent.subscribe("onClickCourse", clickCourseHandler);

(async () => {
    const coursePaginationMetadata = await fetchData("course", new RequestParams(null, pageSizeForCourses, 1));
    courseGridComponent.courses = coursePaginationMetadata.data;
    courseGridComponent.connectedCallback();
    paginationComponentForCourses.currentPage = 1;
    paginationComponentForCourses.totalPages = coursePaginationMetadata.paginationMetadata.totalPages;
    paginationComponentForCourses.connectedCallback();
})();