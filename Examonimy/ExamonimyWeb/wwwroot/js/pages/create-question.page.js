// Imports
import { getTinyMCEOption } from "../helpers/tinymce.helper.js";
import { BASE_API_URL, PAGINATION_METADATA_HEADER } from "../config.js";
import { Course } from "../models/course.model.js";
import { RequestParams } from "../models/request-params.model.js";
import { PaginationMetadata } from "../models/pagination-metadata.model.js";
import { ChoiceValueMappings, QuestionTypeIDs, QuestionTypeIdQuestionCreateDtoConstructorMappings } from "../helpers/question.helper.js";
import { FillInBlankQuestionCreateDto, MultipleChoiceQuestionCreateDto, MultipleChoiceQuestionWithMultipleCorrectAnswersCreateDto, MultipleChoiceQuestionWithOneCorrectAnswerCreateDto, QuestionCreateDto, ShortAnswerQuestionCreateDto, TrueFalseQuestionCreateDto } from "../dtos/question-create.dto.js";
import { QuestionType } from "../models/question-type.model.js";
import { QuestionLevel } from "../models/question-level.model.js";
import { toggleDropdown, selectDropdownItem } from "../helpers/markup.helper.js";
import { Question } from "../models/question.model.js";

// DOM selectors
const courseContainer = document.querySelector("#course-container");
const questionTypeDropdown = document.querySelector("#question-type-dropdown");
const questionTypeDropdownButton = document.querySelector("#question-type-dropdown-btn");
const questionLevelDropdown = document.querySelector("#question-level-dropdown");
const questionLevelDropdownButton = document.querySelector("#question-level-dropdown-btn");
const paginationContainer = document.querySelector("#pagination-container");
const paginationInfoElement = document.querySelector("#pagination-info");
const previousButton = document.querySelector("#prev-btn");
const nextButton = document.querySelector("#next-btn");
const stepContainer = document.querySelector("#step-container");
const segments = Array.from(document.querySelectorAll(".segment"));
const optionEditorContainer = document.querySelector("#option-editor-container");
const answerEditors = Array.from(document.querySelectorAll(".answer-editor"));
const answerEditorForMultipleChoiceQuestionWithOneCorrectAnswer = document.querySelector('.answer-editor[data-question-type-id="1"]');
const answerEditorForMultipleChoiceQuestionWithMultipleCorrectAnswers = document.querySelector('.answer-editor[data-question-type-id="2"]');
const answerEditorForTrueFalseQuestion = document.querySelector('.answer-editor[data-question-type-id="3"]');
const blankAnswerEditor = document.querySelector("#blank-answer-editor");
const step3 = document.querySelector("#step-3");
const step4 = document.querySelector("#step-4");
const coursePreview = document.querySelector("#course-preview");
const questionTypePreview = document.querySelector("#question-type-preview");
const questionLevelPreview = document.querySelector("#question-level-preview");
const questionContentPreview = document.querySelector("#question-content-preview");
const answerPreview = document.querySelector("#answer-preview");

// States and rule
const coursesRequestParams = new RequestParams(12, 1);
let paginationMetadata = new PaginationMetadata();
let question = new Question();
let questionCreateDto = new QuestionCreateDto();


// Function expressions
const populatePreviewInfo = (question = new Question()) => {
    coursePreview.textContent = question.course.name;
    questionTypePreview.textContent = question.questionType.name;
    questionLevelPreview.textContent = question.questionLevel.name;
    questionContentPreview.innerHTML = tinymce.get("question-content-editor").getContent();   
}

