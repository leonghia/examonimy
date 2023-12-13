import { MAX_PAGE_SIZE } from "../config.js";
export class RequestParams {
    pageSize = 10;
    pageNumber = 1;
    searchQuery = null;

    constructor(searchQuery = null, pageSize = 10, pageNumber = 1) {
        this.searchQuery = searchQuery;
        this.pageSize = pageSize || 10;
        this.pageNumber = pageNumber || 1;
    }

    
}