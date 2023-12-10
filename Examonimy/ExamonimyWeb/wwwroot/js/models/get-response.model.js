import { PaginationMetadata } from "./pagination-metadata.model.js";

export class GetResponse {
    data = [];
    paginationMetadata = new PaginationMetadata();

    constructor(data = [], paginationMetadata = new PaginationMetadata()) {
        this.data = data;
        this.paginationMetadata = paginationMetadata;
    }
}