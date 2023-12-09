import { Course } from "./course.model.js";
import { PaginationMetadata } from "./pagination-metadata.model.js";

export class CoursePaginationMetadata {
    courses;
    paginationMetadata;

    constructor(courses = [new Course], paginationMetadata = new PaginationMetadata()) {
        this.courses = courses;
        this.paginationMetadata = paginationMetadata;
    }
}