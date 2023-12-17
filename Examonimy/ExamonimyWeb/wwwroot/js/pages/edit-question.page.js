// Imports
import { fetchDataById } from "../helpers/ajax.helper.js";
import { changeHtmlBackgroundColorToWhite, selectDropdownItem, toggleDropdown } from "../helpers/markup.helper.js";
import { MultipleChoiceQuestionWithOneCorrectAnswerUpdate, QuestionUpdate } from "../models/question-update.model.js";
import { getTinyMCEOption } from "../helpers/tinymce.helper.js";
import { FillInBlankQuestion, MultipleChoiceQuestionWithMultipleCorrectAnswers, MultipleChoiceQuestionWithOneCorrectAnswer, Question, QuestionType, ShortAnswerQuestion, TrueFalseQuestion } from "../models/question.model.js";
import { QuestionTypeIDs } from "../helpers/question.helper.js";
import { AnswerRadioMultipleChoiceComponent } from "../components/answer-radio-multiple-choice.component.js";
import { AnswerCheckboxMultipleChoiceComponent } from "../components/answer-checkbox-multiple-choice.component.js";
import { AnswerRadioTrueFalseComponent } from "../components/answer-radio-true-false.component.js";

// DOM selectors
const questionLevelDropdownButton = document.querySelector("#question-level-dropdown-btn");
const questionLevelDropdown = document.querySelector("#question-level-dropdown");
const questionSpecificSection = document.querySelector("#question-specific-section");
const saveButton = document.querySelector("#save-btn");


// States
const temp = window.location.href.split("/");
const questionId = Number(temp[temp.length - 1]);
let question;
let questionUpdate;

// Function expressions
const renderMultipleChoiceQuestionWithOneCorrectAnswerSection = async (q = new MultipleChoiceQuestionWithOneCorrectAnswer()) => {
    questionSpecificSection.innerHTML = `
    <div id="choice-container" class="flex flex-col gap-y-8 mb-10">
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
    <div id="answer-container">
        
    </div>
    `;
    await tinymce.init(getTinyMCEOption("#choice-a-editor", 150));
    await tinymce.init(getTinyMCEOption("#choice-b-editor", 150));
    await tinymce.init(getTinyMCEOption("#choice-c-editor", 150));
    await tinymce.init(getTinyMCEOption("#choice-d-editor", 150));
    tinymce.get("choice-a-editor").setContent(q.choiceA);
    tinymce.get("choice-b-editor").setContent(q.choiceB);
    tinymce.get("choice-c-editor").setContent(q.choiceC);
    tinymce.get("choice-d-editor").setContent(q.choiceD);

    const answerRadioComponent = new AnswerRadioMultipleChoiceComponent(document.querySelector("#answer-container"), q.correctAnswer);
    answerRadioComponent.connectedCallback();
    answerRadioComponent.subscribe("click", (answer = "") => {
        question.correctAnswer = answer;
    });
}

const renderMultipleChoiceQuestionWithMultipleCorrectAnswersSection = async (q = new MultipleChoiceQuestionWithMultipleCorrectAnswers()) => {
    questionSpecificSection.innerHTML = `
    <div id="choice-container" class="flex flex-col gap-y-8 mb-10">
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
    <div id="answer-container">
        
    </div>
    `;
    await tinymce.init(getTinyMCEOption("#choice-a-editor", 150));
    await tinymce.init(getTinyMCEOption("#choice-b-editor", 150));
    await tinymce.init(getTinyMCEOption("#choice-c-editor", 150));
    await tinymce.init(getTinyMCEOption("#choice-d-editor", 150));
    tinymce.get("choice-a-editor").setContent(q.choiceA);
    tinymce.get("choice-b-editor").setContent(q.choiceB);
    tinymce.get("choice-c-editor").setContent(q.choiceC);
    tinymce.get("choice-d-editor").setContent(q.choiceD);

    const answerCheckboxComponent = new AnswerCheckboxMultipleChoiceComponent(document.querySelector("#answer-container"), q.correctAnswers);
    answerCheckboxComponent.connectedCallback();
    answerCheckboxComponent.subscribe("click", (answers = [""]) => {
        question.correctAnswers = answers;
    });
}

const renderTrueFalseQuestionSection = async (q = new TrueFalseQuestion()) => {
    questionSpecificSection.innerHTML = `
    <div id="answer-container">
        
    </div>
    `;
    const answerRadioTrueFalseComponent = new AnswerRadioTrueFalseComponent(document.querySelector("#answer-container"), q.correctAnswer);
    answerRadioTrueFalseComponent.connectedCallback();
    answerRadioTrueFalseComponent.subscribe("click", (answer = "") => {
        question.correctAnswer = answer;
    });
}

