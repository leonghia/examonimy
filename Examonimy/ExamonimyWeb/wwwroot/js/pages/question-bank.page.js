// Imports
import { Question } from "../models/question.model.js";
import { GetResponse } from "../models/get-response.model.js";
import { fetchData } from "../helpers/ajax.helper.js";
import { RequestParams } from "../models/request-params.model.js";
import { QuestionTableComponent } from "../components/question-table.component.js";

// DOM selectors
const questionTypeDropdownButton = document.querySelector("#question-type-dropdown-btn");
const questionTypeDropdown = document.querySelector("#question-type-dropdown");
const selectedQuestionType = document.querySelector("#selected-question-type");
const questionTableContainer = document.querySelector("#question-table-container");


// States
let questions = [new Question()]
let questionTableComponent = new QuestionTableComponent(null, null);

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
        questions = getResponse.data;
        questionTableComponent = new QuestionTableComponent(questionTableContainer, questions);
        questionTableComponent.connectedCallback();
    } catch (err) {
        console.error(err);
    }
})();