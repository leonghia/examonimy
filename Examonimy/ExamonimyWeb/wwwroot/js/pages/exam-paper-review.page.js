// Imports
import { QuestionPreviewComponent } from "../components/question-preview.component.js";
import { fetchData } from "../helpers/ajax.helper.js";
import { ExamPaperQuestion } from "../models/exam-paper-question.model.js";

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
        clickedCommentButton.closest(".question").nextElementSibling.insertAdjacentHTML("beforeend", `
<div class="absolute left-4 top-2 flex items-start space-x-4">
    <div class="flex-shrink-0">
        <img class="inline-block h-10 w-10 rounded-full" src="${profilePfp}" alt="user profile picture">
    </div>
    <div class="min-w-0 flex-1">
        <form action="#" class="relative">
            <div class="overflow-hidden rounded-lg">
                <label for="comment" class="sr-only">Add your comment</label>
                <textarea rows="3" name="comment" id="comment" class="bg-white block w-72 border-0 bg-transparent py-1.5 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm sm:leading-6" placeholder="Add your comment..."></textarea>

                <!-- Spacer element to match the height of the toolbar -->
                <div class="py-2 bg-white" aria-hidden="true">
                    <!-- Matches height of button in toolbar (1px border + 36px content height) -->
                    <div class="py-px">
                        <div class="h-9"></div>
                    </div>
                </div>
            </div>

            <div class="absolute inset-x-0 bottom-0 flex justify-between py-2 pl-3 pr-2">
                <div class="flex-shrink-0">
                    <button type="submit" class="inline-flex items-center rounded-md bg-indigo-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600">Post</button>
                </div>
            </div>
        </form>
    </div>
</div>
        `);
        return;
    }
});

// On load
(async () => {
    const examPaperQuestions = [new ExamPaperQuestion()];   
    Object.assign(examPaperQuestions, (await fetchData(`exam-paper/${examPaperId}/question-with-answer`)).data);
    examPaperQuestions.forEach(ePQ => {
        questionListContainer.insertAdjacentHTML("beforeend", `
    <div class="flex">
        <div class="question basis-1/2 relative bg-white rounded-lg p-6" data-question-number="${ePQ.number}" data-question-id="${ePQ.question.id}">
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
        <div class="basis-1/2 relative"></div>
    </div>`);
        new QuestionPreviewComponent(questionListContainer.lastElementChild.querySelector(".question-container"), ePQ.question).connectedCallback();
        
    });
})();