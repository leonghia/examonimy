import { MAX_PAGE_SIZE } from "../config.js";
export class RequestParams {
    #pageSize = 10;
    #pageNumber = 1;

    constructor(pageSize, pageNumber) {
        this.#pageSize = pageSize > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : pageSize;
        this.#pageNumber = pageNumber;
    }

    get pageSize() {
        return this.#pageSize;
    }

    get pageNumber() {
        return this.#pageNumber;
    }

    set pageSize(value) {
        this.#pageSize = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value;
    }

    set pageNumber(value) {
        this.#pageNumber = value;
    }
}