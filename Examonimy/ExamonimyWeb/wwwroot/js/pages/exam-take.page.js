// Imports

// Selectors
const confirmRuleInput = document.querySelector("#confirm-rule");
const ruleContainer = document.querySelector("#rule-container");
const technicalCheckContainer = document.querySelector("#technical-check-container");
const technicalCheckProgressBar = document.querySelector("#technical-check-progress-bar");
const statusElement = document.querySelector("#status");
const technicalCheckSpinner = document.querySelector("#technical-check-spinner");
const technicalCheckpoints = Array.from(technicalCheckContainer.querySelectorAll(".technical-checkpoint"));
const startButton = technicalCheckContainer.querySelector("#start-btn");

// States
const technicalCheckProgressBarWidths = ["0%", "22%", "37.5%", "51%", "62.5%", "81%", "100%"];
const technicalCheckStatus = ["Đang kiểm tra đường truyền mạng", "Đang kiểm tra hệ thống", "Đang kiểm tra trình duyệt", "Đang kiểm tra thiết bị"]

// Functions
const runTechnicalCheck = () => {
    let i = 0;
    let j = 0;
    statusElement.textContent = `${technicalCheckStatus[j]}.....(${technicalCheckProgressBarWidths[i]})`;
    const interval = setInterval(() => {             
        technicalCheckProgressBar.style.width = technicalCheckProgressBarWidths[++i];  
        j += 0.5;
        statusElement.textContent = (technicalCheckStatus[j] || technicalCheckStatus[j - 0.5]) + `.....(${technicalCheckProgressBarWidths[i]})`;
        if (j !== 0 && Number.isInteger(j)) {
            technicalCheckpoints[j - 1].classList.add("text-emerald-600");
            technicalCheckpoints[j - 1].querySelector("svg").classList.remove("hidden");
        }
        if (i == technicalCheckProgressBarWidths.length) {
            technicalCheckpoints[technicalCheckpoints.length - 1].classList.add("text-emerald-600");
            technicalCheckpoints[technicalCheckpoints.length - 1].querySelector("svg").classList.remove("hidden");
            clearInterval(interval);
            technicalCheckSpinner.remove();
            statusElement.textContent = "Đã xong! Bạn có thể bắt đầu thi ngay bây giờ.";
            startButton.classList.remove(..."opacity-20 pointer-events-none".split(" "));
        }
    }, 1500);
}

const startExam = () => {

}

// Event listener
confirmRuleInput.addEventListener("change", () => {
    setTimeout(() => {
        ruleContainer.remove();
        technicalCheckContainer.classList.remove("hidden");
        runTechnicalCheck();
    }, 1500);   
})

startButton.addEventListener("click", () => {
    startExam();
});
// On load