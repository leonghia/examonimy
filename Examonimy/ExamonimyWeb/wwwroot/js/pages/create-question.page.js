// Imports
import { getTinyMCEOption } from "../helpers/tinymce.helper.js";
import { BASE_API_URL } from "../config.js";
import { Course } from "../models/course.model.js";
import { ChoiceValueMappings, QuestionTypeIDs, QuestionTypeIdQuestionCreateDtoConstructorMappings, QuestionTypeIdQuestionCreationEndpointMappings } from "../helpers/question.helper.js";
import { FillInBlankQuestionCreateDto, MultipleChoiceQuestionCreateDto, MultipleChoiceQuestionWithMultipleCorrectAnswersCreateDto, MultipleChoiceQuestionWithOneCorrectAnswerCreateDto, QuestionCreateDto, ShortAnswerQuestionCreateDto, TrueFalseQuestionCreateDto } from "../models/question-create.model.js";
import { toggleDropdown, selectDropdownItem, showSpinnerForButton, hideSpinnerForButton, changeHtmlBackgroundColorToWhite, changeHtmlBackgroundColorToGray } from "../helpers/markup.helper.js";
import { Question, QuestionType, QuestionLevel } from "../models/question.model.js";
import { SpinnerOption } from "../models/spinner-option.model.js";
import { CourseGridComponent } from "../components/course-grid.component.js";
import { SimplePaginationComponent } from "../components/simple-pagination.component.js";
import { fetchData } from "../helpers/ajax.helper.js";
import { StepperComponent } from "../components/stepper.component.js";

// DOM selectors
const courseContainer = document.querySelector("#course-container");
const questionTypeDropdown = document.querySelector("#question-type-dropdown");
const questionTypeDropdownButton = document.querySelector("#question-type-dropdown-btn");
const questionLevelDropdown = document.querySelector("#question-level-dropdown");
const questionLevelDropdownButton = document.querySelector("#question-level-dropdown-btn");
const paginationContainerForCourses = document.querySelector("#pagination-container");
const stepperContainer = document.querySelector("#step-container");
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
const buttonContainer = document.querySelector("#button-container");
const createQuestionButton = document.querySelector("#create-question-btn");

// States and rule
const pageSizeForCourses = 12;
let question = new Question();
let questionCreateDto = new QuestionCreateDto();
const courseGridComponent = new CourseGridComponent(courseContainer);
const paginationComponentForCourses = new SimplePaginationComponent(paginationContainerForCourses);
const stepperComponent = new StepperComponent(stepperContainer, ["Chọn môn học", "Nhập nội dung", "Nhập đáp án", "Xem trước"]);

// Function expressions
const populatePreviewInfo = (question = new Question()) => {
    coursePreview.textContent = question.course.name;
    questionTypePreview.textContent = question.questionType.name;
    questionLevelPreview.textContent = question.questionLevel.name;
    if (question.questionType.id === QuestionTypeIDs.FillInBlank)
        return;
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
    // Render choice preview
    questionContentPreview.insertAdjacentHTML("beforeend", `
<fieldset class="mt-4">
    <legend class="sr-only">Phương án</legend>
    <div class="space-y-5">
        <div class="relative flex items-start">
            <div class="flex h-6 items-center">
                <input name="choices" type="radio" class="pointer-events-none h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
            </div>
            <div class="ml-3 leading-6 flex">
                <label class="mr-1">Đúng</label>           
            </div>
        </div>
        <div class="relative flex items-start">
            <div class="flex h-6 items-center">
                <input name="choices" type="radio" class="pointer-events-none h-4 w-4 border-gray-300 text-indigo-600 focus:ring-indigo-600">
            </div>
            <div class="ml-3 leading-6 flex">
                <label class="mr-1">Sai</label>             
            </div>
        </div>
    </div>
</fieldset>
    `);

    // Render answer preview
    answerPreview.innerHTML = `
<span class="flex h-8 w-8 flex-shrink-0 items-center justify-center rounded-full bg-green-600">
    <span class="text-white">${questionCreateDto.correctAnswer ? "Đ" : "S"}</span>
</span>
    `;
}

const renderPreviewForShortAnswerQuestion = (questionCreateDto = new ShortAnswerQuestionCreateDto()) => {
    // Render answer preview
    questionCreateDto.correctAnswer = tinymce.get("answer-editor-for-short-answer-question").getContent();
    answerPreview.innerHTML = questionCreateDto.correctAnswer;
}

