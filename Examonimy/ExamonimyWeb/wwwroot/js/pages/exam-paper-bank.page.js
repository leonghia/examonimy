// Imports
import { ExamPaperTableComponent } from "../components/exam-paper-table.component.js";
import { AdvancedPaginationComponent } from "../components/advanced-pagination.component.js";
import { ExamPaperRequestParams, RequestParams } from "../models/request-params.model.js";
import { deleteData, fetchData } from "../helpers/ajax.helper.js";
import { selectDropdownItem, toggleDropdown } from "../helpers/markup.helper.js";
import { ConfirmModalComponent } from "../components/confirm-modal.component.js";
import { BASE_URL } from "../config.js";

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
const requestParams = new ExamPaperRequestParams(null, null, null, 10, 1);
let modalComponent;

// Function expressions
const init = async (requestParams = new ExamPaperRequestParams()) => {
    const res = await fetchData("exam-paper", requestParams);
    const examPapers = res.data;
    examPaperTableComponent = new ExamPaperTableComponent(tableContainer, examPapers);
    examPaperTableComponent.connectedCallback();
    paginationComponent = new AdvancedPaginationComponent(paginationContainer, "đề thi", res.paginationMetadata.totalCount, res.paginationMetadata.pageSize, res.paginationMetadata.currentPage, res.paginationMetadata.totalPages);
    paginationComponent.connectedCallback();
}

const handler = async (requestParams = new ExamPaperRequestParams()) => {
    const res = await fetchData("exam-paper", requestParams);   
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
        requestParams.status = null;
    else
        requestParams.status = Number(value);
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
        requestParams.courseId = null;
    else
        requestParams.courseId = Number(value);
});

filterButton.addEventListener("click", () => {
    handler(requestParams);
});

searchForm.addEventListener("click", event => {
    event.preventDefault();
    requestParams.searchQuery = searchInput.value;
    handler(requestParams);
});

tableContainer.addEventListener("click", event => {
    const clickedDeleteButton = event.target.closest(".delete-btn");
    if (clickedDeleteButton) {
        const modalContainer = clickedDeleteButton.parentElement.querySelector(".modal-container");
        const examPaperId = Number(clickedDeleteButton.dataset.examPaperId);
        modalComponent = new ConfirmModalComponent(modalContainer, {
            title: "Xóa đề thi",
            description: "Bạn có chắc chắn muốn xóa đề thi này? Đề thi sau khi bị xóa sẽ không thể khôi phục lại.",
            ctaText: "Xác nhận"
        });
        modalComponent.connectedCallback();
        modalComponent.subscribe("confirm", async () => {
            try {
                await deleteData("exam-paper", examPaperId);
                document.location.reload();
            } catch (err) {
                console.error(err);
            }
        });
        modalComponent.subscribe("cancel", () => {
            modalComponent?.disconnectedCallback();
            modalComponent = null;
        });
    }
});


// On load
init(requestParams);