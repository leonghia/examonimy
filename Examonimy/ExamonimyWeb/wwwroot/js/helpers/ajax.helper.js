import { BASE_API_URL, PAGINATION_METADATA_HEADER } from "../config.js"
import { GetResponse } from "../models/get-response.model.js";

export const fetchData = async (entityName = "", pageSize = 50, pageNumber = 1) => {
    const getResponse = new GetResponse();
    const res = await fetch(`${BASE_API_URL}/${entityName}?pageSize=${pageSize}&pageNumber=${pageNumber}`);
    getResponse.data = await res.json();
    const paginationMetadata = JSON.parse(res.headers.get(PAGINATION_METADATA_HEADER));
    getResponse.paginationMetadata.currentPage = paginationMetadata.currentPage;
    getResponse.paginationMetadata.totalPages = paginationMetadata.totalPages;
    return getResponse;
}