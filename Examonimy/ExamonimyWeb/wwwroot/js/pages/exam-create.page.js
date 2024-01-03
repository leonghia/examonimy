// Imports
import { fetchData } from "../helpers/ajax.helper.js";
import { MediaType } from "../helpers/media-type.helper.js";
import { ExamPaper } from "../models/exam-paper.model.js";
import { ExamPaperRequestParams } from "../models/request-params.model.js";

// DOM selectors
const courseListContainer = document.querySelector("#course-list-container");
const selectedCourseElement = document.querySelector("#selected-course");
const examPaperListContainer = document.querySelector("#exam-paper-list-container");
const emptyExamPaper = document.querySelector("#empty-exam-paper");


// States


// Functions
const fetchExamPapersByCourseId = async (courseId = 0) => {
    try {
        const res = await fetchData("exam-paper", new ExamPaperRequestParams(courseId, null, null, 50), MediaType.Default);
        const examPapers = res.data;
        populateExamPapers(examPapers);
    } catch (err) {
        console.error(err);
    }
}

const populateExamPapers = (examPapers = [new ExamPaper()]) => {
    if (!examPapers || examPapers.length === 0) {
        emptyExamPaper.classList.remove("hidden");
        examPaperListContainer.classList.add("hidden");
        return;
    }
    emptyExamPaper.classList.add("hidden");
    examPaperListContainer.classList.remove("hidden");
    examPaperListContainer.innerHTML = "";
    examPapers.forEach(ep => examPaperListContainer.insertAdjacentHTML("beforeend", `
<div class="relative flex items-center pb-4 pt-3.5">
    <div class="min-w-0 flex-1 text-sm leading-6">
        <label for="exam-paper-${ep.id}" class="font-medium text-gray-900">${ep.examPaperCode}</label>
        <p class="text-gray-500">Tác giả: ${ep.authorName}</p>
    </div>
    <div class="ml-3 flex h-6 items-center">
        <input id="exam-paper-${ep.id}" name="exam-paper" value="${ep.id}" type="radio" class="h-4 w-4 border-none text-violet-600 bg-gray-300 focus:ring-0 focus:ring-offset-0">
    </div>
</div>
    `));
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