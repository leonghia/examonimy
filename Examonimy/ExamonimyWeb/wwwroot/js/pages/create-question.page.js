// Imports
import { getTinyMCEOption } from "../helpers/tinymce.helper.js";
import { QuestionTypeRenderers } from "../helpers/question.helper.js";
// DOM selectors
const courseContainer = document.querySelector("#course-container");
const courseElements = Array.from(document.querySelectorAll(".course"));
const questionTypeDropdown = document.querySelector("#question-type-dropdown");
const questionTypeDropdownButton = document.querySelector("#question-type-dropdown-btn");
const questionLevelDropdown = document.querySelector("#question-level-dropdown");
const questionLevelDropdownButton = document.querySelector("#question-level-dropdown-btn");
const questionEditorContainer = document.querySelector("#question-editor-container");
const questionOptionContainer = document.querySelector("#question-option-container");
const tabContainer = document.querySelector("#tab-container");
const tabs = Array.from(document.querySelectorAll(".tab"));
// States and rule


// Function expressions
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
    courseElements.forEach(course => {
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

tabContainer.addEventListener("click", event => {
    const clicked = event.target.closest(".tab");
    if (!clicked)
        return;
    // unhighlight all tabs
    tabs.forEach(tab => {
        tab.classList.remove(..."bg-indigo-100 text-indigo-700".split(" "));
        tab.classList.add(..."text-gray-500 hover:text-gray-700".split(" "));
    });
    // highlight clicked tab
    clicked.classList.remove(..."text-gray-500 hover:text-gray-700".split(" "));
    clicked.classList.add(..."bg-indigo-100 text-indigo-700".split(" "));

    if (clicked.dataset.tab === "preview") {
        console.log("preview");      
           
    }
});

// On load
(() => {
    document.documentElement.classList.remove("bg-gray-100");
    document.documentElement.classList.add("bg-white");
})();

tinymce.init(getTinyMCEOption("#question-content-editor", 300));