import { BaseComponent } from "./base.component.js";

export class AdvancedPaginationComponent extends BaseComponent {
    #totalCount = 0;
    #pageSize = 0;
    #currentPage = 0;
    #totalPages = 0;
    #unitName = "";

    #container;

    _events = {
        next: [],
        prev: []
    }

    constructor(container = new HTMLElement(), unitName = "", totalCount = 0, pageSize = 0, currentPage = 0, totalPages = 0) {
        super();
        this.#container = container;
        this.#totalCount = totalCount;
        this.#pageSize = pageSize;
        this.#currentPage = currentPage;
        this.#totalPages = totalPages;
        this.#unitName = unitName;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();
        this.#highlightCurrentPage();
    } 

    #highlightCurrentPage() {
        this.#container.querySelector(`#page-${this.#currentPage}-btn`).classList.remove("text-gray-600");
        this.#container.querySelector(`#page-${this.#currentPage}-btn`).classList.add(..."text-gray-700 bg-gray-100".split(" "));       
    }

    setPaginationFields(totalCount = 0, pageSize = 0, currentPage = 0, totalPages = 0) {
        this.#totalCount = totalCount;
        this.#pageSize = pageSize;
        this.#currentPage = currentPage;
        this.#totalPages = totalPages;
    }

    #render() {
        let markupForPageNumbers;
        if ((this.#totalPages >= 9 && this.#currentPage <= 3) || (this.#totalPages >= 9 && this.#currentPage >= this.#totalPages - 2)) {
            markupForPageNumbers = `
            <button id="page-${1}-btn" type="button" class="page-number-btn rounded-md relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-600">1</button>
            <button id="page-${2}-btn" type="button" class="page-number-btn rounded-md relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-600">2</button>
            <button id="page-${3}-btn" type="button" class="page-number-btn rounded-md relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-600">3</button>
            <span class="relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-700">...</span>
            <button id="page-${this.#totalPages - 2}-btn" type="button" class="page-number-btn rounded-md relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-600">${this.#totalPages - 2}</button>
            <button id="page-${this.#totalPages - 1}-btn" type="button" class="page-number-btn rounded-md relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-600">${this.#totalPages - 1}</button>
            <button id="page-${this.#totalPages}-btn" type="button" class="page-number-btn rounded-md relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-600">${this.#totalPages}</button>
            `;
        }
        if (this.#totalPages >= 9 && this.#currentPage > 3 && this.#currentPage < this.#totalPages - 2) {
            markupForPageNumbers = `
            <button id="page-${1}-btn" type="button" class="page-number-btn rounded-md relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-600">1</button>           
            <span class="relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-700">...</span>
            <button id="page-${this.#currentPage - 1}-btn" type="button" class="page-number-btn rounded-md relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-600">${this.#currentPage - 1}</button>
            <button id="page-${this.#currentPage}-btn" type="button" class="page-number-btn rounded-md relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-600">${this.#currentPage}</button>
            <button id="page-${this.#currentPage + 1}-btn" type="button" class="page-number-btn rounded-md relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-600">${this.#currentPage + 1}</button>
            <span class="relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-700">...</span>
            <button id="page-${this.#totalPages}-btn" type="button" class="page-number-btn rounded-md relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-600">${this.#totalPages}</button>
            `;
        }
        if (this.#totalPages < 9) {
            markupForPageNumbers = "";
            for (let i = 0; i < this.#totalPages; i++) {
                markupForPageNumbers = markupForPageNumbers.concat(`
                <button id="page-${i + 1}-btn" type="button" class="page-number-btn rounded-md relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-600">${i + 1}</button>
                `);
            }
        }
        return `
<div class="flex items-center justify-between border-t border-gray-100 bg-white px-4 py-3 sm:px-6">
    <div class="flex flex-1 justify-between sm:hidden">
        <a href="#" class="relative inline-flex items-center rounded-md border border-gray-300 bg-white px-4 py-2 text-sm font-medium text-gray-700 hover:bg-gray-50">Previous</a>
        <a href="#" class="relative ml-3 inline-flex items-center rounded-md border border-gray-300 bg-white px-4 py-2 text-sm font-medium text-gray-700 hover:bg-gray-50">Next</a>
    </div>
    <div class="hidden sm:flex sm:flex-1 sm:items-center sm:justify-between">
        <div>
            <p class="text-sm text-gray-700">
                Đang hiển thị              
                <span id="page-size" class="font-medium">${this.#pageSize}</span>
                trong số
                <span id="total-count" class="font-medium">${this.#totalCount}</span>
                ${this.#unitName}
            </p>
        </div>
        <div>
            <nav class="isolate inline-flex -space-x-px rounded-md" aria-label="Pagination">
                <button type="button" class="prev-btn relative inline-flex items-center rounded-l-md px-2 py-2 text-gray-400 hover:bg-gray-50 focus:z-20">
                    <span class="sr-only">Previous</span>
                    <svg class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                        <path fill-rule="evenodd" d="M12.79 5.23a.75.75 0 01-.02 1.06L8.832 10l3.938 3.71a.75.75 0 11-1.04 1.08l-4.5-4.25a.75.75 0 010-1.08l4.5-4.25a.75.75 0 011.06.02z" clip-rule="evenodd" />
                    </svg>
                </button>
                ${markupForPageNumbers}                           
                <button type="button" class="next-btn rounded-md relative inline-flex items-center rounded-r-md px-2 py-2 text-gray-400 hover:bg-gray-50 focus:z-20">
                    <span class="sr-only">Next</span>
                    <svg class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                        <path fill-rule="evenodd" d="M7.21 14.77a.75.75 0 01.02-1.06L11.168 10 7.23 6.29a.75.75 0 111.04-1.08l4.5 4.25a.75.75 0 010 1.08l-4.5 4.25a.75.75 0 01-1.06-.02z" clip-rule="evenodd" />
                    </svg>
                </button>
            </nav>
        </div>
    </div>
</div>
        `;
    }
}