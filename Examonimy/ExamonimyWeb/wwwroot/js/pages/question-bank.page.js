// Imports
import { Question } from "../models/question.model.js";
import { GetResponse } from "../models/get-response.model.js";
import { fetchData } from "../helpers/ajax.helper.js";
import { QuestionRequestParams, RequestParams } from "../models/request-params.model.js";
import { QuestionTableComponent } from "../components/question-table.component.js";
import { AdvancedPaginationComponent } from "../components/advanced-pagination.component.js";
import { calculateFromItemNumber } from "../helpers/number.helper.js";


// DOM selectors
const questionTypeDropdownButton = document.querySelector("#question-type-dropdown-btn");
const questionTypeDropdown = document.querySelector("#question-type-dropdown");
const selectedQuestionType = document.querySelector("#selected-question-type");

const questionLevelDropdownButton = document.querySelector("#question-level-dropdown-btn");
const questionLevelDropdown = document.querySelector("#question-level-dropdown");
const selectedQuestionLevel = document.querySelector("#selected-question-level");

const courseDropdownButton = document.querySelector("#course-dropdown-btn");
const courseDropdown = document.querySelector("#course-dropdown");
const selectedCourse = document.querySelector("#selected-course");

const questionTableContainer = document.querySelector("#question-table-container");
const paginationContainer = document.querySelector("#pagination-container");
const createQuestionButtonContainer = document.querySelector("#create-question-btn-container");
const searchForm = document.querySelector("#search-form");
const searchInput = document.querySelector("#search-input");

const filterButton = document.querySelector("#filter-btn");


// States
let questions = [new Question()]
const questionTableComponent = new QuestionTableComponent(questionTableContainer);
const paginationComponent = new AdvancedPaginationComponent(paginationContainer, "câu hỏi");
const pageSizeForQuestions = 10;
const requestParams = new QuestionRequestParams(null, null, null, null, pageSizeForQuestions, 1);

// Function expressions
const navigateHandler = async (pageNumber) => {
    requestParams.pageNumber = pageNumber;
    await init(requestParams);
}

const init = async (requestParams = new QuestionRequestParams()) => {
    const getResponse = new GetResponse();
    try {
        Object.assign(getResponse, await fetchData("question", requestParams));       
        questionTableComponent.questions = getResponse.data;        
        questionTableComponent.fromItemNumber = calculateFromItemNumber(requestParams.pageSize, requestParams.pageNumber);
        questionTableComponent.connectedCallback();

        if (questionTableComponent.questions.length > 0) {
            paginationComponent.setPaginationFields(getResponse.paginationMetadata.totalCount, getResponse.paginationMetadata.pageSize, getResponse.paginationMetadata.currentPage, getResponse.paginationMetadata.totalPages);
            paginationComponent.connectedCallback();
        } else {
            paginationComponent.disconnectedCallback();
        }
        
    } catch (err) {
        console.error(err);
    }
}

// Event listeners
questionTypeDropdownButton.addEventListener("click", () => {
    questionTypeDropdown.classList.toggle("hidden");
});
questionTypeDropdown.addEventListener("click", event => {   
    const clicked = event.target.closest("input");
    if (!clicked)
        return;
    selectedQuestionType.textContent = clicked.nextElementSibling.textContent;
    requestParams.questionTypeId = Number(clicked.value);
});

////////////////////////////////////////////////
questionLevelDropdownButton.addEventListener("click", () => {
    questionLevelDropdown.classList.toggle("hidden");
});
questionLevelDropdown.addEventListener("click", event => {
    const clicked = event.target.closest("input");
    if (!clicked)
        return;
    selectedQuestionLevel.textContent = clicked.nextElementSibling.textContent;
    requestParams.questionLevelId = Number(clicked.value);
});

////////////////////////////////////////////////
courseDropdownButton.addEventListener("click", () => {
    courseDropdown.classList.toggle("hidden");
});
courseDropdown.addEventListener("click", event => {
    const clicked = event.target.closest("input");
    if (!clicked)
        return;
    selectedCourse.textContent = clicked.nextElementSibling.textContent;
    requestParams.courseId = Number(clicked.value);
});

///////////////////////////////////////////////
searchForm.addEventListener("submit", event => {
    event.preventDefault();
    requestParams.searchQuery = searchInput.value;
    init(requestParams);
});

//////////////////////////////////////////////
filterButton.addEventListener("click", () => {
    init(requestParams);
});

// On load
(async () => {
    await init(requestParams);
})();

paginationComponent.subscribe("prev", navigateHandler);
paginationComponent.subscribe("next", navigateHandler);
paginationComponent.subscribe("clickPage", navigateHandler);