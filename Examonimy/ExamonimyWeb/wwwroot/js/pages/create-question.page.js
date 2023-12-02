// Imports
import { getTinyMCEOption } from "../helpers/tinymce.helper.js";
import { QuestionTypeRenderers } from "../helpers/question.helper.js";
import { BASE_API_URL, PAGINATION_METADATA_HEADER } from "../config.js";
import { CourseGet } from "../models/course-get.model.js";
import { RequestParams } from "../models/request-params.model.js";
import { PaginationMetadata } from "../models/pagination-metadata.model.js";
// DOM selectors
const courseContainer = document.querySelector("#course-container");
const questionTypeDropdown = document.querySelector("#question-type-dropdown");
const questionTypeDropdownButton = document.querySelector("#question-type-dropdown-btn");
const questionLevelDropdown = document.querySelector("#question-level-dropdown");
const questionLevelDropdownButton = document.querySelector("#question-level-dropdown-btn");
const questionEditorContainer = document.querySelector("#question-editor-container");
const questionOptionContainer = document.querySelector("#question-option-container");
const paginationContainer = document.querySelector("#pagination-container");
const paginationInfoElement = document.querySelector("#pagination-info");
const previousButton = document.querySelector("#prev-btn");
const nextButton = document.querySelector("#next-btn");
const stepContainer = document.querySelector("#step-container");
// States and rule
let courses = [new CourseGet()];
const coursesRequestParams = new RequestParams(12, 1);
let paginationMetadata = new PaginationMetadata();
// Function expressions
const getCourses = async (pageSize, pageNumber) => {
    paginationContainer.classList.add("hidden");
    const response = await fetch(`${BASE_API_URL}/course?pageSize=${pageSize}&pageNumber=${pageNumber}`, {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    });

    const data = await response.json();   
    Object.assign(paginationMetadata, JSON.parse(response.headers.get(PAGINATION_METADATA_HEADER)));
    paginationInfoElement.textContent = `${paginationMetadata.CurrentPage} trên ${paginationMetadata.TotalPages}`;
    return data;
}

