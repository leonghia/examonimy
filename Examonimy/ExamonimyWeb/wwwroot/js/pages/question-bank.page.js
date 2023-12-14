// Imports
import { Question } from "../models/question.model.js";
import { GetResponse } from "../models/get-response.model.js";
import { fetchData } from "../helpers/ajax.helper.js";
import { RequestParams } from "../models/request-params.model.js";
import { QuestionTableComponent } from "../components/question-table.component.js";
import { AdvancedPaginationComponent } from "../components/advanced-pagination.component.js";

// DOM selectors
const questionTypeDropdownButton = document.querySelector("#question-type-dropdown-btn");
const questionTypeDropdown = document.querySelector("#question-type-dropdown");
const selectedQuestionType = document.querySelector("#selected-question-type");
const questionTableContainer = document.querySelector("#question-table-container");
const paginationContainer = document.querySelector("#pagination-container");

// States
let questions = [new Question()]
const questionTableComponent = new QuestionTableComponent(questionTableContainer);
const paginationComponent = new AdvancedPaginationComponent(paginationContainer, "câu hỏi");

// Function expressions



// Event listeners
questionTypeDropdownButton.addEventListener("click", () => {
    if (questionTypeDropdown.classList.contains("opacity-0")) {
        questionTypeDropdown.classList.remove(..."opacity-0 pointer-events-none".split(" "));
    } else {
        questionTypeDropdown.classList.add(..."opacity-0 pointer-events-none".split(" "));
    }
});

questionTypeDropdown.addEventListener("click", event => {
    const clicked = event.target.closest("input");
    if (!clicked)
        return;
    selectedQuestionType.textContent = clicked.nextElementSibling.textContent;
});


// On load
(async () => {
    const getResponse = new GetResponse();
    try {
        Object.assign(getResponse, await fetchData("question", new RequestParams()));       
        questionTableComponent.questions = getResponse.data;
        questionTableComponent.connectedCallback();

        paginationComponent.setPaginationFields(getResponse.paginationMetadata.totalCount, getResponse.paginationMetadata.pageSize, getResponse.paginationMetadata.currentPage, getResponse.paginationMetadata.totalPages);
        paginationComponent.connectedCallback();
    } catch (err) {
        console.error(err);
    }
})();