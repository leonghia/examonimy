// Imports
import { QuestionPreviewComponent } from "../components/question-preview.component.js";
import { fetchData, postData } from "../helpers/ajax.helper.js";
import { hideSpinnerForButtonWithoutCheckmark, hideSpinnerForButtonWithCheckmark, showSpinnerForButton } from "../helpers/markup.helper.js";
import { Operation } from "../helpers/operation.helper.js";
import { ExamPaperQuestion, ExamPaperQuestionComment, ExamPaperQuestionCommentCreate } from "../models/exam-paper-question.model.js";
import { ExamPaperReviewHistory, ExamPaperReviewHistoryComment, ExamPaperReviewHistoryAddReviewer } from "../models/exam-paper.model.js";
import { convertToAgo } from "../helpers/datetime.helper.js";

// DOM selectors
const questionListContainer = document.querySelector("#question-list-container");
const reviewHistoryList = document.querySelector("#review-history-list");


// States
const examPaperId = Number(document.querySelector("#exam-paper-container").dataset.examPaperId);
const profilePfp = document.querySelector(".profile-pfp").src;
// Function expressions

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

// On load
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

(async () => {
    const examPaperReviewHistories = [new ExamPaperReviewHistory()];
    Object.assign(examPaperReviewHistories, (await fetchData(`exam-paper/${examPaperId}/review-history`)).data);
    examPaperReviewHistories.forEach((v, i) => {
        let markup;
        switch (v.operationId) {
            case Operation.CreateExamPaper:
                markup = `
<li>
    <div class="relative pb-8">
        ${i === examPaperReviewHistories.length - 1 ? `` : `<span class="absolute left-5 top-5 -ml-px h-full w-0.5 bg-gray-200" aria-hidden="true"></span>`}
        <div class="relative flex items-start space-x-3">
            <div>
                <div class="relative px-1">
                    <div class="flex h-8 w-8 items-center justify-center rounded-full bg-gray-100 ring-8 ring-white">                                               
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="h-5 w-5 text-gray-500">
                            <path fill-rule="evenodd" d="M5.625 1.5c-1.036 0-1.875.84-1.875 1.875v17.25c0 1.035.84 1.875 1.875 1.875h12.75c1.035 0 1.875-.84 1.875-1.875V12.75A3.75 3.75 0 0 0 16.5 9h-1.875a1.875 1.875 0 0 1-1.875-1.875V5.25A3.75 3.75 0 0 0 9 1.5H5.625ZM7.5 15a.75.75 0 0 1 .75-.75h7.5a.75.75 0 0 1 0 1.5h-7.5A.75.75 0 0 1 7.5 15Zm.75 2.25a.75.75 0 0 0 0 1.5H12a.75.75 0 0 0 0-1.5H8.25Z" clip-rule="evenodd" />
                            <path d="M12.971 1.816A5.23 5.23 0 0 1 14.25 5.25v1.875c0 .207.168.375.375.375H16.5a5.23 5.23 0 0 1 3.434 1.279 9.768 9.768 0 0 0-6.963-6.963Z" />
                        </svg>
                    </div>
                </div>
            </div>
            <div class="min-w-0 flex-1 py-1.5">
                <div class="text-sm text-gray-500">
                    <span class="font-medium text-gray-900">${v.actorName}</span>
                    đã tạo đề thi                   
                    <span class="whitespace-nowrap">${convertToAgo(new Date(v.createdAt))}</span>
                </div>
            </div>
        </div>
    </div>
</li>
                `;
                break;
            case Operation.AskForReviewForExamPaper:
                const eprhar = new ExamPaperReviewHistoryAddReviewer();
                Object.assign(eprhar, v);
                markup = `
<li>
    <div class="relative pb-8">
        ${i === examPaperReviewHistories.length - 1 ? `` : `<span class="absolute left-5 top-5 -ml-px h-full w-0.5 bg-gray-200" aria-hidden="true"></span>`}
        <div class="relative flex items-start space-x-3">
            <div>
                <div class="relative px-1">
                    <div class="flex h-8 w-8 items-center justify-center rounded-full bg-gray-100 ring-8 ring-white">
                        <svg class="h-5 w-5 text-gray-500" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                            <path fill-rule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-5.5-2.5a2.5 2.5 0 11-5 0 2.5 2.5 0 015 0zM10 12a5.99 5.99 0 00-4.793 2.39A6.483 6.483 0 0010 16.5a6.483 6.483 0 004.793-2.11A5.99 5.99 0 0010 12z" clip-rule="evenodd"></path>
                        </svg>
                    </div>
                </div>
            </div>
            <div class="min-w-0 flex-1 py-1.5">
                <div class="text-sm text-gray-500">
                    <span class="font-medium text-gray-900">${eprhar.actorName}</span>
                    đã thêm
                    <span class="font-medium text-gray-900">${eprhar.reviewerName}</span> 
                    làm kiểm duyệt viên
                    <span class="whitespace-nowrap">${convertToAgo(new Date(eprhar.createdAt))}</span>
                </div>
            </div>
        </div>
    </div>
</li>
                `;
                break;
            case Operation.CommentExamPaper:
                const eprhc = new ExamPaperReviewHistoryComment();
                Object.assign(eprhc, v);
                markup = `
<li>
    <div class="relative pb-8">
        ${i === examPaperReviewHistories.length - 1 ? `` : `<span class="absolute left-5 top-5 -ml-px h-full w-0.5 bg-gray-200" aria-hidden="true"></span>`}
        <div class="relative flex items-start space-x-3">
            <div class="relative">
                <img class="flex h-10 w-10 items-center justify-center rounded-full bg-gray-400 ring-8 ring-white" src="${eprhc.actorProfilePicture}" alt="user profile picture">

                <span class="absolute -bottom-0.5 -right-1 rounded-tl bg-white px-0.5 py-px">
                    <svg class="h-5 w-5 text-gray-400" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                        <path fill-rule="evenodd" d="M10 2c-2.236 0-4.43.18-6.57.524C1.993 2.755 1 4.014 1 5.426v5.148c0 1.413.993 2.67 2.43 2.902.848.137 1.705.248 2.57.331v3.443a.75.75 0 001.28.53l3.58-3.579a.78.78 0 01.527-.224 41.202 41.202 0 005.183-.5c1.437-.232 2.43-1.49 2.43-2.903V5.426c0-1.413-.993-2.67-2.43-2.902A41.289 41.289 0 0010 2zm0 7a1 1 0 100-2 1 1 0 000 2zM8 8a1 1 0 11-2 0 1 1 0 012 0zm5 1a1 1 0 100-2 1 1 0 000 2z" clip-rule="evenodd"></path>
                    </svg>
                </span>
            </div>
            <div class="min-w-0 flex-1">
                <div>
                    <div class="text-sm">
                        <span class="font-medium text-gray-900">${eprhc.actorName}</span>
                    </div>
                    <p class="mt-0.5 text-sm text-gray-500">Đã nhận xét ${convertToAgo(new Date(eprhc.createdAt))}</p>
                </div>
                <div class="mt-2 text-sm text-gray-700">
                    <p>${eprhc.comment}</p>
                </div>
            </div>
        </div>
    </div>
</li>
                `;
                break;
            default:
                break;
        }
        reviewHistoryList.insertAdjacentHTML("beforeend", markup);
    });   
})();