// Imports
import { BASE_API_URL } from "../config.js";
import { CourseGridComponent } from "../components/course-grid.component.js";
import { Course } from "../models/course.model.js";
import { changeHtmlBackgroundColorToWhite } from "../helpers/markup.helper.js";
import { RequestParams } from "../models/request-params.model.js";

// DOM selectors
const courseContainer = document.querySelector("#course-container");
// States
const courseGridComponent = new CourseGridComponent(courseContainer);
const courseRequestParams = new RequestParams(12, 1);
// Function expressions
const fetchCourses = async () => {
    const res = await fetch(`${BASE_API_URL}/course?pageSize=${courseRequestParams.pageSize}&pageNumber=${courseRequestParams.pageNumber}`, {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    });
    const data = await res.json();
    const courses = [new Course()];
    Object.assign(courses, data);
    return courses;
}
// Event listeners

// On load
changeHtmlBackgroundColorToWhite();

(async () => {
    const courses = await fetchCourses();
    courseGridComponent.populate(courses);
})();