const renderPreviewForMultipleChoiceQuestionWithOneCorrectAnswer = (questionCreateDto = new MultipleChoiceQuestionWithOneCorrectAnswerCreateDto()) => {

    // Render choice preview
    questionCreateDto.choiceA = tinymce.get("choice-a").getContent();
    questionCreateDto.choiceB = tinymce.get("choice-b").getContent();
    questionCreateDto.choiceC = tinymce.get("choice-c").getContent();
    questionCreateDto.choiceD = tinymce.get("choice-d").getContent();
    questionContentPreview.insertAdjacentHTML("beforeend", `
<fieldset class="mt-4">
    <legend class="sr-only">Phương án</legend>
    <div class="space-y-5">
        <div class="relative flex items-start">
            <div class="flex h-6 items-center">
                <input name="choices" type="radio" class="pointer-events-none h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
            </div>
            <div class="ml-3 leading-6 flex">
                <label class="font-medium text-gray-900 mr-1">A.</label>
                <div class="prose-p:m-0">${questionCreateDto.choiceA}</div>
            </div>
        </div>
        <div class="relative flex items-start">
            <div class="flex h-6 items-center">
                <input name="choices" type="radio" class="pointer-events-none h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
            </div>
            <div class="ml-3 leading-6 flex">
                <label class="font-medium text-gray-900 mr-1">B.</label>
                <div class="prose-p:m-0">${questionCreateDto.choiceB}</div>
            </div>
        </div>
        <div class="relative flex items-start">
            <div class="flex h-6 items-center">
                <input name="choices" type="radio" class="pointer-events-none h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
            </div>
            <div class="ml-3 leading-6 flex">
                <label class="font-medium text-gray-900 mr-1">C.</label>
                <div class="prose-p:m-0">${questionCreateDto.choiceC}</div>
            </div>
        </div>
        <div class="relative flex items-start">
            <div class="flex h-6 items-center">
                <input name="choices" type="radio" class="pointer-events-none h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
            </div>
            <div class="ml-3 leading-6 flex">
                <label class="font-medium text-gray-900 mr-1">D.</label>
                <div class="prose-p:m-0">${questionCreateDto.choiceD}</div>
            </div>
        </div>
    </div>
</fieldset>
    `);

    // Render answer preview
    answerPreview.innerHTML = `
<span class="flex h-8 w-8 flex-shrink-0 items-center justify-center rounded-full bg-green-600">
    <span class="text-white">${ChoiceValueMappings[questionCreateDto.correctAnswer]}</span>
</span>
    `;
}

const renderPreviewForMultipleChoiceQuestionWithMultipleCorrectAnswers = (questionCreateDto = new MultipleChoiceQuestionWithMultipleCorrectAnswersCreateDto()) => {
    // Render choice preview
    questionCreateDto.choiceA = tinymce.get("choice-a").getContent();
    questionCreateDto.choiceB = tinymce.get("choice-b").getContent();
    questionCreateDto.choiceC = tinymce.get("choice-c").getContent();
    questionCreateDto.choiceD = tinymce.get("choice-d").getContent();
    questionContentPreview.insertAdjacentHTML("beforeend", `
<fieldset class="mt-4">
    <legend class="sr-only">Phương án</legend>
    <div class="space-y-5">
        <div class="relative flex items-start">
            <div class="flex h-6 items-center">
                <input name="choices" type="checkbox" class="pointer-events-none rounded h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
            </div>
            <div class="ml-3 leading-6 flex">
                <label class="font-medium text-gray-900 mr-1">A.</label>
                <div class="prose-p:m-0">${questionCreateDto.choiceA}</div>
            </div>
        </div>
        <div class="relative flex items-start">
            <div class="flex h-6 items-center">
                <input name="choices" type="checkbox" class="pointer-events-none rounded h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
            </div>
            <div class="ml-3 leading-6 flex">
                <label class="font-medium text-gray-900 mr-1">B.</label>
                <div class="prose-p:m-0">${questionCreateDto.choiceB}</div>
            </div>
        </div>
        <div class="relative flex items-start">
            <div class="flex h-6 items-center">
                <input name="choices" type="checkbox" class="pointer-events-none rounded h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
            </div>
            <div class="ml-3 leading-6 flex">
                <label class="font-medium text-gray-900 mr-1">C.</label>
                <div class="prose-p:m-0">${questionCreateDto.choiceC}</div>
            </div>
        </div>
        <div class="relative flex items-start">
            <div class="flex h-6 items-center">
                <input name="choices" type="checkbox" class="pointer-events-none rounded h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
            </div>
            <div class="ml-3 leading-6 flex">
                <label class="font-medium text-gray-900 mr-1">D.</label>
                <div class="prose-p:m-0">${questionCreateDto.choiceD}</div>
            </div>
        </div>
    </div>
</fieldset>
    `);

    // Render answer preview
    answerPreview.innerHTML = "";
    const correctAnswers = questionCreateDto.correctAnswers.split("|").map(str => Number(str)).sort((a, b) => a - b);
    correctAnswers.forEach(num => {
        answerPreview.insertAdjacentHTML("beforeend", `
<span class="flex h-8 w-8 flex-shrink-0 items-center justify-center rounded bg-green-600">
    <span class="text-white">${ChoiceValueMappings[num]}</span>
</span>
        `);
    });
}

