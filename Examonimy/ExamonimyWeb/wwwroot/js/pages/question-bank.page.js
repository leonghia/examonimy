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


// States
let questions = [new Question()]
const questionTableComponent = new QuestionTableComponent(questionTableContainer);
const paginationComponent = new AdvancedPaginationComponent(paginationContainer, "câu hỏi");
const pageSizeForQuestions = 10;


// Function expressions
const navigateHandler = async (data) => {
    await init(null, data.pageNumber, data.fromItemNumber);
}

const init = async (searchQuery = null, pageNumber = 0, fromItemNumber = 0) => {
    const getResponse = new GetResponse();
    try {
        Object.assign(getResponse, await fetchData("question", new RequestParams(searchQuery, pageSizeForQuestions, pageNumber)));       
        questionTableComponent.questions = getResponse.data;        
        questionTableComponent.fromItemNumber = fromItemNumber;
        questionTableComponent.connectedCallback();

        if (questionTableComponent.questions.length > 0) {
            paginationComponent.setPaginationFields(getResponse.paginationMetadata.totalCount, getResponse.paginationMetadata.pageSize, getResponse.paginationMetadata.currentPage, getResponse.paginationMetadata.totalPages);
            paginationComponent.connectedCallback();
        } else {
            createQuestionButtonContainer.classList.add("hidden");
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
});

///////////////////////////////////////////////
searchForm.addEventListener("submit", event => {
    event.preventDefault();
    init(searchInput.value, 1, 1);
});


// On load
(async () => {
    await init(null, 1, 1);
})();

paginationComponent.subscribe("prev", navigateHandler);
paginationComponent.subscribe("next", navigateHandler);
paginationComponent.subscribe("clickPage", navigateHandler);