import { BaseComponent } from "./base.component.js";
import { Question } from "../models/question.model.js";
import { SimplePaginationComponent } from "./simple-pagination.component.js";
import { trimMarkup } from "../helpers/markup.helper.js";
import { fetchData } from "../helpers/ajax.helper.js";
import { QuestionPreviewComponent } from "./question-preview.component.js";
import { RequestParams } from "../models/request-params.model.js";

export class QuestionListPaletteComponent extends BaseComponent {

    #container;
    #questions = [new Question()];
    #paginationContainer;
    #searchQuery = "";
    #currentPage = 1;
    #totalPages = 999;
    #pageSize = 10;
    #questionListContainerForPalette;
    #questionPreviewContainer;
    #questionPreviewWrapper;
    #questionPreviewComponent = new QuestionPreviewComponent();
    #backLink;
    #disabledQuestionIds = [0];
    #searchForm;
    #searchInput;
    #paginationComponent = new SimplePaginationComponent(null);
    #courseName = "";

    constructor(container = new HTMLElement(), questions = [new Question()]) {
        super();
        this.#container = container;
        this.#questions = questions;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();    

        this.#questionListContainerForPalette = this.#container.querySelector("#question-list-container-for-palette");
        this.#paginationContainer = this.#container.querySelector("#pagination-container-for-palette");
        this.#questionPreviewContainer = this.#container.querySelector("#question-preview-container");        
        this.#questionPreviewWrapper = this.#container.querySelector("#question-preview-wrapper");
        this.#backLink = this.#container.querySelector("#back-link");
        this.#searchForm = this.#container.querySelector("#search-form");
        this.#searchInput = this.#container.querySelector("#search-input");

        this.#questionPreviewComponent = new QuestionPreviewComponent(this.#questionPreviewWrapper);

        this.#paginationComponent = new SimplePaginationComponent(this.#paginationContainer);
        this.#paginationComponent.currentPage = this.#currentPage;
        this.#paginationComponent.totalPages = this.#totalPages;
        if (this.#paginationComponent.totalPages > 0) {
            this.#paginationComponent.connectedCallback();    
        }   

        this.#paginationComponent.subscribe("next", this.navigateHandler.bind(this));
        this.#paginationComponent.subscribe("prev", this.navigateHandler.bind(this));   



        this.#container.addEventListener("click", event => {

            const clickedToggleAnswerButton = event.target.closest(".toggle-answer-btn");
            if (clickedToggleAnswerButton) {
                clickedToggleAnswerButton.parentElement.nextElementSibling.querySelector(".answer-container").classList.toggle("hidden");
                return;
            }

            const clickedQuestionPreviewButton = event.target.closest(".preview-question-btn");
            if (clickedQuestionPreviewButton) {
                this.#questionListContainerForPalette.classList.add("hidden");
                this.#paginationContainer.classList.add("hidden");

                // retrieve the question
                const idOfQuestionToPreview = Number(clickedQuestionPreviewButton.closest(".question-palette-item").dataset.questionId);
                const questionToPreview = this.#questions.find(q => q.id === idOfQuestionToPreview);
                this.#questionPreviewComponent.question = questionToPreview;
                this.#questionPreviewComponent.connectedCallback();
                this.#questionPreviewContainer.classList.remove("hidden");
                return;
            }

            const clickedQuestion = event.target.closest(".question-palette-item");
            if (!clickedQuestion)
                return;

            this.unHighlightAllQuestions();

            clickedQuestion.classList.add("bg-gray-100");      
        });

        this.#questionListContainerForPalette.addEventListener("dragstart", event => {
            if (event.target.matches(".question-palette-item")) {
                event.dataTransfer.setData("text/plain", event.target.dataset.questionId);
                event.dataTransfer.dropEffect = "copy";
            }           
        })

        this.#backLink.addEventListener("click", event => {
            event.preventDefault();
            this.#questionListContainerForPalette.classList.remove("hidden");
            this.#paginationContainer.classList.remove("hidden");
            this.#questionPreviewContainer.classList.add("hidden");
            this.#questionPreviewWrapper.innerHTML = "";
        });

        this.#searchForm.addEventListener("submit", event => {
            event.preventDefault();
            this.#searchQuery = this.#searchInput.value;
            this.navigateHandler(1);
        });

        this.#disableQuestions();
    }

    unHighlightAllQuestions() {
        Array.from(this.#container.querySelectorAll(".question-palette-item")).forEach(item => {
            item.classList.remove("bg-gray-100");
        });
    }

    disableQuestion(questionId = 0) {
        if (!this.#disabledQuestionIds.includes(questionId)) {
            this.#disabledQuestionIds.push(questionId);
            Array.from(this.#container.querySelectorAll(".question-palette-item")).find(item => Number(item.dataset.questionId) === questionId).classList.add(..."opacity-20 pointer-events-none".split(" "));
            return this.#questions.find(q => q.id === questionId);
        }
        return null;   
    }

    enableQuestion(questionId = 0) {
        const index = this.#disabledQuestionIds.indexOf(questionId);
        if (index > -1) {
            this.#disabledQuestionIds.splice(index, 1);
            Array.from(this.#container.querySelectorAll(".question-palette-item")).find(item => Number(item.dataset.questionId) === questionId)?.classList.remove(..."opacity-20 pointer-events-none".split(" "));
        }
    }

    #disableQuestions() {      
        Array.from(this.#container.querySelectorAll(".question-palette-item")).forEach(item => {
            if (this.#disabledQuestionIds.includes(Number(item.dataset.questionId)))
                item.classList.add(..."opacity-20 pointer-events-none".split(" "));
        });    
    }

    async navigateHandler(pageNumber = 1) {       
        const getResponse = await fetchData("question", new RequestParams(this.#searchQuery, this.#pageSize, pageNumber));
        this.#questions = getResponse.data;
        this.#currentPage = getResponse.paginationMetadata.currentPage;
        this.#totalPages = getResponse.paginationMetadata.totalPages;
        this.#paginationComponent.currentPage = this.#currentPage;
        this.#paginationComponent.totalPages = this.#totalPages;
        this.#paginationComponent.populatePaginationInfo();                
        this.#questionListContainerForPalette.innerHTML = this.#renderQuestions();    
        this.#disableQuestions();
    }   

    set questions(value = [new Question()]) {
        this.#questions = value;
    }

    set currentPage(value) {
        this.#currentPage = value;
    }

    set totalPages(value) {
        this.#totalPages = value;
    }

    set courseName(value) {
        this.#courseName = value;
    }  

    set disabledQuestionIds(value = [0]) {
        this.#disabledQuestionIds = value;
    }

    #renderQuestions() {
        return this.#questions.reduce((accumulator, currentValue) => {
            return accumulator + `
<!-- Active: "bg-gray-200" -->
<li data-question-id="${currentValue.id}" class="question-palette-item group flex items-center select-none rounded-lg p-3 cursor-pointer" draggable="true">
    <div class="ml-4 basis-4/5">
        <!-- Active: "text-gray-900", Not Active: "text-gray-700" -->
        <div class="prose prose-sm font-medium text-violet-700 mb-2">
            ${trimMarkup(currentValue.questionContent)}
        </div>
        <!-- Active: "text-gray-700", Not Active: "text-gray-500" -->
        <div class="flex items-center gap-x-8 text-sm">
            <div>
                <span class="font-medium text-gray-700">Dạng câu hỏi: </span>
                <span class="font-normal text-gray-500">${currentValue.questionType.name}</span>
            </div>
            <div>
                •
            </div>
            <div>
                <span class="font-medium text-gray-700">Mức độ: </span>
                <span class="font-normal text-gray-500">${currentValue.questionLevel.name}</span>
            </div>
        </div>
    </div>
    <div class="basis-1/5 flex h-10 w-10 flex-none items-center justify-end rounded-lg">     
        <button type="button" class="preview-question-btn rounded bg-blue-50 px-2 py-1 text-xs font-semibold text-blue-600 shadow-sm hover:bg-blue-100">Xem chi tiết</button>
    </div>
</li>
            `;
        }, "")
    }

    #renderEmptyState() {
        return `
<tr>
    <td colspan="6">
        <div class="text-center py-12">        
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" data-slot="icon" class="mx-auto h-12 w-12 text-gray-400">
            <path stroke-linecap="round" stroke-linejoin="round" d="M9.879 7.519c1.171-1.025 3.071-1.025 4.242 0 1.172 1.025 1.172 2.687 0 3.712-.203.179-.43.326-.67.442-.745.361-1.45.999-1.45 1.827v.75M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Zm-9 5.25h.008v.008H12v-.008Z" />
          </svg>
          <h3 class="mt-2 text-sm font-semibold text-gray-700">Không có câu hỏi nào</h3>               
        </div>
    </td>
</tr>
        `;
    }

    #render() {
        

        return `
<div class="mx-auto transform divide-y divide-gray-100 overflow-hidden rounded-lg bg-white transition-all">   
    <div>
        <p class="mb-4 mt-4 px-5 text-sm font-semibold text-gray-500">Môn học: ${this.#courseName}</p>
    </div>
    <form id="search-form" method="GET" action="" class="relative">
        <button type="submit" class="absolute left-4 top-3.5">
            <svg class="h-5 w-5 text-gray-400" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                <path fill-rule="evenodd" d="M9 3.5a5.5 5.5 0 100 11 5.5 5.5 0 000-11zM2 9a7 7 0 1112.452 4.391l3.328 3.329a.75.75 0 11-1.06 1.06l-3.329-3.328A7 7 0 012 9z" clip-rule="evenodd" />
            </svg>
        </button>
        <input id="search-input" type="text" class="h-12 w-full border-0 bg-transparent pl-11 pr-4 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm" placeholder="Tìm kiếm câu hỏi..." role="combobox" aria-expanded="false" aria-controls="options">
    </form>
    <div id="question-preview-container" class="hidden">
        <div class="bg-white px-4 py-5 sm:px-6">
            <div class="-ml-4 -mt-2 flex flex-wrap items-center justify-between sm:flex-nowrap">
                <a href="#" id="back-link" class="ml-4 mt-2 flex items-center gap-x-2 cursor-pointer">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-6 h-6 text-blue-600">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M6.75 15.75L3 12m0 0l3.75-3.75M3 12h18" />
                    </svg>
                    <span class="text-blue-600 font-medium text-sm">Quay lại</span>
                </a>
                <button title="Ẩn/hiện đáp án" type="button" class="toggle-answer-btn bg-yellow-100 rounded-md text-yellow-700 p-2 hover:bg-yellow-200 hover:text-yellow-800">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-5 h-5">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M12 18v-5.25m0 0a6.01 6.01 0 0 0 1.5-.189m-1.5.189a6.01 6.01 0 0 1-1.5-.189m3.75 7.478a12.06 12.06 0 0 1-4.5 0m3.75 2.383a14.406 14.406 0 0 1-3 0M14.25 18v-.192c0-.983.658-1.823 1.508-2.316a7.5 7.5 0 1 0-7.517 0c.85.493 1.509 1.333 1.509 2.316V18" />
                    </svg>
                </button>
            </div>
            <div id="question-preview-wrapper">
                
            </div>
        </div>
    </div>
    <!-- Results, show/hide based on command palette state -->
    <ul id="question-list-container-for-palette" class="max-h-96 scroll-py-3 overflow-y-auto p-3 divide-y divide-gray-100">
        ${this.#questions.length ? this.#renderQuestions()  : this.#renderEmptyState() }
    </ul>

    <!-- Empty state, show/hide based on command palette state -->
    <!--
    <div class="px-6 py-14 text-center text-sm sm:px-14">
        <svg class="mx-auto h-6 w-6 text-gray-400" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" aria-hidden="true">
            <path stroke-linecap="round" stroke-linejoin="round" d="M12 9v3.75m9-.75a9 9 0 11-18 0 9 9 0 0118 0zm-9 3.75h.008v.008H12v-.008z" />
        </svg>
        <p class="mt-4 font-semibold text-gray-900">No results found</p>
        <p class="mt-2 text-gray-500">No components found for this search term. Please try again.</p>
    </div>
    -->
    <div id="pagination-container-for-palette" class="p-4 flex items-center justify-end">
        
    </div>
</div>
        `;
    }
}