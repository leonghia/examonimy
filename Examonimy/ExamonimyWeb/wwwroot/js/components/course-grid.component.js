import { Course } from "../models/course.model.js";
import { BaseComponent } from "./base.component.js";

export class CourseGridComponent extends BaseComponent {

    
    #container;
    #courses = [new Course()];
    _events = {
        click: []
    }

    constructor(courseContainer = new HTMLElement(), courses = [new Course()]) {
        super();
        this.#container = courseContainer;
        this.#courses = courses;
        
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();

        this.#container.addEventListener("click", event => {
            const clickedCourse = event.target.closest(".course-label");
            if (!clickedCourse)
                return;
            this.#highlightCourse(clickedCourse);
            const courseId = Number(clickedCourse.dataset.id);
            const courseName = clickedCourse.querySelector(".course-name").textContent;
            const courseCode = clickedCourse.dataset.courseCode;
            const course = new Course(courseId, courseName, courseCode);

            this._trigger("click", course);        
        });
    }

    populateCourses(courses = [new Course()]) {
        this.#courses = courses;
        this.#container.innerHTML = this.#render();
    }

    set courses(data) {
        this.#courses = data;
    }

    highlightCourseById(courseId) {
        const courseLabelEl = this.#container.querySelector(`#course-label-${courseId}`);
        if (!courseLabelEl)
            return;
        this.#highlightCourse(courseLabelEl);
    }

    get courses() {
        return this.#courses;
    }

    #render() {     
        return this.#courses.reduce((accumulator, course) => {          
            return accumulator + `
        <!-- Active: "border-indigo-600 ring-2 ring-indigo-600", Not Active: "border-gray-300" -->
        <label id="course-label-${course.id}" data-id="${course.id}" data-course-code="${course.courseCode}" class="course-label relative flex cursor-pointer rounded-lg bg-indigo-200 p-4 focus:outline-none">         
            <span class="flex flex-1">
                <span class="flex flex-col">                  
                    <span class="course-name block text-sm font-medium text-indigo-800">${course.name}</span>
                    <span class="mt-1 flex items-center text-sm text-gray-500">${course.courseCode}</span>
                    <span class="mt-6 text-sm font-medium text-gray-900">${course.numbersOfExamPapers || course.numbersOfQuestions} ${course.numbersOfExamPapers ? "đề thi" : "câu hỏi"}</span>
                </span>
            </span>
            <!-- Not Checked: "invisible" -->
            <svg class="course-checkbox h-5 w-5 text-indigo-600 invisible" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z" clip-rule="evenodd" />
            </svg>
            <!--
              Active: "border", Not Active: "border-2"
              Checked: "border-indigo-600", Not Checked: "border-transparent"
            -->
            <span class="course-border pointer-events-none absolute -inset-px rounded-lg border-transparent" aria-hidden="true"></span>
        </label>
        `}, "");
    }

    #highlightCourse(courseLabelElement = new HTMLElement()) {
        const courseLabels = Array.from(this.#container.querySelectorAll(".course-label"));
        courseLabels.forEach(courseLabel => {
            courseLabel.classList.remove(..."border-indigo-600 ring-2 ring-indigo-600".split(" "));           
            const checkboxSvg = courseLabel.querySelector(".course-checkbox");
            checkboxSvg.classList.add("invisible");
            const labelBorder = courseLabel.querySelector(".course-border");
            labelBorder.classList.remove(..."border-2 border-indigo-600".split(" "));
            labelBorder.classList.add("border-transparent");
        });      
        courseLabelElement.classList.add(..."border-indigo-600 ring-2 ring-indigo-600".split(" "));     
        const checkboxSvg = courseLabelElement.querySelector(".course-checkbox");
        checkboxSvg.classList.remove("invisible");
        const labelBorder = courseLabelElement.querySelector(".course-border");
        labelBorder.classList.remove("border-transparent");
        labelBorder.classList.add(..."border-2 border-indigo-600".split(" "));  
    }
}