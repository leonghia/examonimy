// Imports
import { ExamTableForStudentComponent } from "../components/exam-table.component.js";
import { fetchData } from "../helpers/ajax.helper.js";


// DOM selectors
const examTableContainer = document.querySelector("#exam-table-container");


// States
let examTableForStudentComponent;


// Function expressions

// Event listeners

// On load
(async () => {
    try {
        const res = await fetchData("exam");
        examTableForStudentComponent = new ExamTableForStudentComponent(examTableContainer, res.data);
        examTableForStudentComponent.connectedCallback();
    } catch (err) {
        console.error(err);
    }
})();