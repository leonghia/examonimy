import { BASE_API_URL, PAGINATION_METADATA_HEADER } from "../config.js"
import { CoursePaginationMetadata } from "../models/course-pagination-metadata.model.js";

export const fetchCourses = async (pageSize = 1, pageNumber = 999) => {
    const coursePaginationMetadata = new CoursePaginationMetadata();
    const res = await fetch(`${BASE_API_URL}/course?pageSize=${pageSize}&pageNumber=${pageNumber}`);
    coursePaginationMetadata.courses = await res.json();
    const paginationMetadata = JSON.parse(res.headers.get(PAGINATION_METADATA_HEADER));
    coursePaginationMetadata.paginationMetadata.CurrentPage = paginationMetadata.CurrentPage;
    coursePaginationMetadata.paginationMetadata.TotalPages = paginationMetadata.TotalPages;
    return coursePaginationMetadata;
}