const renderPreviewForTrueFalseQuestion = (questionCreateDto = new TrueFalseQuestionCreateDto()) => {

}

const renderPreviewForShortAnswerQuestion = (questionCreateDto = new ShortAnswerQuestionCreateDto()) => {

}

const renderPreviewForFillInBlankQuestion = (questionCreateDto = new FillInBlankQuestionCreateDto()) => {

}

const showAnswerEditor = (questionTypeId = 1) => {
    // hide answer editors of other question types   
    // show the answer editor of the spectify question type
    answerEditors.forEach(answerEditor => {
        if (Number(answerEditor.dataset.questionTypeId) === questionTypeId)
            answerEditor.classList.remove("hidden");
        else
            answerEditor.classList.add("hidden");
    });
}

const renderChoiceEditorForMultipleChoiceQuestion = (choiceEditorContainer = new HTMLElement()) => {
    tinymce.remove("#choice-a");
    tinymce.remove("#choice-b");
    tinymce.remove("#choice-c");
    tinymce.remove("#choice-d");
    clearChoiceEditorContainer(choiceEditorContainer);
    choiceEditorContainer.insertAdjacentHTML("beforeend", `
<fieldset>
  <legend class="sr-only">Plan</legend>
  <div class="space-y-5">
    <div class="relative flex items-start">    
      <div class="text-sm leading-6 grow">
        <label for="small" class="font-medium text-gray-700">1. Phương án A</label>
        <div class="mt-2">
            <textarea id="choice-a"></textarea>
        </div>
      </div>
    </div>
    <div class="relative flex items-start">   
      <div class="text-sm leading-6 grow">
        <label for="medium" class="font-medium text-gray-700">2. Phương án B</label>
        <div class="mt-2">
            <textarea id="choice-b"></textarea>
        </div>
      </div>
    </div>
    <div class="relative flex items-start">   
      <div class="text-sm leading-6 grow">
        <label for="large" class="font-medium text-gray-700">3. Phương án C</label>
        <div class="mt-2">
            <textarea id="choice-c"></textarea>
        </div>
      </div>
    </div>
    <div class="relative flex items-start">   
      <div class="text-sm leading-6 grow">
        <label for="large" class="font-medium text-gray-700">4. Phương án D</label>
        <div class="mt-2">
            <textarea id="choice-d"></textarea>
        </div>
      </div>
    </div>
  </div>
</fieldset>
    `);
    tinymce.init(getTinyMCEOption("#choice-a", 200));
    tinymce.init(getTinyMCEOption("#choice-b", 200));
    tinymce.init(getTinyMCEOption("#choice-c", 200));
    tinymce.init(getTinyMCEOption("#choice-d", 200));
}

const clearChoiceEditorContainer = (choiceEditorContainer = new HTMLElement()) => {
    if (choiceEditorContainer.firstChild) {
        choiceEditorContainer.innerHTML = "";
    }
}

const fetchCourses = async (pageSize, pageNumber) => {
    paginationContainer.classList.add("hidden");
    const response = await fetch(`${BASE_API_URL}/course?pageSize=${pageSize}&pageNumber=${pageNumber}`, {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    });

    const data = await response.json();
    const courses = [new Course()];
    Object.assign(paginationMetadata, JSON.parse(response.headers.get(PAGINATION_METADATA_HEADER)));
    paginationInfoElement.textContent = `${paginationMetadata.CurrentPage} trên ${paginationMetadata.TotalPages}`;
    Object.assign(courses, data);
    return courses;
}

