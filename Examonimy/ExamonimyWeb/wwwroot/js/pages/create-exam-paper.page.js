// Imports
import { CourseGridComponent } from "../components/course-grid.component.js";
import { changeHtmlBackgroundColorToGray, changeHtmlBackgroundColorToWhite } from "../helpers/markup.helper.js";
import { SimplePaginationComponent } from "../components/simple-pagination.component.js";
import { ExamPaperCreate } from "../models/exam-paper-create.model.js";
import { StepperComponent } from "../components/stepper.component.js";
import { Course } from "../models/course.model.js";
import { ExamPaper } from "../models/exam-paper.model.js";
import { fetchData } from "../helpers/ajax.helper.js";
import { QuestionListPaletteComponent } from "../components/question-list-palette.component.js";
import { QuestionPreviewComponent } from "../components/question-preview.component.js";
import { QuestionSampleComponent } from "../components/question-sample.component.js";

// DOM selectors
const courseContainer = document.querySelector("#course-container");
const paginationContainerForCourses = document.querySelector("#pagination-container"); 
const stepperContainer = document.querySelector("#stepper-container");
const numbersOfQuestionInput = document.querySelector("#numbers-of-question");
const questionSampleListContainer = document.querySelector("#question-sample-list-container");
const questionListPaletteContainer = document.querySelector("#question-list-palette-container");

// States
const courseGridComponent = new CourseGridComponent(courseContainer);
const paginationComponentForCourses = new SimplePaginationComponent(paginationContainerForCourses);
const stepperComponent = new StepperComponent(stepperContainer, ["Chọn môn học", "Nhập thông tin", "Thêm câu hỏi", "Xem trước"])
const examPaper = new ExamPaper();
const pageSizeForCourses = 12;
const pageSizeForQuestions = 10;
const questionListPaletteComponent = new QuestionListPaletteComponent(questionListPaletteContainer);

// Function expressions
const onClickCourseHandler = (course = new Course()) => {
    examPaper.course = course;
}

const navigateHandler = async (pageNumber = 0) => {
    const coursePaginationMetadata = await fetchData("course", pageSizeForCourses, pageNumber);
    courseGridComponent.populateCourses(coursePaginationMetadata.data);
    paginationComponentForCourses.populatePaginationInfo(coursePaginationMetadata.paginationMetadata.totalPages);
}

questionSampleListContainer.addEventListener("dragenter", event => {
    event.preventDefault();
    if (event.target.matches(".empty-placeholder")) {
        event.target.classList.remove("border-gray-300");
        event.target.classList.add("border-green-500");
        event.target.classList.add("bg-green-50");
    }
});

questionSampleListContainer.addEventListener("dragleave", event => {
    event.preventDefault();
    if (event.target.matches(".empty-placeholder")) {
        event.target.classList.remove("bg-green-50");
        event.target.classList.remove("border-green-500");
        event.target.classList.add("border-gray-300");
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
        const questionSampleComponent = new QuestionSampleComponent(event.target.parentElement.querySelector(".question-placeholder"), questionListPaletteComponent.questions.find(q => q.id === questionId));
        questionSampleComponent.connectedCallback();
        event.target.classList.add("hidden");
        
    }  
});

const populateEmptyQuestions = (numbersOfQuestion = 0) => {
    questionSampleListContainer.innerHTML = "";
    for (let i = 0; i < numbersOfQuestion; i++) {
        questionSampleListContainer.insertAdjacentHTML("beforeend", `
<div data-number="${i + 1}" class="empty-question bg-white rounded-lg p-6">
    <p class="font-bold text-base text-gray-900 mb-4">Câu ${i + 1}</p>
    <div class="empty-placeholder relative block w-full rounded-lg border-2 border-dashed border-gray-300 p-12 text-center hover:border-gray-400 focus:outline-none focus:ring-0 focus:ring-indigo-500 focus:ring-offset-2">
        <svg class="empty-icon pointer-events-none mx-auto h-12 w-12 text-gray-400" stroke="currentColor" fill="none" viewBox="0 0 48 48" aria-hidden="true">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 14v20c0 4.418 7.163 8 16 8 1.381 0 2.721-.087 4-.252M8 14c0 4.418 7.163 8 16 8s16-3.582 16-8M8 14c0-4.418 7.163-8 16-8s16 3.582 16 8m0 0v14m0-4c0 4.418-7.163 8-16 8S8 28.418 8 24m32 10v6m0 0v6m0-6h6m-6 0h-6" />
        </svg>
        <span class="empty-text pointer-events-none mt-2 block text-sm font-semibold text-gray-400">Chưa có câu hỏi</span>
    </div>
    <div class="question-placeholder">

    </div>
</div>
        `);
    }
}

const populateCourseCodeForExamPaperCodeInput = (courseCode = "") => {
    document.querySelector("#course-code").textContent = courseCode;
}

const onClickStepperHandler = async (stepOrder = 0) => {
    if (stepOrder === 1)
        changeHtmlBackgroundColorToWhite();
    else
        changeHtmlBackgroundColorToGray();
    if (stepOrder === 2) {
        populateCourseCodeForExamPaperCodeInput(examPaper.course.courseCode);
        const res = await fetchData("question", pageSizeForQuestions);
        questionListPaletteComponent.questions = res.data
        questionListPaletteComponent.currentPage = res.paginationMetadata.currentPage;
        questionListPaletteComponent.totalPages = res.paginationMetadata.totalPages;
        questionListPaletteComponent.connectedCallback();
    }
        
    if (stepOrder === 3) {
        examPaper.numbersOfQuestion = Number(numbersOfQuestionInput.value);   
        populateEmptyQuestions(examPaper.numbersOfQuestion);
    }
}

// Event listeners


// On load
changeHtmlBackgroundColorToWhite();
stepperComponent.connectedCallback();
stepperComponent.subscribe("onClick", onClickStepperHandler);
paginationComponentForCourses.subscribe("onNext", navigateHandler);
paginationComponentForCourses.subscribe("onPrev", navigateHandler);
courseGridComponent.subscribe("onClickCourse", onClickCourseHandler);

(async () => {
    const coursePaginationMetadata = await fetchData("course", pageSizeForCourses, 1);
    courseGridComponent.courses = coursePaginationMetadata.data;
    courseGridComponent.connectedCallback();
    paginationComponentForCourses.currentPage = 1;
    paginationComponentForCourses.totalPages = coursePaginationMetadata.paginationMetadata.totalPages;
    paginationComponentForCourses.connectedCallback();
})();