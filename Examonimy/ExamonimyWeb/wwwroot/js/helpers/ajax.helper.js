import { BASE_API_URL, PAGINATION_METADATA_HEADER } from "../config.js"
import { GetResponse } from "../models/get-response.model.js";
import { ProblemDetails } from "../models/problem-details.model.js";
import { MediaType } from "./media-type.helper.js";

export const fetchData = async (route = "", mediaType = MediaType.Default) => {
    const getResponse = new GetResponse();
    const url = new URL(`${BASE_API_URL}/${route}`);    
    try {
        const res = await fetch(url, {
            method: "GET",
            headers: {
                "Accept": mediaType           
            }
        });

        if (!res.ok) {
            const problemDetails = new ProblemDetails();
            Object.assign(problemDetails, await res.json());
            throw problemDetails;
        }

        getResponse.data = await res.json();
        if (res.headers.get(PAGINATION_METADATA_HEADER)) {
            const paginationMetadata = JSON.parse(res.headers.get(PAGINATION_METADATA_HEADER));
            getResponse.paginationMetadata.currentPage = paginationMetadata.currentPage;
            getResponse.paginationMetadata.totalPages = paginationMetadata.totalPages;
            getResponse.paginationMetadata.pageSize = paginationMetadata.pageSize;
            getResponse.paginationMetadata.totalCount = paginationMetadata.totalCount;
        }       
        return getResponse;

    } catch (err) {
        throw err;
    }
}

export const fetchDataById = async (route = "", id = 0, mediaType = MediaType.Default) => {
    try {
        const res = await fetch(`${BASE_API_URL}/${route}/${id}`, {
            method: "GET",
            headers: {
                "Accept": mediaType,
                "TimezoneOffset": new Date().getTimezoneOffset()
            }
        });
        if (!res.ok) {
            throw res;
        }
        const data = await res.json();
        return data;
    } catch (err) {
        throw err;
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

        const dataReturned = await res.text();

        if (dataReturned) {
            return JSON.parse(dataReturned);
        }

    } catch (err) {
        throw err;
    }
    
}

export const putData = async (route = "", dataToPut = null) => {
    const url = `${BASE_API_URL}/${route}`;
    try {
        const res = await fetch(url, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(dataToPut ?? {})
        });

        if (!res.ok) {
            const problemDetails = new ProblemDetails();
            Object.assign(problemDetails, await res.json());
            throw problemDetails;
        }


    } catch (err) {
        throw err;
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
        throw err;
    }
}