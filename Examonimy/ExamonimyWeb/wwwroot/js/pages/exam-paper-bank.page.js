// Imports
import { ExamPaperTableComponent } from "../components/exam-paper-table.component.js";
import { AdvancedPaginationComponent } from "../components/advanced-pagination.component.js";
import { RequestParams } from "../models/request-params.model.js";
import { fetchData } from "../helpers/ajax.helper.js";

// DOM selectors
const tableContainer = document.querySelector("#table-container");
const paginationContainer = document.querySelector("#pagination-container");


// States
let examPaperTableComponent = new ExamPaperTableComponent(tableContainer);
let paginationComponent = new AdvancedPaginationComponent(paginationContainer);
const requestParams = new RequestParams(null, 10, 1);
// Function expressions

// Event listeners

// On load
(async () => {
    const res = await fetchData("exam-paper", requestParams);
    const examPapers = res.data;
    examPaperTableComponent = new ExamPaperTableComponent(tableContainer, examPapers);
    examPaperTableComponent.connectedCallback();
    if (res.paginationMetadata.totalPages > 0) {
        paginationComponent = new AdvancedPaginationComponent(paginationContainer, "đề thi", res.paginationMetadata.totalCount, res.paginationMetadata.pageSize, res.paginationMetadata.currentPage, res.paginationMetadata.totalPages);
        paginationComponent.connectedCallback();
    }
})();