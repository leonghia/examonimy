// Imports
import { fetchDataById } from "../helpers/ajax.helper.js";
import { changeHtmlBackgroundColorToWhite, selectDropdownItem, toggleDropdown } from "../helpers/markup.helper.js";
import { QuestionUpdate } from "../models/question-update.model.js";
import { getTinyMCEOption } from "../helpers/tinymce.helper.js";
import { MultipleChoiceQuestionWithOneCorrectAnswer, Question, QuestionType } from "../models/question.model.js";
import { QuestionTypeIDs, QuestionTypeIdQuestionCreateDtoConstructorMappings } from "../helpers/question.helper.js";

// DOM selectors
const questionLevelDropdownButton = document.querySelector("#question-level-dropdown-btn");
const questionLevelDropdown = document.querySelector("#question-level-dropdown");
const questionSpecificSection = document.querySelector("#question-specific-section");


// States
const temp = window.location.href.split("/");
const questionId = Number(temp[temp.length - 1]);
let question = new Question();
const questionUpdate = new QuestionUpdate();

// Function expressions
const renderMultipleChoiceQuestionWithOneCorrectAnswerSection = async (question = new MultipleChoiceQuestionWithOneCorrectAnswer()) => {
    questionSpecificSection.innerHTML = `
    <div id="choice-container" class="flex flex-col gap-y-8">
        <div id="choice-a">
            <label for="choice-a-editor" class="block text-sm leading-6 text-gray-700 font-medium mb-3">Phương án A</label>
            <textarea id="choice-a-editor"></textarea>
        </div>
        <div id="choice-b">
            <label for="choice-b-editor" class="block text-sm leading-6 text-gray-700 font-medium mb-3">Phương án B</label>
            <textarea id="choice-b-editor"></textarea>
        </div>
        <div id="choice-c">
            <label for="choice-c-editor" class="block text-sm leading-6 text-gray-700 font-medium mb-3">Phương án C</label>
            <textarea id="choice-c-editor"></textarea>
        </div>
        <div id="choice-d">
            <label for="choice-d-editor" class="block text-sm leading-6 text-gray-700 font-medium mb-3">Phương án D</label>
            <textarea id="choice-d-editor"></textarea>
        </div>
    </div>
    `;
    await tinymce.init(getTinyMCEOption("#choice-a-editor", 150));
    await tinymce.init(getTinyMCEOption("#choice-b-editor", 150));
    await tinymce.init(getTinyMCEOption("#choice-c-editor", 150));
    await tinymce.init(getTinyMCEOption("#choice-d-editor", 150));
    tinymce.get("choice-a-editor").setContent(question.choiceA);
    tinymce.get("choice-b-editor").setContent(question.choiceB);
    tinymce.get("choice-c-editor").setContent(question.choiceC);
    tinymce.get("choice-d-editor").setContent(question.choiceD);
}


// Event listeners
questionLevelDropdownButton.addEventListener("click", () => {
    toggleDropdown(questionLevelDropdown);
});

questionLevelDropdown.addEventListener("click", event => {
    selectDropdownItem(event);
    const clicked = event.target.closest(".dropdown-item");
    if (!clicked)
        return;
    question.questionLevel.id = Number(clicked.dataset.questionLevelId);
    question.questionLevel.name = clicked.querySelector(".dropdown-item-name").textContent;
});

// On load
changeHtmlBackgroundColorToWhite();


(async () => {
    question = await fetchDataById("question", questionId);
    await tinymce.init(getTinyMCEOption("#question-content-editor", 300));
    tinymce.get("question-content-editor").setContent(question.questionContent);
    switch (question.questionType.id) {
        case QuestionTypeIDs.MultipleChoiceWithOneCorrectAnswer:
            renderMultipleChoiceQuestionWithOneCorrectAnswerSection(question);
            break;
        case QuestionTypeIDs.MultipleChoiceWithMultipleCorrectAnswers:
            break;
        case QuestionTypeIDs.TrueFalse:
            break;
        case QuestionTypeIDs.ShortAnswer:
            break;
        case QuestionTypeIDs.FillInBlank:
            break;
    }
})();