const populateCourses = (courses = [new Course()]) => {
    courseContainer.innerHTML = "";
    courses.forEach(course => {
        courseContainer.insertAdjacentHTML("beforeend", `
        <!-- Active: "border-violet-600 ring-2 ring-violet-600", Not Active: "border-gray-300" -->
        <label data-id="${course.id}" class="course relative flex cursor-pointer rounded-lg bg-violet-50 p-4 focus:outline-none">
            <input type="radio" name="project-type" value="Newsletter" class="sr-only" aria-labelledby="project-type-0-label" aria-describedby="project-type-0-description-0 project-type-0-description-1">
            <span class="flex flex-1">
                <span class="flex flex-col">                  
                    <span class="course-name block text-sm font-medium text-violet-800">${course.name}</span>
                    <span class="mt-1 flex items-center text-sm text-gray-500">Chưa có câu hỏi nào được tạo</span>
                    <span class="mt-6 text-sm font-medium text-gray-900">0 câu hỏi</span>
                </span>
            </span>
            <!-- Not Checked: "invisible" -->
            <svg class="checkbox h-5 w-5 text-violet-600 invisible" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z" clip-rule="evenodd" />
            </svg>
            <!--
              Active: "border", Not Active: "border-2"
              Checked: "border-violet-600", Not Checked: "border-transparent"
            -->
            <span class="pointer-events-none absolute -inset-px rounded-lg border-transparent" aria-hidden="true"></span>
        </label>
        `);
    });
    paginationContainer.classList.remove("hidden");
}

const selectQuestionType = (event = new Event()) => {
    const clicked = event.target.closest(".dropdown-item");
    if (!clicked)
        return;
    selectDropdownItem(clicked);

    // render the option editor
    const questionTypeId = Number(clicked.dataset.questionTypeId);
    if (questionTypeId === QuestionTypeIDs.MultipleChoiceWithOneCorrectAnswer || questionTypeId === QuestionTypeIDs.MultipleChoiceWithMultipleCorrectAnswers) {
        renderChoiceEditorForMultipleChoiceQuestion(optionEditorContainer);
    } else {
        clearChoiceEditorContainer(optionEditorContainer);
    }

    // show the answer editor
    showAnswerEditor(questionTypeId);

    // construct the questionCreateDto based on its question type
    questionCreateDto = QuestionTypeIdQuestionCreateDtoConstructorMappings[questionTypeId];

    // set the course id of questionCreateDto as the course id state
    questionCreateDto.courseId = question.course.id;

    // set the questionTypeId of questionCreateDto
    question.questionType = new QuestionType(questionTypeId, clicked.querySelector(".dropdown-item-name").textContent);
    questionCreateDto.questionTypeId = question.questionType.id;
}

const selectQuestionLevel = (event = new Event()) => {
    const clicked = event.target.closest(".dropdown-item");
    if (!clicked)
        return;
    selectDropdownItem(clicked);
    // update the question level id of questionCreateDto state
    question.questionLevel = new QuestionLevel(Number(clicked.dataset.questionLevelId), clicked.querySelector(".dropdown-item-name").textContent);
    questionCreateDto.questionLevelId = question.questionLevel.id;
}


const selectCourse = (event = new Event()) => {
    const clicked = event.target.closest(".course");
    if (!clicked)
        return;
    // Unhightlight all courses
    Array.from(document.querySelectorAll(".course")).forEach(course => {
        course.classList.remove(..."border border-violet-600 ring-1 ring-violet-600".split(" "));
        course.querySelector(".checkbox").classList.add("invisible");
    });
    clicked.classList.add(..."border border-violet-600 ring-1 ring-violet-600".split(" "));
    clicked.querySelector(".checkbox").classList.remove("invisible");

    // update questionState   
    question.course = new Course(Number(clicked.dataset.id), clicked.querySelector(".course-name").textContent);
}

const fetchQuestionTypes = async () => {
    const response = await fetch(`${BASE_API_URL}/question/type`, {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    });

    const data = await response.json();
    const questionTypes = [new QuestionType()];
    Object.assign(questionTypes, data);
    return questionTypes;
}

const fetchQuestionLevels = async () => {
    const response = await fetch(`${BASE_API_URL}/question/level`, {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    });

    const data = await response.json();
    const questionLevels = [new QuestionLevel()];
    Object.assign(questionLevels, data);
    return questionLevels;
}