const populateCourses = (courses = [new CourseGet()]) => {
    courseContainer.innerHTML = "";
    courses.forEach(course => {
        courseContainer.insertAdjacentHTML("beforeend", `
        <!-- Active: "border-violet-600 ring-2 ring-violet-600", Not Active: "border-gray-300" -->
        <label class="course relative flex cursor-pointer rounded-lg bg-violet-50 p-4 focus:outline-none">
            <input type="radio" name="project-type" value="Newsletter" class="sr-only" aria-labelledby="project-type-0-label" aria-describedby="project-type-0-description-0 project-type-0-description-1">
            <span class="flex flex-1">
                <span class="flex flex-col">                  
                    <span class="block text-sm font-medium text-violet-800">${course.name}</span>
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

const selectDropdownItem = (event = new Event()) => {
    const clicked = event.target.closest(".dropdown-item");
    if (!clicked)
        return;
    const dropdownItemName = clicked.querySelector(".dropdown-item-name");
    const dropdownContainer = clicked.closest(".dropdown-container");
    dropdownContainer.querySelector(".selected-item").textContent = dropdownItemName.textContent;  
    const dropdown = clicked.closest(".dropdown");
    const dropdownItems = Array.from(dropdown.querySelectorAll(".dropdown-item"));
    dropdownItems.forEach(dropdownItem => {
        const itemName = dropdownItem.querySelector(".dropdown-item-name");
        itemName.classList.remove("font-semibold");
        itemName.classList.add("font-normal");
        const itemCheckmark = dropdownItem.querySelector(".dropdown-item-checkmark");
        itemCheckmark.classList.remove("text-violet-600");
        itemCheckmark.classList.add("text-white");
    });
    dropdownItemName.classList.remove("font-normal");
    dropdownItemName.classList.add("font-semibold");
    const dropdownItemCheckmark = clicked.querySelector(".dropdown-item-checkmark");
    dropdownItemCheckmark.classList.remove("text-white");
    dropdownItemCheckmark.classList.add("text-violet-600");
    toggleDropdown(dropdown);
    const questionTypeDropdown = clicked.closest("#question-type-dropdown");
    if (!questionTypeDropdown)
        return;
    const questionType = clicked.dataset.type;
    const renderer = QuestionTypeRenderers[questionType];
    renderer(questionOptionContainer);
}

const toggleDropdown = (dropdown = new HTMLElement()) => {
    dropdown.classList.toggle("opacity-0");
    dropdown.classList.toggle("pointer-events-none");
}

// Event listeners
courseContainer.addEventListener("click", event => {
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
});

questionTypeDropdownButton.addEventListener("click", () => toggleDropdown(questionTypeDropdown));

questionTypeDropdown.addEventListener("click", selectDropdownItem);

questionLevelDropdownButton.addEventListener("click", () => toggleDropdown(questionLevelDropdown));

questionLevelDropdown.addEventListener("click", selectDropdownItem);

previousButton.addEventListener("click", async () => {
    if (paginationMetadata.CurrentPage === 1)
        return;
    coursesRequestParams.pageNumber--;
    courses = await getCourses(coursesRequestParams.pageSize, coursesRequestParams.pageNumber);
    populateCourses(courses);
});

nextButton.addEventListener("click", async () => {
    if (paginationMetadata.CurrentPage === paginationMetadata.TotalPages)
        return;
    coursesRequestParams.pageNumber++;
    courses = await getCourses(coursesRequestParams.pageSize, coursesRequestParams.pageNumber);
    populateCourses(courses);
});

stepContainer.addEventListener("click", event => {
    const clicked = event.target.closest(".step-btn");
    if (!clicked)
        return;
    const currentStep = clicked.closest(".step");
    const previousStep = currentStep.previousElementSibling;
    if (!previousStep)
        return;
    // mark current step
    const currentStepOrder = currentStep.dataset.order;
    const currentStepName = currentStep.querySelector(".step-name").textContent;  
    currentStep.innerHTML = `
      <div class="absolute inset-0 flex items-center" aria-hidden="true">
        <div class="h-0.5 w-full bg-gray-200"></div>
      </div>
      <a href="#" class="relative flex h-8 w-8 items-center justify-center rounded-full border-2 border-green-500 bg-white" aria-current="step">
        <span class="h-2.5 w-2.5 rounded-full bg-green-500" aria-hidden="true"></span>
        <span class="step-name absolute top-10 p-0 overflow-hidden whitespace-nowrap border-0 text-base font-bold text-green-500">${currentStepName}</span>
      </a>
    `;
    // mark previous step as completed
    const previousStepOrder = previousStep.dataset.order;
    const previousStepName = previousStep.querySelector(".step-name").textContent;
    previousStep.innerHTML = `
      <div class="absolute inset-0 flex items-center" aria-hidden="true">
        <div class="h-0.5 w-full bg-green-500"></div>
      </div>
      <a href="#" class="relative flex h-8 w-8 items-center justify-center rounded-full bg-green-500 hover:bg-green-600">
        <svg class="h-5 w-5 text-white" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
          <path fill-rule="evenodd" d="M16.704 4.153a.75.75 0 01.143 1.052l-8 10.5a.75.75 0 01-1.127.075l-4.5-4.5a.75.75 0 011.06-1.06l3.894 3.893 7.48-9.817a.75.75 0 011.05-.143z" clip-rule="evenodd" />
        </svg>
        <span class="step-name absolute top-10 p-0 overflow-hidden whitespace-nowrap border-0 text-base font-bold text-green-500">${previousStepName}</span>
      </a>
    `;

    // hide previous step
    const previousSegmentContainer = document.querySelector(`#segment-${previousStepOrder}`);
    previousSegmentContainer.classList.add("hidden");
    // show current step
    const currentSegmentContainer = document.querySelector(`#segment-${currentStepOrder}`);
    currentSegmentContainer.classList.remove("hidden");
});

// On load
(() => {
    document.documentElement.classList.remove("bg-gray-100");
    document.documentElement.classList.add("bg-white");
})();

(async () => {
    courses = await getCourses(coursesRequestParams.pageSize, coursesRequestParams.pageNumber);
    populateCourses(courses);
})();

tinymce.init(getTinyMCEOption("#question-content-editor", 300));