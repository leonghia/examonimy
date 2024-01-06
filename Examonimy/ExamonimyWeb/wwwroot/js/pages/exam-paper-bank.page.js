// Imports
import { ExamPaperTableComponent } from "../components/exam-paper-table.component.js";
import { AdvancedPaginationComponent } from "../components/advanced-pagination.component.js";
import { ExamPaperRequestParams, UserRequestParams } from "../models/request-params.model.js";
import { fetchData } from "../helpers/ajax.helper.js";
import { selectDropdownItem, toggleDropdown } from "../helpers/markup.helper.js";
import { RoleId } from "../helpers/user.helper.js";
import { MediaType } from "../helpers/media-type.helper.js";

// DOM selectors
const tableContainer = document.querySelector("#table-container");
const paginationContainer = document.querySelector("#pagination-container");
const statusDropdownButton = document.querySelector("#status-dropdown-btn");
const statusDropdown = document.querySelector("#status-dropdown");
const courseDropdownButton = document.querySelector("#course-dropdown-btn");
const courseDropdown = document.querySelector("#course-dropdown");
const filterButton = document.querySelector("#filter-btn");
const searchForm = document.querySelector("#search-form");
const searchInput = document.querySelector("#search-input");

// States
let examPaperTableComponent = new ExamPaperTableComponent(tableContainer);
let paginationComponent = new AdvancedPaginationComponent(paginationContainer);
const examPaperRequestParams = new ExamPaperRequestParams(null, null, null, 10, 1);
const teacherRequestParams = new UserRequestParams(RoleId.Teacher);

// Function expressions
const init = async (examPaperRequestParams = new ExamPaperRequestParams(), teacherRequestParams = new UserRequestParams()) => {
    const getExamPapersResponse = await fetchData("exam-paper", examPaperRequestParams, MediaType.ExamPaper.Full);
    const examPapers = getExamPapersResponse.data;  
    const getTeachersResponse = await fetchData("user", teacherRequestParams);
    const teachers = getTeachersResponse.data;
    examPaperTableComponent = new ExamPaperTableComponent(tableContainer, examPapers, teachers);
    examPaperTableComponent.connectedCallback();
    paginationComponent = new AdvancedPaginationComponent(paginationContainer, "đề thi", getExamPapersResponse.paginationMetadata.totalCount, getExamPapersResponse.paginationMetadata.pageSize, getExamPapersResponse.paginationMetadata.currentPage, getExamPapersResponse.paginationMetadata.totalPages);
    paginationComponent.connectedCallback();
}

const examPapersHandler = async (requestParams = new ExamPaperRequestParams()) => {
    const res = await fetchData("exam-paper", requestParams, MediaType.ExamPaper.Full);   
    examPaperTableComponent.examPapers = res.data;
    examPaperTableComponent.populateTableBody();
    paginationComponent.totalCount = res.paginationMetadata.totalCount;
    paginationComponent.pageSize = res.paginationMetadata.pageSize;
    paginationComponent.currentPage = res.paginationMetadata.currentPage;
    paginationComponent.totalPages = res.paginationMetadata.totalPages;
    paginationComponent.populatePagination();
}

// Event listeners
statusDropdownButton.addEventListener("click", () => {
    toggleDropdown(statusDropdown);
});
statusDropdown.addEventListener("click", event => {
    selectDropdownItem(event);
    const clicked = event.target.closest(".dropdown-item");
    if (!clicked)
        return;
    const value = clicked.querySelector("input").value;
    if (value === "all")
        examPaperRequestParams.status = null;
    else
        examPaperRequestParams.status = Number(value);
});

courseDropdownButton.addEventListener("click", () => {
    toggleDropdown(courseDropdown);
});
courseDropdown.addEventListener("click", event => {
    selectDropdownItem(event);
    const clicked = event.target.closest(".dropdown-item");
    if (!clicked)
        return;
    const value = clicked.querySelector("input").value;
    if (value === "all")
        examPaperRequestParams.courseId = null;
    else
        examPaperRequestParams.courseId = Number(value);
});

filterButton.addEventListener("click", () => {
    examPapersHandler(examPaperRequestParams);
});

searchForm.addEventListener("click", event => {
    event.preventDefault();
    examPaperRequestParams.searchQuery = searchInput.value;
    examPapersHandler(examPaperRequestParams);
});


// On load
init(examPaperRequestParams, teacherRequestParams);