const populateQuestionTypes = (dropdown = new HTMLElement(), questionTypes = [new QuestionType()]) => {
    dropdown.innerHTML = "";
    questionTypes.forEach(questionType => {
        dropdown.insertAdjacentHTML("beforeend", `
<li class="dropdown-item text-gray-900 relative select-none py-2 pl-3 pr-9" data-question-type-id="${questionType.id}">
    <span class="dropdown-item-name font-normal block truncate">${questionType.name}</span>
    <span class="dropdown-item-checkmark text-white absolute inset-y-0 right-0 flex items-center pr-4">
        <svg class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
            <path fill-rule="evenodd" d="M16.704 4.153a.75.75 0 01.143 1.052l-8 10.5a.75.75 0 01-1.127.075l-4.5-4.5a.75.75 0 011.06-1.06l3.894 3.893 7.48-9.817a.75.75 0 011.05-.143z" clip-rule="evenodd" />
        </svg>
    </span>
</li>
        `);
    });
}

const populateQuestionLevels = (dropdown = new HTMLElement(), questionLevels = [new QuestionLevel()]) => {
    dropdown.innerHTML = "";
    questionLevels.forEach(questionLevel => {
        dropdown.insertAdjacentHTML("beforeend", `
<li class="dropdown-item text-gray-900 relative select-none py-2 pl-3 pr-9" data-question-level-id="${questionLevel.id}">  
    <span class="dropdown-item-name font-normal block truncate">${questionLevel.name}</span>   
    <span class="text-white absolute inset-y-0 right-0 flex items-center pr-4 dropdown-item-checkmark">
        <svg class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
            <path fill-rule="evenodd" d="M16.704 4.153a.75.75 0 01.143 1.052l-8 10.5a.75.75 0 01-1.127.075l-4.5-4.5a.75.75 0 011.06-1.06l3.894 3.893 7.48-9.817a.75.75 0 011.05-.143z" clip-rule="evenodd" />
        </svg>
    </span>
</li>
        `);
    });
}

// Event listeners
courseContainer.addEventListener("click", selectCourse);

questionTypeDropdownButton.addEventListener("click", () => toggleDropdown(questionTypeDropdown));

questionTypeDropdown.addEventListener("click", selectQuestionType);

questionLevelDropdownButton.addEventListener("click", () => toggleDropdown(questionLevelDropdown));

questionLevelDropdown.addEventListener("click", selectQuestionLevel);

previousButton.addEventListener("click", async () => {
    if (paginationMetadata.CurrentPage === 1)
        return;
    coursesRequestParams.pageNumber--;
    const courses = await fetchCourses(coursesRequestParams.pageSize, coursesRequestParams.pageNumber);
    populateCourses(courses);
});

nextButton.addEventListener("click", async () => {
    if (paginationMetadata.CurrentPage === paginationMetadata.TotalPages)
        return;
    coursesRequestParams.pageNumber++;
    const courses = await fetchCourses(coursesRequestParams.pageSize, coursesRequestParams.pageNumber);
    populateCourses(courses);
});

