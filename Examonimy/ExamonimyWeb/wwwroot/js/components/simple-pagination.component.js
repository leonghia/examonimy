import { BaseComponent } from "./base.component.js";

export class SimplePaginationComponent extends BaseComponent {

    #currentPage = 1;
    #totalPages = 999;
    #container;
    #nextButton;
    #prevButton;
    #paginationInfo;
    _events = {
        next: [],
        prev: []
    }

    constructor(container = new HTMLElement()) {
        super();
        this.#container = container;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();      

        this.#nextButton = this.#container.querySelector("#next-btn");
        this.#prevButton = this.#container.querySelector("#prev-btn");
        this.#paginationInfo = this.#container.querySelector("#pagination-info");

        this.#nextButton.addEventListener("click", () => {
            if (this.#currentPage === this.#totalPages)
                return;
            this.#currentPage++;
            this._trigger("next", this.#currentPage);
            this.populatePaginationInfo();
        });

        this.#prevButton.addEventListener("click", () => {
            if (this.#currentPage === 1)
                return;
            this.#currentPage--;
            this._trigger("prev", this.#currentPage);
            this.populatePaginationInfo();
        });
    }

    set currentPage(value) {
        this.#currentPage = value;
    }

    set totalPages(value) {
        this.#totalPages = value;
    }

    populatePaginationInfo() {              
        this.#paginationInfo.textContent = `Trang ${this.#currentPage} / ${this.#totalPages}`;
    }

    #render() {
        return `
<div class="flex items-center justify-between w-full text-gray-600 dark:text-gray-400 bg-gray-100 rounded-lg dark:bg-gray-600 max-w-fit">
    <button id="prev-btn" type="button" class="inline-flex items-center justify-center h-8 px-1 w-6 bg-gray-100 rounded-s-lg dark:bg-gray-600 hover:bg-gray-200 dark:hover:bg-gray-800 focus:outline-none focus:ring-0 focus:ring-gray-200 dark:focus:ring-gray-800">
        <svg class="w-2 h-2 rtl:rotate-180" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 6 10">
            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 1 1 5l4 4" />
        </svg>
        <span class="sr-only">Previous page</span>
    </button>
    <span id="pagination-info" class="flex-shrink-0 mx-1 text-sm font-medium space-x-0.5 rtl:space-x-reverse">Trang ${this.#currentPage} / ${this.#totalPages}</span>
    <button id="next-btn" type="button" class="inline-flex items-center justify-center h-8 px-1 w-6 bg-gray-100 rounded-e-lg dark:bg-gray-600 hover:bg-gray-200 dark:hover:bg-gray-800 focus:outline-none focus:ring-0 focus:ring-gray-200 dark:focus:ring-gray-800">
        <svg class="w-2 h-2 rtl:rotate-180" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 6 10">
            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 9 4-4-4-4" />
        </svg>
        <span class="sr-only">Next page</span>
    </button>
</div>
        `;
    }

    
}