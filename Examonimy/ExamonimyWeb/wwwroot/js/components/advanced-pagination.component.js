import { BaseComponent } from "./base.component.js";

export class AdvancedPaginationComponent extends BaseComponent {
    #totalCount = 0;
    #pageSize = 0;
    #currentPage = 0;
    #totalPages = 0;
    #unitName = "";
    #fromItemNumber = 1;

    #container;   
    #paginationInfoContainer;  
    #buttonContainer;

    _events = {
        next: [],
        prev: [],
        clickPage: []
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
        this.#paginationInfoContainer = this.#container.querySelector("#pagination-info-container");
        this.#buttonContainer = this.#container.querySelector("#button-container");

        this.#highlightCurrentPage();      

        this.#buttonContainer.addEventListener("click", event => {
            const clickedPageNumberButton = event.target.closest(".page-number-btn");
            if (clickedPageNumberButton) {
                const pageNumber = Number(clickedPageNumberButton.textContent);
                this.#currentPage = pageNumber;
                this.#fromItemNumber = this.#pageSize * (this.#currentPage - 1) + 1;
                this._trigger("clickPage", this.#currentPage);
                return;
            }

            const clickedPrevButton = event.target.closest("#prev-btn");
            if (clickedPrevButton) {
                if (this.#currentPage === 1)
                    return;
                this.#currentPage--;
                this.#fromItemNumber -= this.#pageSize;
                this._trigger("prev", this.#currentPage);
                return;
            }

            const clickedNextButton = event.target.closest("#next-btn");
            if (clickedNextButton) {
                if (this.#currentPage === this.#totalPages)
                    return;
                this.#currentPage++;
                this.#fromItemNumber += this.#pageSize;
                this._trigger("next", this.#currentPage);
            }
        });
    }   

    #highlightCurrentPage() {
        this.#container.querySelector(`#page-${this.#currentPage}-btn`).classList.remove("text-gray-600");
        this.#container.querySelector(`#page-${this.#currentPage}-btn`).classList.add(..."text-gray-700 bg-gray-100".split(" "));  
    }

    set totalCount(value) {
        this.#totalCount = value;
    }

    set pageSize(value) {
        this.#pageSize = value;
    }

    set currentPage(value) {
        this.#currentPage = value;
    }

    set totalPages(value) {
        this.#totalPages = value;
    }

    populatePagination() {
        this.#paginationInfoContainer.innerHTML = this.#totalPages > 0 ? this.#renderPaginationInfo() : "";
        this.#buttonContainer.innerHTML = this.#totalPages > 0 ? this.#renderButtons() : "";
    }

    #renderButtons() {
        let markupForPageNumberButtons;
        if ((this.#totalPages >= 9 && this.#currentPage <= 3) || (this.#totalPages >= 9 && this.#currentPage >= this.#totalPages - 2)) {
            markupForPageNumberButtons = `
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
            markupForPageNumberButtons = `
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
            markupForPageNumberButtons = "";
            for (let i = 0; i < this.#totalPages; i++) {
                markupForPageNumberButtons = markupForPageNumberButtons.concat(`
                <button id="page-${i + 1}-btn" type="button" class="page-number-btn rounded-md relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-600">${i + 1}</button>
                `);
            }
        }       
        return `
<button type="button" class="prev-btn relative inline-flex items-center rounded-l-md px-2 py-2 text-gray-400 hover:bg-gray-50 focus:z-20">
    <span class="sr-only">Previous</span>
    <svg class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
        <path fill-rule="evenodd" d="M12.79 5.23a.75.75 0 01-.02 1.06L8.832 10l3.938 3.71a.75.75 0 11-1.04 1.08l-4.5-4.25a.75.75 0 010-1.08l4.5-4.25a.75.75 0 011.06.02z" clip-rule="evenodd" />
    </svg>
</button>
<div id="page-number-button-container">
    ${markupForPageNumberButtons}
</div>
<button type="button" class="next-btn rounded-md relative inline-flex items-center rounded-r-md px-2 py-2 text-gray-400 hover:bg-gray-50 focus:z-20">
    <span class="sr-only">Next</span>
    <svg class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
        <path fill-rule="evenodd" d="M7.21 14.77a.75.75 0 01.02-1.06L11.168 10 7.23 6.29a.75.75 0 111.04-1.08l4.5 4.25a.75.75 0 010 1.08l-4.5 4.25a.75.75 0 01-1.06-.02z" clip-rule="evenodd" />
    </svg>
</button>
        `;
    }

    #renderPaginationInfo() {
        return `
            <p class="text-sm text-gray-700">
                Đang hiển thị
                <span id="from-item-number" class="font-medium">${this.#fromItemNumber}</span>
                đến
                <span id="to-item-number" class="font-medium">${this.#fromItemNumber + this.#pageSize - 1 > this.#totalCount ? this.#totalCount : this.#fromItemNumber + this.#pageSize - 1}</span>
                trong số
                <span id="total-count" class="font-medium">${this.#totalCount}</span>
                ${this.#unitName}
            </p>
        `;
    }
    

    #render() {
        
        return `
<div class="flex items-center justify-between border-t border-gray-100 bg-white px-4 py-3 sm:px-6">
    <div class="flex flex-1 justify-between sm:hidden">
        <a href="#" class="relative inline-flex items-center rounded-md border border-gray-300 bg-white px-4 py-2 text-sm font-medium text-gray-700 hover:bg-gray-50">Previous</a>
        <a href="#" class="relative ml-3 inline-flex items-center rounded-md border border-gray-300 bg-white px-4 py-2 text-sm font-medium text-gray-700 hover:bg-gray-50">Next</a>
    </div>
    <div class="hidden sm:flex sm:flex-1 sm:items-center sm:justify-between">
        <div id="pagination-info-container">
            ${this.#totalPages > 0 ? this.#renderPaginationInfo() : ""}
        </div>
        <div>
            <nav id="button-container" class="isolate inline-flex -space-x-px rounded-md">
                ${this.#totalPages > 0 ? this.#renderButtons() : ""}
            </nav>
        </div>
    </div>
</div>
        `;
    }
}