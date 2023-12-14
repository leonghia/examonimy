import { BASE_API_URL, PAGINATION_METADATA_HEADER } from "../config.js"
import { GetResponse } from "../models/get-response.model.js";
import { ProblemDetails } from "../models/problem-details.model.js";
import { RequestParams } from "../models/request-params.model.js";

export const fetchData = async (routeName = "", requestParams = new RequestParams()) => {
    const getResponse = new GetResponse();
    const url = new URL(`${BASE_API_URL}/${routeName}`);
    if (requestParams.searchQuery)
        url.searchParams.set("searchQuery", requestParams.searchQuery);
    url.searchParams.set("pageSize", requestParams.pageSize);
    url.searchParams.set("pageNumber", requestParams.pageNumber);    

    try {
        const res = await fetch(url);

        if (!res.ok) {
            const problemDetails = new ProblemDetails();
            Object.assign(problemDetails, await res.json());
            throw problemDetails;
        }

        getResponse.data = await res.json();
        const paginationMetadata = JSON.parse(res.headers.get(PAGINATION_METADATA_HEADER));
        getResponse.paginationMetadata.currentPage = paginationMetadata.currentPage;
        getResponse.paginationMetadata.totalPages = paginationMetadata.totalPages;
        return getResponse;

    } catch (err) {
        console.error(err);
    }
}

export const postData = async (routeName = "", dataToPost) => {
    const url = `${BASE_API_URL}/${routeName}`;

    try {
        const res = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"              
            },
            body: JSON.stringify(dataToPost)
        });

        if (!res.ok) {
            const problemDetails = new ProblemDetails();
            Object.assign(problemDetails, await res.json());
            throw problemDetails;
        }    

        const bodyData = await res.json();
        return bodyData;

    } catch (err) {
        console.error(err);
    }
    
}