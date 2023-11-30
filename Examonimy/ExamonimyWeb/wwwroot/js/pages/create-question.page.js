// Imports

// DOM selectors
const courseContainer = document.querySelector("#course-container");
const courseElements = Array.from(document.querySelectorAll(".course"));
const questionTypeDropdown = document.querySelector("#question-type-dropdown");
const questionTypeDropdownButton = document.querySelector("#question-type-dropdown-btn");
const questionLevelDropdown = document.querySelector("#question-level-dropdown");
const questionLevelDropdownButton = document.querySelector("#question-level-dropdown-btn");
const questionTypeLabel = document.querySelector("#question-type-label");
const questionLevelLabel = document.querySelector("#question-level-label");
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
    const dropdownButton = dropdownContainer.querySelector(".dropdown-btn");
    dropdownButton.classList.remove("text-gray-500");
    dropdownButton.classList.add("text-gray-900");
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
// On load
(() => {
    document.documentElement.classList.remove("bg-gray-100");
    document.documentElement.classList.add("bg-white");
})();