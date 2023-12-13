import { BASE_API_URL, PAGINATION_METADATA_HEADER } from "../config.js"
import { GetResponse } from "../models/get-response.model.js";
import { RequestParams } from "../models/request-params.model.js";

export const fetchData = async (entityName = "", requestParams = new RequestParams()) => {
    const getResponse = new GetResponse();
    const url = new URL(`${BASE_API_URL}/${entityName}`);
    if (requestParams.searchQuery)
        url.searchParams.set("searchQuery", requestParams.searchQuery);
    url.searchParams.set("pageSize", requestParams.pageSize);
    url.searchParams.set("pageNumber", requestParams.pageNumber);    
    const res = await fetch(url);
    getResponse.data = await res.json();
    const paginationMetadata = JSON.parse(res.headers.get(PAGINATION_METADATA_HEADER));
    getResponse.paginationMetadata.currentPage = paginationMetadata.currentPage;
    getResponse.paginationMetadata.totalPages = paginationMetadata.totalPages;
    return getResponse;
}