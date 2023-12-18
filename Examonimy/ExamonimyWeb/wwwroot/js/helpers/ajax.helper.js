import { BASE_API_URL, PAGINATION_METADATA_HEADER } from "../config.js"
import { GetResponse } from "../models/get-response.model.js";
import { ProblemDetails } from "../models/problem-details.model.js";
import { RequestParams } from "../models/request-params.model.js";

export const fetchData = async (route = "", requestParams = new RequestParams()) => {
    const getResponse = new GetResponse();
    const url = new URL(`${BASE_API_URL}/${route}`);
    for (const [k, v] of Object.entries(requestParams)) {
        if (v) {
            url.searchParams.set(k, v);
        }
    }
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
        getResponse.paginationMetadata.pageSize = paginationMetadata.pageSize;
        getResponse.paginationMetadata.totalCount = paginationMetadata.totalCount;
        return getResponse;

    } catch (err) {
        console.error(err);
    }
}

export const fetchDataById = async (route = "", id = 0) => {
    try {
        const res = await fetch(`${BASE_API_URL}/${route}/${id}`);
        if (!res.ok) {
            throw res;
        }
        const data = await res.json();
        return data;
    } catch (err) {
        console.error(err);
    }
}

export const postData = async (route = "", dataToPost) => {
    const url = `${BASE_API_URL}/${route}`;

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

export const putData = async (route = "", id = 0, dataToPut) => {
    const url = `${BASE_API_URL}/${route}/${id}`;
    try {
        const res = await fetch(url, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(dataToPut)
        });

        if (!res.ok) {
            const problemDetails = new ProblemDetails();
            Object.assign(problemDetails, await res.json());
            throw problemDetails;
        }


    } catch (err) {
        console.error(err);
    }
}

export const deleteData = async (route = "", id = 0) => {
    const url = `${BASE_API_URL}/${route}/${id}`;
    try {
        const res = await fetch(url, {
            method: "DELETE"
        });

        if (!res.ok) {
            const problemDetails = new ProblemDetails();
            Object.assign(problemDetails, await res.json());
            throw problemDetails;
        }
    } catch (err) {
        console.error(err);
    }
}