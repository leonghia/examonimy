// Imports
import { LOCALES } from "../config.js";
import { fetchDataById, putData } from "../helpers/ajax.helper.js";
import { constructExamSchedule } from "../helpers/datetime.helper.js";
import { ExamUpdate } from "../models/exam.model.js";
import { hideSpinnerForButtonWithCheckmark, hideSpinnerForButtonWithoutCheckmark, showSpinnerForButton } from "../helpers/markup.helper.js";
import { SpinnerOption } from "../models/spinner-option.model.js";


// DOM selectors
const fromHourInput = document.querySelector("#from-hour");
const fromMinuteInput = document.querySelector("#from-minute");
const fromDateInput = document.querySelector("#from-date");
const toHourInput = document.querySelector("#to-hour");
const toMinuteInput = document.querySelector("#to-minute");
const toDateInput = document.querySelector("#to-date");
const submitButton = document.querySelector("#submit-btn");



// States
const examId = Number(document.querySelector("#exam-container").dataset.examId);
const examUpdate = new ExamUpdate();
const spinnerOption = new SpinnerOption("fill-violet-800", "w-5", "h-5");

// Functions
const populateSchedule = (from = new Date(), to = new Date()) => {
    fromHourInput.value = from.getHours().toString().padStart(2, "0");
    fromMinuteInput.value = from.getMinutes().toString().padStart(2, "0");
    fromDateInput.value = from.toLocaleDateString(LOCALES);
    toHourInput.value = to.getHours().toString().padStart(2, "0");
    toMinuteInput.value = to.getMinutes().toString().padStart(2, "0");
    toDateInput.value = to.toLocaleDateString(LOCALES);
}

// Event listeners
submitButton.addEventListener("click", async () => {
    showSpinnerForButton(submitButton, spinnerOption);
    examUpdate.examPaperId = Number(document.querySelector('input[name="exam-paper"]:checked').value);
    examUpdate.from = constructExamSchedule(fromHourInput.value, fromMinuteInput.value, fromDateInput.value);
    examUpdate.to = constructExamSchedule(toHourInput.value, toMinuteInput.value, toDateInput.value);
    try {
        await putData(`exam/${examId}`, examUpdate);
        hideSpinnerForButtonWithCheckmark(submitButton, spinnerOption);
        setTimeout(() => document.location.href = "/exam", 1000);
    } catch (err) {
        console.error(err);
        hideSpinnerForButtonWithoutCheckmark(submitButton, spinnerOption);
    }
});


// On load
(async () => {
    try {
        const data = await fetchDataById("exam", examId);       
        populateSchedule(new Date(data.from), new Date(data.to));
    } catch (err) {
        console.error(err);
    }
})();