// Imports
import { ExamPaperTimelineComponent } from "../components/exam-paper-timeline.component.js";
import { QuestionPreviewComponent } from "../components/question-preview.component.js";
import { fetchData, postData } from "../helpers/ajax.helper.js";
import { hideSpinnerForButtonWithoutCheckmark, showSpinnerForButton } from "../helpers/markup.helper.js";
import { Operation } from "../helpers/operation.helper.js";
import { ExamPaperQuestion } from "../models/exam-paper-question.model.js";
import { ExamPaperReviewHistory, ExamPaperReviewCommentCreate, ExamPaperReviewHistoryComment } from "../models/exam-paper.model.js";
import { signalRConnection, startSignalR } from "../teacher.layout.js";

// DOM selectors
const questionListContainer = document.querySelector("#question-list-container");
const submitReviewButton = document.querySelector("#submit-review-btn");
const reviewForm = document.querySelector("#review-form");
const commentTextArea = document.querySelector("#comment-textarea");
const examPaperTimelineContainer = document.querySelector("#exam-paper-timeline-container");


// States
const examPaperId = Number(document.querySelector("#exam-paper-container").dataset.examPaperId);
const profilePfp = document.querySelector(".profile-pfp").src;
let examPaperTimelineComponent;


// Function expressions
const initTimeline = async () => {
    const examPaperReviewHistories = [new ExamPaperReviewHistory()];
    Object.assign(examPaperReviewHistories, (await fetchData(`exam-paper/${examPaperId}/review-history`)).data);
    if (examPaperTimelineComponent) {
        examPaperTimelineComponent.examPaperReviewHistories = examPaperReviewHistories;
        examPaperTimelineComponent.populate();
    } else {
        examPaperTimelineComponent = new ExamPaperTimelineComponent(examPaperTimelineContainer, examPaperReviewHistories);
        examPaperTimelineComponent.connectedCallback();
    } 
}

// Event listeners
questionListContainer.addEventListener("click", event => {
    const clickedToggleAnswerButton = event.target.closest(".toggle-answer-btn");
    if (clickedToggleAnswerButton) {
        clickedToggleAnswerButton.closest(".question").querySelector(".answer-container").classList.toggle("hidden");
        return;
    }

    const clickedCommentButton = event.target.closest(".comment-btn");
    if (clickedCommentButton) {
        const ePQCId = Number(clickedCommentButton.closest(".question").dataset.examPaperQuestionId);
        if (clickedCommentButton.closest(".question").nextElementSibling.querySelector(".question-comment")) {
            clickedCommentButton.closest(".question").nextElementSibling.querySelector(".question-comment").remove();
            // show the comments
            Array.from(clickedCommentButton.closest(".question").nextElementSibling.querySelectorAll(".epqc")).forEach(epqc => epqc.classList.remove("hidden"));
        } else {
            // temprorarily hide the comments
            Array.from(clickedCommentButton.closest(".question").nextElementSibling.querySelectorAll(".epqc")).forEach(epqc => epqc.classList.add("hidden"));
            clickedCommentButton.closest(".question").nextElementSibling.insertAdjacentHTML("beforeend", `
<div data-exam-paper-question-id="${ePQCId}" class="question-comment absolute -left-5 top-2 flex items-start space-x-4">
    <div class="flex-shrink-0">
        <img class="inline-block h-10 w-10 rounded-full" src="${profilePfp}" alt="user profile picture">
    </div>
    <div class="min-w-0 flex-1 rounded-md bg-white">
        <form action="#" class="relative">
            <div class="overflow-hidden rounded-t-md">
                <label for="comment" class="sr-only">Add your comment</label>
                <textarea rows="2" name="comment" id="comment" class="block w-72 border-0 py-1.5 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm sm:leading-6" placeholder="Nhập bình luận về câu hỏi..."></textarea>              
            </div>

            <div class="flex justify-end py-2 pl-3 pr-2">
                <div class="flex-shrink-0">
                    <button type="button" class="submit-comment-btn w-16 inline-flex justify-center items-center rounded-md bg-violet-300 px-3 py-2 text-sm font-semibold text-violet-800 hover:bg-violet-400 hover:text-violet-900">Đăng</button>
                </div>
            </div>
        </form>
    </div>
</div>
        `);
        }
        return;
    }
});

submitReviewButton.addEventListener("click", async () => {
    showSpinnerForButton(submitReviewButton);
    const value = Number(reviewForm.elements["review"].value);
    switch (value) {
        case Operation.CommentExamPaper:
            // comment          
            try {
                const examPaperReviewCommentCreate = new ExamPaperReviewCommentCreate(commentTextArea.value);
                await postData(`exam-paper/${examPaperId}/review/comment`, examPaperReviewCommentCreate);              
            } catch (err) {
                console.error(err);
            } finally {
                hideSpinnerForButtonWithoutCheckmark(submitReviewButton, "Gửi đi");
            }
            break;
        case Operation.ApproveExamPaper:
            // approve exam paper
            break;
        case Operation.RejectExamPaper:
            // reject exam paper
            break;
    }
});

signalRConnection.on("ReceiveComment", (eprhc = ExamPaperReviewHistoryComment()) => {
    console.log(eprhc);
});

// On load
startSignalR();

(async () => {
    const examPaperQuestions = [new ExamPaperQuestion()];   
    Object.assign(examPaperQuestions, (await fetchData(`exam-paper/${examPaperId}/question-with-answer`)).data);
    examPaperQuestions.forEach(ePQ => {
        questionListContainer.insertAdjacentHTML("beforeend", `
        <div class="question relative bg-white rounded-lg p-6" data-question-number="${ePQ.number}" data-question-id="${ePQ.question.id}" data-exam-paper-question-id="${ePQ.id}">
            <div class="flex items-center justify-between mb-6">
                <p class="text-sm font-bold text-gray-900">Câu ${ePQ.number}</p>
                <div class="flex items-center gap-x-4">
                    <button type="button" title="Ẩn/hiện đáp án" class="toggle-answer-btn bg-yellow-100 rounded-md p-2 text-yellow-600 hover:bg-yellow-200 hover:text-yellow-700">
                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-5 h-5">
                            <path stroke-linecap="round" stroke-linejoin="round" d="M12 18v-5.25m0 0a6.01 6.01 0 0 0 1.5-.189m-1.5.189a6.01 6.01 0 0 1-1.5-.189m3.75 7.478a12.06 12.06 0 0 1-4.5 0m3.75 2.383a14.406 14.406 0 0 1-3 0M14.25 18v-.192c0-.983.658-1.823 1.508-2.316a7.5 7.5 0 1 0-7.517 0c.85.493 1.509 1.333 1.509 2.316V18" />
                        </svg>
                    </button>                  
                </div>

            </div>
            
            <div class="question-container"></div>
        </div> 
        `);
        new QuestionPreviewComponent(questionListContainer.lastElementChild.querySelector(".question-container"), ePQ.question).connectedCallback();
        
    });
})();

initTimeline();