const renderShortAnswerQuestionSection = async (q = new ShortAnswerQuestion()) => {
    questionSpecificSection.innerHTML = `
    <div id="answer-container">
        <label for="answer-editor" class="block text-sm leading-6 text-gray-700 font-medium mb-3">Đáp án</label>
        <textarea id="answer-editor"></textarea>
    </div>
    `;
    await tinymce.init(getTinyMCEOption("#answer-editor", 300));
    tinymce.get("answer-editor").setContent(question.correctAnswer);
}

const renderFillInBlankQuestionSection = async (q = new FillInBlankQuestion()) => {
    questionSpecificSection.innerHTML = `
    <div class="flex justify-center mb-10">
        <button type="button" id="refresh-blank-answer-btn" class="rounded-md bg-indigo-50 px-2.5 py-1.5 text-sm font-semibold text-indigo-600 shadow-sm hover:bg-indigo-100">Làm mới đáp án</button>
    </div>
    <div id="answer-container" class="space-y-8">      
    </div>
    `;
    const answerContainer = document.querySelector("#answer-container");
    questionSpecificSection.querySelector("#refresh-blank-answer-btn").addEventListener("click", async () => {
        answerContainer.innerHTML = "";      
        const content = tinymce.get("question-content-editor").getContent();
        const count = (content.match(/__/g) || []).length;
        for (let i = 0; i < count; i++) {           
            answerContainer.insertAdjacentHTML("beforeend", `
    <div class="blank-answer">
        <label for="blank-${i + 1}" class="block text-sm leading-6 text-gray-700 font-medium mb-3">Đáp án chỗ trống ${i + 1}</label>
        <textarea id="blank-${i + 1}"></textarea>
    </div>
            `);

            await tinymce.remove(`#blank-${i + 1}`);
            await tinymce.init(getTinyMCEOption(`#blank-${i + 1}`, 150));          
        }
    });
    for (let i = 0; i < q.correctAnswers.length; i++) {
        answerContainer.insertAdjacentHTML("beforeend", `
    <div class="blank-answer">
        <label for="blank-${i + 1}" class="block text-sm leading-6 text-gray-700 font-medium mb-3">Đáp án chỗ trống ${i + 1}</label>
        <textarea id="blank-${i + 1}"></textarea>
    </div>
        `);
        await tinymce.init(getTinyMCEOption(`#blank-${i + 1}`, 150));
        tinymce.get(`blank-${i + 1}`).setContent(q.correctAnswers[i]);
    }
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

saveButton.addEventListener("click", async () => {
    switch (question.questionType.id) {
        case QuestionTypeIDs.MultipleChoiceWithOneCorrectAnswer:
            questionUpdate = new MultipleChoiceQuestionWithOneCorrectAnswerUpdate();
            questionUpdate.choiceA = tinymce.get("choice-a-editor").getContent();
            questionUpdate.choiceB = tinymce.get("choice-b-editor").getContent();
            questionUpdate.choiceC = tinymce.get("choice-c-editor").getContent();
            questionUpdate.choiceD = tinymce.get("choice-d-editor").getContent();
            questionUpdate.correctAnswer = question.correctAnswer;
            questionUpdate.questionLevelId = question.questionLevel.id;
            questionUpdate.questionContent = tinymce.get("question-content-editor").getContent();
            break;
        default:
            break;
    }
});

// On load
changeHtmlBackgroundColorToWhite();


(async () => {
    try {
        question = await fetchDataById("question", questionId);
        await tinymce.init(getTinyMCEOption("#question-content-editor", 300));
        tinymce.get("question-content-editor").setContent(question.questionContent);
        switch (question.questionType.id) {
            case QuestionTypeIDs.MultipleChoiceWithOneCorrectAnswer:
                renderMultipleChoiceQuestionWithOneCorrectAnswerSection(question);
                break;
            case QuestionTypeIDs.MultipleChoiceWithMultipleCorrectAnswers:
                renderMultipleChoiceQuestionWithMultipleCorrectAnswersSection(question);
                break;
            case QuestionTypeIDs.TrueFalse:
                renderTrueFalseQuestionSection(question);
                break;
            case QuestionTypeIDs.ShortAnswer:
                renderShortAnswerQuestionSection(question);
                break;
            case QuestionTypeIDs.FillInBlank:
                renderFillInBlankQuestionSection(question);
                break;
        }
    } catch (err) {
        console.error(err);
    }  
})();