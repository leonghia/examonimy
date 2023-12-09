// Imports
import { CourseGridComponent } from "../components/course-grid.component.js";
import { changeHtmlBackgroundColorToGray, changeHtmlBackgroundColorToWhite } from "../helpers/markup.helper.js";
import { SimplePaginationComponent } from "../components/simple-pagination.component.js";
import { ExamPaperCreate } from "../models/exam-paper-create.model.js";
import { fetchCourses } from "../helpers/ajax.helper.js";
import { StepperComponent } from "../components/stepper.component.js";
import { Course } from "../models/course.model.js";
import { ExamPaper } from "../models/exam-paper.model.js";

// DOM selectors
const courseContainer = document.querySelector("#course-container");
const paginationContainerForCourses = document.querySelector("#pagination-container"); 
const stepperContainer = document.querySelector("#stepper-container");

// States
const courseGridComponent = new CourseGridComponent(courseContainer);
const paginationComponentForCourses = new SimplePaginationComponent(paginationContainerForCourses);
const stepperComponent = new StepperComponent(stepperContainer, ["Chọn môn học", "Nhập thông tin", "Thêm câu hỏi", "Xem trước"])
const examPaperCreate = new ExamPaperCreate();
const examPaper = new ExamPaper();
const pageSizeForCourses = 12;

// Function expressions
const onClickCourseHandler = (course = new Course()) => {
    examPaper.course = course;
}

const onNavigateHandler = async (pageNumber = 0) => {
    const coursePaginationMetadata = await fetchCourses(pageSizeForCourses, pageNumber);
    courseGridComponent.populateCourses(coursePaginationMetadata.courses);
    paginationComponentForCourses.populatePaginationInfo(coursePaginationMetadata.paginationMetadata.TotalPages);
}

const onClickStepperHandler = (stepOrder = 0) => {
    if (stepOrder === 1)
        changeHtmlBackgroundColorToWhite();
    else
        changeHtmlBackgroundColorToGray();
}

// Event listeners


// On load
changeHtmlBackgroundColorToWhite();
stepperComponent.connectedCallback();
stepperComponent.subscribe("onClick", onClickStepperHandler);
paginationComponentForCourses.subscribe("onNext", onNavigateHandler);
paginationComponentForCourses.subscribe("onPrev", onNavigateHandler);
courseGridComponent.subscribe("onClickCourse", onClickCourseHandler);

(async () => {
    const coursePaginationMetadata = await fetchCourses(pageSizeForCourses, 1);
    courseGridComponent.courses = coursePaginationMetadata.courses;
    courseGridComponent.connectedCallback();
    paginationComponentForCourses.currentPage = 1;
    paginationComponentForCourses.totalPages = coursePaginationMetadata.paginationMetadata.TotalPages;
    paginationComponentForCourses.connectedCallback();
})();