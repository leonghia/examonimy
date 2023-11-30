// Imports

// DOM selectors
const questionTypeDropdownButton = document.querySelector("#question-type-dropdown-btn");
const questionTypeDropdown = document.querySelector("#question-type-dropdown");
const selectedQuestionType = document.querySelector("#selected-question-type");
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