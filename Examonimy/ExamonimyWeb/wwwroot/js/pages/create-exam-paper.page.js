// Imports
import { BASE_API_URL, PAGINATION_METADATA_HEADER } from "../config.js";
import { CourseGridComponent } from "../components/course-grid.component.js";
import { Course } from "../models/course.model.js";
import { changeHtmlBackgroundColorToWhite } from "../helpers/markup.helper.js";
import { SimplePaginationComponent } from "../components/simple-pagination.component.js";
import { ExamPaperCreate } from "../models/exam-paper-create.model.js";

// DOM selectors
const courseContainer = document.querySelector("#course-container");
const paginationContainerForCourses = document.querySelector("#pagination-container"); 

// States
const courseGridComponent = new CourseGridComponent(courseContainer);
const paginationComponentForCourses = new SimplePaginationComponent();
const examPaperCreate = new ExamPaperCreate();
const pageSizeForCourses = 12;

// Function expressions
const fetchCourses = async (pageSize, pageNumber) => {
    const res = await fetch(`${BASE_API_URL}/course?pageSize=${pageSize}&pageNumber=${pageNumber}`, {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    });
    const data = await res.json();
    const courses = [new Course()];
    Object.assign(courses, data);

    // Update state for pagination component
    const paginationHeader = JSON.parse(res.headers.get(PAGINATION_METADATA_HEADER));
    paginationComponentForCourses.currentPage = paginationHeader.CurrentPage;
    paginationComponentForCourses.totalPages = paginationHeader.TotalPages;

    return courses;
}

const populateCourses = async () => {
    paginationContainerForCourses.classList.add("hidden");
    const courses = await fetchCourses(pageSizeForCourses, paginationComponentForCourses.currentPage);

    // Render courses
    courseContainer.innerHTML = courseGridComponent.render(courses);

    // Render pagination for courses
    paginationContainerForCourses.classList.remove("hidden");
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
        if (paginationComponentForCourses.currentPage === paginationComponentForCourses.totalPages)
            return;
        paginationComponentForCourses.currentPage++;
        populateCourses();
    }

    if (event.target.closest("#prev-btn")) {
        if (paginationComponentForCourses.currentPage === 1)
            return;
        paginationComponentForCourses.currentPage--;
        populateCourses();
    }
});

// On load
changeHtmlBackgroundColorToWhite();
populateCourses();