const renderPreviewForFillInBlankQuestion = (questionCreateDto = new FillInBlankQuestionCreateDto()) => {
    // Render the question content with styled blanks
    let i = 0;
    const content = tinymce.get("question-content-editor").getContent().replaceAll("__", () => {
        return `
<span class="mx-1 inline-flex h-6 w-6 flex-shrink-0 items-center justify-center rounded-full bg-gray-700">
    <span class="text-white font-semibold text-xs">${++i}</span>
</span>
<span class="mr-2 inline-flex w-20 border-b-2 border-gray-300"></span>
        `;
    });
    questionContentPreview.innerHTML = content;
    answerPreview.innerHTML = `
<fieldset class="">
    <legend class="sr-only">Đáp án</legend>
    <div class="space-y-5" id="blank-answer-container">
                 
    </div>
</fieldset>
    `;

    // Render the answer preview
    i = 0;
    var correctAnswers = Array.from(blankAnswerEditor.querySelectorAll("textarea")).map(textarea => textarea.id).map(id => tinymce.get(id).getContent());
    correctAnswers.forEach(answer => {
        answerPreview.querySelector("#blank-answer-container").insertAdjacentHTML("beforeend", `
        <div class="relative flex items-start">
            <div class="leading-6 flex">
                <span class="mr-2 inline-flex h-7 w-7 flex-shrink-0 items-center justify-center rounded-full bg-green-600">
                   <span class="text-white font-semibold text-xs">${++i}</span>
                </span>
                <div class="prose-p:m-0">${answer}</div>
            </div>
        </div>
        `);
    });

    // Update the correctAnswers state for questionCreateDto
    questionCreateDto.correctAnswers = correctAnswers.join("|");
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

const populateCourses = async () => {
    const coursePaginationMetadata = await fetchData("course", pageSizeForCourses, paginationComponentForCourses.currentPage);
    courseGridComponent.courses = coursePaginationMetadata.data;
    courseGridComponent.connectedCallback();
    paginationComponentForCourses.totalPages = coursePaginationMetadata.paginationMetadata.totalPages;
    paginationComponentForCourses.connectedCallback();
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

const postQuestion = async (questionCreateDto = new QuestionCreateDto()) => {
    const buttonText = createQuestionButton.querySelector("span");
    showSpinnerForButton(buttonText, createQuestionButton, new SpinnerOption());
    const endpoint = QuestionTypeIdQuestionCreationEndpointMappings[questionCreateDto.questionTypeId];
    const response = await fetch(`${BASE_API_URL}/${endpoint}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        },
        body: JSON.stringify(questionCreateDto)
    });
    hideSpinnerForButton(createQuestionButton, buttonText);
    if (response.ok) {
        window.location.href = "/question";
    }
}

const onClickStep3Handler = () => {
    // Update the question content for questionCreateDto
    const content = tinymce.get("question-content-editor").getContent();
    questionCreateDto.questionContent = content;
    if (questionCreateDto.questionTypeId === QuestionTypeIDs.MultipleChoiceWithOneCorrectAnswer || questionCreateDto.questionTypeId === QuestionTypeIDs.MultipleChoiceWithMultipleCorrectAnswers) {
        questionCreateDto.choiceA = tinymce.get("choice-a").getContent();
        questionCreateDto.choiceB = tinymce.get("choice-b").getContent();
        questionCreateDto.choiceC = tinymce.get("choice-c").getContent();
        questionCreateDto.choiceD = tinymce.get("choice-d").getContent();
    }

    // We only proceed if this is a fill in blank question
    if (questionCreateDto.questionTypeId !== 5)
        return;
    // We render the answer editor based on the numbers of blank from tinymce editor
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
        tinymce.init(getTinyMCEOption(`#blank-${i + 1}`, 150));
    }
}

const onClickStep4Hanlder = () => {
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

    buttonContainer.classList.remove("hidden");
}

const onClickCourseHandler = (course = new Course()) => {
    question.course = course;
    questionCreateDto.courseId = course.id;
}

const onNavigateCoursePageHandler = async (pageNumber = 0) => {
    const res = await fetchData("course", pageSizeForCourses, pageNumber);
    const courses = res.data;
    courseGridComponent.populateCourses(courses);
}

// Event listeners
courseContainer.addEventListener("click", selectCourse);

questionTypeDropdownButton.addEventListener("click", () => toggleDropdown(questionTypeDropdown));

questionTypeDropdown.addEventListener("click", selectQuestionType);

questionLevelDropdownButton.addEventListener("click", () => toggleDropdown(questionLevelDropdown));

questionLevelDropdown.addEventListener("click", selectQuestionLevel);

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

createQuestionButton.addEventListener("click", async () => {
    await postQuestion(questionCreateDto);
});

// On load
changeHtmlBackgroundColorToWhite();
courseGridComponent.subscribe("onClickCourse", onClickCourseHandler);
stepperComponent.connectedCallback();
stepperComponent.subscribe("onClickStep3", onClickStep3Handler);
stepperComponent.subscribe("onClickStep4", onClickStep4Hanlder);
paginationComponentForCourses.subscribe("onNext", onNavigateCoursePageHandler);
paginationComponentForCourses.subscribe("onPrev", onNavigateCoursePageHandler);

(async () => {
    populateCourses();
    const questionTypes = await fetchQuestionTypes();
    populateQuestionTypes(questionTypeDropdown, questionTypes);
    const questionLevels = await fetchQuestionLevels();
    populateQuestionLevels(questionLevelDropdown, questionLevels);
})();

tinymce.init(getTinyMCEOption("#question-content-editor", 300));
tinymce.init(getTinyMCEOption("#answer-editor-for-short-answer-question", 300));