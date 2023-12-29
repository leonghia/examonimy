// Imports
import { QuestionPreviewComponent } from "../components/question-preview.component.js";
import { fetchData, postData } from "../helpers/ajax.helper.js";
import { convertToAgo } from "../helpers/datetime.helper.js";
import { hideSpinnerForFailButton, hideSpinnerForSuccessButton, showSpinnerForButton } from "../helpers/markup.helper.js";
import { ExamPaperQuestion, ExamPaperQuestionComment, ExamPaperQuestionCommentCreate } from "../models/exam-paper-question.model.js";

// DOM selectors
const questionListContainer = document.querySelector("#question-list-container");
// States
const examPaperId = Number(document.querySelector("#exam-paper-detail-container").dataset.examPaperId);
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
        if (clickedCommentButton.closest(".question").nextElementSibling.querySelector(".question-comment"))
            clickedCommentButton.closest(".question").nextElementSibling.querySelector(".question-comment").remove();
        else
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
        return;
    }

    const clickedSubmitCommentButton = event.target.closest(".submit-comment-btn");
    if (clickedSubmitCommentButton) {
        const comment = clickedSubmitCommentButton.closest("form").querySelector("textarea").value;
        const ePQId = Number(clickedSubmitCommentButton.closest(".question-comment").dataset.examPaperQuestionId);
        const examPaperQuestionCommentCreate = new ExamPaperQuestionCommentCreate(ePQId, comment);
        showSpinnerForButton(clickedSubmitCommentButton);
        postData("exam-paper-question/comment", examPaperQuestionCommentCreate)
            .then((data) => {
                hideSpinnerForSuccessButton(clickedSubmitCommentButton);             
                // insert the new comment
                const examPaperQuestionComment = new ExamPaperQuestionComment();
                Object.assign(examPaperQuestionComment, data);
                clickedSubmitCommentButton.closest(".question-comment").parentElement.insertAdjacentHTML("beforeend", `
    <div class="relative p-4 bg-white rounded-lg w-80">       
        <div class="relative flex items-start space-x-3">
          <div class="relative">
            <img class="flex h-10 w-10 items-center justify-center rounded-full bg-gray-400 ring-8 ring-white" src="${examPaperQuestionComment.commenterProfilePicture}" alt="user profile picture">

            <span class="absolute -bottom-0.5 -right-1 rounded-tl bg-white px-0.5 py-px">
              <svg class="h-5 w-5 text-gray-400" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                <path fill-rule="evenodd" d="M10 2c-2.236 0-4.43.18-6.57.524C1.993 2.755 1 4.014 1 5.426v5.148c0 1.413.993 2.67 2.43 2.902.848.137 1.705.248 2.57.331v3.443a.75.75 0 001.28.53l3.58-3.579a.78.78 0 01.527-.224 41.202 41.202 0 005.183-.5c1.437-.232 2.43-1.49 2.43-2.903V5.426c0-1.413-.993-2.67-2.43-2.902A41.289 41.289 0 0010 2zm0 7a1 1 0 100-2 1 1 0 000 2zM8 8a1 1 0 11-2 0 1 1 0 012 0zm5 1a1 1 0 100-2 1 1 0 000 2z" clip-rule="evenodd" />
              </svg>
            </span>
          </div>
          <div class="min-w-0 flex-1">
            <div>
              <div class="text-sm">
                <a href="#" class="font-medium text-gray-900">${examPaperQuestionComment.commenterName}</a>
              </div>
              <p class="mt-0.5 text-sm text-gray-500">${convertToAgo(new Date(examPaperQuestionComment.commentedAt))}</p>
            </div>
            <div class="mt-2 text-sm text-gray-700">
              <p>${examPaperQuestionComment.comment}</p>
            </div>
          </div>
        </div>
    </div>
                `);
                // remove the comment form
                clickedSubmitCommentButton.closest(".question-comment").remove();
            })
            .catch(err => {
                console.error(err);
                hideSpinnerForFailButton(clickedSubmitCommentButton, "Đăng");
            });
    }
});

// On load
(async () => {
    const examPaperQuestions = [new ExamPaperQuestion()];   
    Object.assign(examPaperQuestions, (await fetchData(`exam-paper/${examPaperId}/question-with-answer`)).data);
    examPaperQuestions.forEach(ePQ => {
        questionListContainer.insertAdjacentHTML("beforeend", `
    <div class="flex gap-x-10">
        <div class="question basis-1/2 relative bg-white rounded-lg p-6" data-question-number="${ePQ.number}" data-question-id="${ePQ.question.id}" data-exam-paper-question-id="${ePQ.id}">
            <div class="flex items-center justify-between mb-6">
                <p class="text-sm font-bold text-gray-900">Câu ${ePQ.number}</p>
                <div class="flex items-center gap-x-4">
                    <button type="button" title="Ẩn/hiện đáp án" class="toggle-answer-btn bg-yellow-100 rounded-md p-2 text-yellow-600 hover:bg-yellow-200 hover:text-yellow-700">
                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-5 h-5">
                            <path stroke-linecap="round" stroke-linejoin="round" d="M12 18v-5.25m0 0a6.01 6.01 0 0 0 1.5-.189m-1.5.189a6.01 6.01 0 0 1-1.5-.189m3.75 7.478a12.06 12.06 0 0 1-4.5 0m3.75 2.383a14.406 14.406 0 0 1-3 0M14.25 18v-.192c0-.983.658-1.823 1.508-2.316a7.5 7.5 0 1 0-7.517 0c.85.493 1.509 1.333 1.509 2.316V18" />
                        </svg>
                    </button>
                    <button type="button" title="Nhận xét câu hỏi" class="comment-btn bg-violet-100 rounded-md p-2 text-violet-600 hover:bg-violet-200 hover:text-violet-700">
                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-5 h-5">
                            <path stroke-linecap="round" stroke-linejoin="round" d="M8.625 9.75a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Zm0 0H8.25m4.125 0a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Zm0 0H12m4.125 0a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Zm0 0h-.375m-13.5 3.01c0 1.6 1.123 2.994 2.707 3.227 1.087.16 2.185.283 3.293.369V21l4.184-4.183a1.14 1.14 0 0 1 .778-.332 48.294 48.294 0 0 0 5.83-.498c1.585-.233 2.708-1.626 2.708-3.228V6.741c0-1.602-1.123-2.995-2.707-3.228A48.394 48.394 0 0 0 12 3c-2.392 0-4.744.175-7.043.513C3.373 3.746 2.25 5.14 2.25 6.741v6.018Z" />
                        </svg>
                    </button>
                </div>

            </div>
            
            <div class="question-container"></div>
        </div>
        <div class="basis-1/3 relative">
                    
        </div>
    </div>`);
        new QuestionPreviewComponent(questionListContainer.lastElementChild.querySelector(".question-container"), ePQ.question).connectedCallback();
        
    });
})();