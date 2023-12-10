import { BaseComponent } from "./base.component.js";
import { Question } from "../models/question.model.js";
import { SimplePaginationComponent } from "./simple-pagination.component.js";
import { trimMarkup } from "../helpers/markup.helper.js";
import { fetchData } from "../helpers/ajax.helper.js";

export class QuestionListPaletteComponent extends BaseComponent {

    #container;
    #questions = [new Question()];
    #paginationContainer;
    #currentPage = 1;
    #totalPages = 999;
    #pageSize = 10;

    constructor(container = new HTMLElement()) {
        super();
        this.#container = container;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();
        this.#paginationContainer = this.#container.querySelector("#pagination-container-for-palette");
        const paginationComponent = new SimplePaginationComponent(this.#paginationContainer);
        paginationComponent.currentPage = this.#currentPage;
        paginationComponent.totalPages = this.#totalPages;
        paginationComponent.connectedCallback();

        paginationComponent.subscribe("onNext", this.onNavigateHandler.bind(this));
        paginationComponent.subscribe("onPrev", this.onNavigateHandler.bind(this));

        this.#container.addEventListener("click", event => {
            const clickedQuestion = event.target.closest(".question-palette-item");
            if (!clickedQuestion)
                return;

            Array.from(this.#container.querySelectorAll(".question-palette-item")).forEach(item => {
                item.classList.remove("bg-gray-100");
            });

            clickedQuestion.classList.add("bg-gray-100");
        });
    }

    async onNavigateHandler(pageNumber = 1) {       
        const getResponse = await fetchData("question", this.#pageSize, pageNumber);
        this.#questions = getResponse.data;
        this.#currentPage = getResponse.paginationMetadata.currentPage;
        this.#container.querySelector("#question-list-container-for-palette").innerHTML = this.#renderQuestions();
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

    #renderQuestions() {      
        return this.#questions.reduce((accumulator, currentValue) => {
            return accumulator + `
<!-- Active: "bg-gray-200" -->
<li data-question-id="${currentValue.id}" class="question-palette-item group flex items-center select-none rounded-xl p-3 hover:bg-gray-100 cursor-pointer">
    <div class="flex h-10 w-10 flex-none items-center justify-center rounded-lg">
        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-7 h-7 text-gray-400">
            <path stroke-linecap="round" stroke-linejoin="round" d="M9.879 7.519c1.171-1.025 3.071-1.025 4.242 0 1.172 1.025 1.172 2.687 0 3.712-.203.179-.43.326-.67.442-.745.361-1.45.999-1.45 1.827v.75M21 12a9 9 0 11-18 0 9 9 0 0118 0zm-9 5.25h.008v.008H12v-.008z" />
        </svg>
    </div>
    <div class="ml-4 flex-auto">
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
</li>
            `;
        }, "");
    }

    #render() {
        var questionListMarkup = this.#renderQuestions();

        return `
<div class="mx-auto transform divide-y divide-gray-100 overflow-hidden rounded-lg bg-white transition-all">   
    <div>
        <p class="mb-4 mt-4 px-5 text-sm font-semibold text-gray-500">Môn học: ${this.#questions[0].course.name}</p>
    </div>
    <div class="relative">
        <svg class="pointer-events-none absolute left-4 top-3.5 h-5 w-5 text-gray-400" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
            <path fill-rule="evenodd" d="M9 3.5a5.5 5.5 0 100 11 5.5 5.5 0 000-11zM2 9a7 7 0 1112.452 4.391l3.328 3.329a.75.75 0 11-1.06 1.06l-3.329-3.328A7 7 0 012 9z" clip-rule="evenodd" />
        </svg>
        <input type="text" class="h-12 w-full border-0 bg-transparent pl-11 pr-4 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm" placeholder="Tìm kiếm câu hỏi..." role="combobox" aria-expanded="false" aria-controls="options">
    </div>   
    <!-- Results, show/hide based on command palette state -->
    <ul id="question-list-container-for-palette" class="max-h-[36rem] scroll-py-3 overflow-y-auto p-3 divide-y divide-gray-100">
        ${questionListMarkup}
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