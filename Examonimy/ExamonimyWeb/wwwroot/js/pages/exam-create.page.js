// Imports
import { fetchData } from "../helpers/ajax.helper.js";
import { ExamPaperRequestParams } from "../models/request-params.model.js";

// DOM selectors
const courseListContainer = document.querySelector("#course-list-container");
const selectedCourseElement = document.querySelector("#selected-course");


// States


// Functions
const fetchExamPapersByCourseId = async (courseId = 0) => {
    try {
        const res = await fetchData("exam-paper", new ExamPaperRequestParams(courseId, null, null, 50));
        const examPapers = res.data;
        populateExamPapers(examPapers);
    } catch (err) {
        console.error(err);
    }
}

const populateExamPapers = (examPapers) => {

}

// Event listeners
courseListContainer.addEventListener("click", event => {
    const clickedRadio = event.target.closest("input");
    if (clickedRadio) {
        selectedCourseElement.textContent = clickedRadio.parentElement.parentElement.querySelector("label").textContent;
        fetchExamPapersByCourseId(Number(clickedRadio.value));
    }
});

// On load