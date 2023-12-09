// Imports
import { CourseGridComponent } from "../components/course-grid.component.js";
import { changeHtmlBackgroundColorToWhite } from "../helpers/markup.helper.js";
import { SimplePaginationComponent } from "../components/simple-pagination.component.js";
import { ExamPaperCreate } from "../models/exam-paper-create.model.js";
import { fetchCourses } from "../helpers/ajax.helper.js";

// DOM selectors
const courseContainer = document.querySelector("#course-container");
const paginationContainerForCourses = document.querySelector("#pagination-container"); 

// States
const courseGridComponent = new CourseGridComponent(courseContainer);
const paginationComponentForCourses = new SimplePaginationComponent();
const examPaperCreate = new ExamPaperCreate();
const pageSizeForCourses = 12;

// Function expressions

const populateCourses = async () => {
    const coursePaginationMetadata = await fetchCourses(pageSizeForCourses, paginationComponentForCourses.currentPage);
    courseGridComponent.courses = coursePaginationMetadata.courses;
    paginationComponentForCourses.totalPages = coursePaginationMetadata.paginationMetadata.TotalPages;
    courseContainer.innerHTML = courseGridComponent.render();
    paginationContainerForCourses.innerHTML = paginationComponentForCourses.render();
}

// Event listeners
courseContainer.addEventListener("click", event => {
    const clickedCourse = event.target.closest(".course-label");
    if (!clickedCourse)
        return;
    courseGridComponent.highlightCourse(clickedCourse);

    // Update the state for examPaperCreateDto
    examPaperCreate.courseId = Number(clickedCourse.dataset.id);
});

paginationContainerForCourses.addEventListener("click", event => {
    if (event.target.closest("#next-btn")) {
        if (paginationComponentForCourses.hasNext()) {
            paginationComponentForCourses.next();
            populateCourses();
        }
    }

    if (event.target.closest("#prev-btn")) {
        if (paginationComponentForCourses.hasPrev()) {
            paginationComponentForCourses.prev();
            populateCourses();
        }
    }
});

// On load
changeHtmlBackgroundColorToWhite();
populateCourses();