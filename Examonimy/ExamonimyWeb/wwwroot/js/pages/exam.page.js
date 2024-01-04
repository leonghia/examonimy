// Imports
import { ExamTableComponent } from "../components/exam-table.component.js";

// DOM selectors
const examTableContainer = document.querySelector("#exam-table-container");
// States

// Functions

// Event listeners

// On load
(() => {
    const examTableComponent = new ExamTableComponent(examTableContainer);
    examTableComponent.connectedCallback();
})();