stepContainer.addEventListener("click", event => {
    // First of all, we need to reset the background color to white (in case it had been changed to gray)
    document.documentElement.classList.remove("bg-gray-100");
    document.documentElement.classList.add("bg-white");

    const clicked = event.target.closest(".step-btn");
    if (!clicked)
        return;
    const clickedStep = clicked.closest(".step");
    const previousStep = clickedStep.previousElementSibling;

    const currentStepOrder = Number(clickedStep.dataset.order);

    // Change the state of the clicked step (if it is not completed yet)
    if (!clickedStep.getAttribute("data-completed")) {
        const currentStepName = clickedStep.querySelector(".step-name").textContent;
        if (currentStepOrder === 4) {
            // If this is the final step (the 4th step), we mark it as completed immediately
            clickedStep.innerHTML = `
            <div class="absolute inset-0 flex items-center" aria-hidden="true">
                <div class="h-0.5 w-full bg-green-500"></div>
            </div>
            <button type="button" class="step-btn relative flex h-8 w-8 items-center justify-center rounded-full bg-green-500 hover:bg-green-600">
                <svg class="h-5 w-5 text-white" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                    <path fill-rule="evenodd" d="M16.704 4.153a.75.75 0 01.143 1.052l-8 10.5a.75.75 0 01-1.127.075l-4.5-4.5a.75.75 0 011.06-1.06l3.894 3.893 7.48-9.817a.75.75 0 011.05-.143z" clip-rule="evenodd" />
                </svg>
                <span class="step-name absolute top-10 p-0 overflow-hidden whitespace-nowrap border-0 text-base font-bold text-green-500">${currentStepName}</span>
            </button>
            `;
            // We also want to change the background color to bg-gray-100
            document.documentElement.classList.remove("bg-white");
            document.documentElement.classList.add("bg-gray-100");

        } else {
            // Else we mark it as current step          
            clickedStep.innerHTML = `
              <div class="absolute inset-0 flex items-center" aria-hidden="true">
                <div class="h-0.5 w-full bg-gray-200"></div>
              </div>
              <button type="button" class="step-btn relative flex h-8 w-8 items-center justify-center rounded-full border-2 border-green-500 bg-white">
                <span class="h-2.5 w-2.5 rounded-full bg-green-500" aria-hidden="true"></span>
                <span class="step-name absolute top-10 p-0 overflow-hidden whitespace-nowrap border-0 text-base font-bold text-green-500">${currentStepName}</span>
              </button>
            `;
        }
    }

    // Mark previous step as completed
    if (previousStep && !previousStep.getAttribute("data-completed")) {      
        previousStep.setAttribute("data-completed", "true");
        const previousStepName = previousStep.querySelector(".step-name").textContent;
        previousStep.innerHTML = `
      <div class="absolute inset-0 flex items-center" aria-hidden="true">
        <div class="h-0.5 w-full bg-green-500"></div>
      </div>
      <button type="button" class="step-btn relative flex h-8 w-8 items-center justify-center rounded-full bg-green-500 hover:bg-green-600">
        <svg class="h-5 w-5 text-white" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
          <path fill-rule="evenodd" d="M16.704 4.153a.75.75 0 01.143 1.052l-8 10.5a.75.75 0 01-1.127.075l-4.5-4.5a.75.75 0 011.06-1.06l3.894 3.893 7.48-9.817a.75.75 0 011.05-.143z" clip-rule="evenodd" />
        </svg>
        <span class="step-name absolute top-10 p-0 overflow-hidden whitespace-nowrap border-0 text-base font-bold text-green-500">${previousStepName}</span>
      </button>
    `;

    }

    // Hide all other segments
    segments.forEach(segment => {
        if (Number(segment.id.split("-")[1]) !== currentStepOrder)
            segment.classList.add("hidden");
    });

    // Show current segment
    const currentSegmentContainer = document.querySelector(`#segment-${currentStepOrder}`);
    currentSegmentContainer.classList.remove("hidden");
});

answerEditorForMultipleChoiceQuestionWithOneCorrectAnswer.addEventListener("click", event => {
    const clicked = event.target.closest("label");
    if (!clicked)
        return;
    // unhighlight all other choices
    const choices = Array.from(answerEditorForMultipleChoiceQuestionWithOneCorrectAnswer.querySelectorAll("label"));
    choices.forEach(choice => {
        choice.classList.remove(..."bg-green-600 text-white hover:bg-green-700".split(" "));
        choice.classList.add(..."bg-gray-100 text-gray-900 hover:bg-gray-200".split(" "));
    });

    // highlight clicked choice
    clicked.classList.remove(..."bg-gray-100 text-gray-900 hover:bg-gray-200".split(" "));
    clicked.classList.add(..."bg-green-600 text-white hover:bg-green-700".split(" "));

    // update correct answer for questionCreateDto state
    questionCreateDto.correctAnswer = Number(clicked.dataset.answer);
});

answerEditorForMultipleChoiceQuestionWithMultipleCorrectAnswers.addEventListener("click", event => {
    const clicked = event.target.closest("label");
    if (!clicked)
        return;
    // toggle the clicked choice
    clicked.classList.toggle("bg-green-600");
    clicked.classList.toggle("text-white");
    clicked.classList.toggle("hover:bg-green-700");
    clicked.classList.toggle("bg-gray-100");
    clicked.classList.toggle("text-gray-900");
    clicked.classList.toggle("hover:bg-gray-200");
    clicked.dataset.selected = clicked.dataset.selected === "false" ? "true" : "false";

    // construct the correct answers for questionCreateDto
    const correctAnswers = [];
    const choices = Array.from(answerEditorForMultipleChoiceQuestionWithMultipleCorrectAnswers.querySelectorAll("label"));
    choices.forEach(choice => {
        if (choice.dataset.selected === "true")
            correctAnswers.push(choice.dataset.answer);
    });

    // update the correct answers for questionCreateDto
    questionCreateDto.correctAnswers = correctAnswers.join("|");
});

