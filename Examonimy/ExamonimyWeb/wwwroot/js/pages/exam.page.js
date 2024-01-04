// Imports
import { ExamTableComponent } from "../components/exam-table.component.js";
import { fetchData } from "../helpers/ajax.helper.js";


// DOM selectors
const examTableContainer = document.querySelector("#exam-table-container");



// States
let examTableComponent;


// Functions

// Event listeners

// On load
(async () => {
    try {
        const res = await fetchData("exam");
        const exams = res.data;
        examTableComponent = new ExamTableComponent(examTableContainer, exams);
        examTableComponent.connectedCallback();
    } catch (err) {
        console.error(err);
    }
    
})();