answerEditorForTrueFalseQuestion.addEventListener("click", event => {
    const clicked = event.target.closest("label");
    if (!clicked)
        return;
    // unhighlight all other choices
    const choices = Array.from(answerEditorForTrueFalseQuestion.querySelectorAll("label"));
    choices.forEach(choice => {
        choice.classList.remove(..."bg-green-600 text-white hover:bg-green-700".split(" "));
        choice.classList.add(..."bg-gray-100 text-gray-900 hover:bg-gray-200".split(" "));
    });

    // highlight clicked choice
    clicked.classList.remove(..."bg-gray-100 text-gray-900 hover:bg-gray-200".split(" "));
    clicked.classList.add(..."bg-green-600 text-white hover:bg-green-700".split(" "));

    // update correct answer for questionCreateDto state
    questionCreateDto.correctAnswer = clicked.dataset.answer === "true";
    console.log(questionCreateDto);
});

step3.addEventListener("click", () => {
    // We check if the question type is fill in blank
    if (questionCreateDto.questionTypeId !== 5)
        return;
    // We render the answer editor based on the numbers of blank from tinymce editor
    const content = tinymce.get("question-content-editor").getContent({ format: "text" });
    const numbersOfBlank = (content.match(/__/g) || []).length;

    const textareas = Array.from(blankAnswerEditor.querySelectorAll("textarea"));

    textareas.forEach(textarea => tinymce.remove(`#${textarea.id}`));

    blankAnswerEditor.innerHTML = "";

    for (let i = 0; i < numbersOfBlank; i++) {       
        blankAnswerEditor.insertAdjacentHTML("beforeend", `
<div class="relative flex items-start">
    <div class="text-sm leading-6 grow">
        <label for="small" class="font-medium text-gray-700">Đáp án cho chỗ trống ${i + 1}</label>
        <div class="mt-2">
            <textarea id="blank-${i + 1}"></textarea>
        </div>
    </div>
</div>
        `);
        tinymce.init(getTinyMCEOption(`#blank-${i + 1}`, 200));
    }
});

step4.addEventListener("click", () => {
    populatePreviewInfo(question);
    // Render the preview based on questionCreateDto's question type
    switch (questionCreateDto.questionTypeId) {
        case QuestionTypeIDs.MultipleChoiceWithOneCorrectAnswer:
            renderPreviewForMultipleChoiceQuestionWithOneCorrectAnswer(questionCreateDto);
            break;
        case QuestionTypeIDs.MultipleChoiceWithMultipleCorrectAnswers:
            renderPreviewForMultipleChoiceQuestionWithMultipleCorrectAnswers(questionCreateDto);
            break;
        case QuestionTypeIDs.TrueFalse:
            renderPreviewForTrueFalseQuestion(questionCreateDto);
            break;
        case QuestionTypeIDs.ShortAnswer:
            renderPreviewForShortAnswerQuestion(questionCreateDto);
            break;
        case QuestionTypeIDs.FillInBlank:
            renderPreviewForFillInBlankQuestion(questionCreateDto);
            break;
        default:
            break;
    }
});

// On load
(() => {
    document.documentElement.classList.remove("bg-gray-100");
    document.documentElement.classList.add("bg-white");
})();

(async () => {
    const courses = await fetchCourses(coursesRequestParams.pageSize, coursesRequestParams.pageNumber);
    populateCourses(courses);
    const questionTypes = await fetchQuestionTypes();
    populateQuestionTypes(questionTypeDropdown, questionTypes);
    const questionLevels = await fetchQuestionLevels();
    populateQuestionLevels(questionLevelDropdown, questionLevels);
})();

tinymce.init(getTinyMCEOption("#question-content-editor", 300));
tinymce.init(getTinyMCEOption("#answer-editor-for-short-answer-question", 300));