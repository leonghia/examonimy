import { Course } from "../models/course.model.js";

export class CourseGridComponent {

    
    #courseContainer;
    #courses;

    constructor(courseContainer = new HTMLElement(), courses = [new Course()]) {
        this.#courseContainer = courseContainer;
        this.#courses = courses;
    }

    set courses(data) {
        this.#courses = data;
    }

    render() {      
        return this.#courses.reduce((accumulator, course) => {
            return accumulator + `
        <!-- Active: "border-violet-600 ring-2 ring-violet-600", Not Active: "border-gray-300" -->
        <label data-id="${course.id}" class="course-label relative flex cursor-pointer rounded-lg bg-violet-50 p-4 focus:outline-none">
            <input type="radio" name="project-type" value="Newsletter" class="sr-only" aria-labelledby="project-type-0-label" aria-describedby="project-type-0-description-0 project-type-0-description-1">
            <span class="flex flex-1">
                <span class="flex flex-col">                  
                    <span class="course-name block text-sm font-medium text-violet-800">${course.name}</span>
                    <span class="mt-1 flex items-center text-sm text-gray-500">Chưa có đề thi nào được tạo</span>
                    <span class="mt-6 text-sm font-medium text-gray-900">0 đề thi</span>
                </span>
            </span>
            <!-- Not Checked: "invisible" -->
            <svg class="course-checkbox h-5 w-5 text-violet-600 invisible" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z" clip-rule="evenodd" />
            </svg>
            <!--
              Active: "border", Not Active: "border-2"
              Checked: "border-violet-600", Not Checked: "border-transparent"
            -->
            <span class="course-border pointer-events-none absolute -inset-px rounded-lg border-transparent" aria-hidden="true"></span>
        </label>
        `}, "");
    }

    highlightCourse(clickedCourse = new HTMLElement()) {
        const courseLabels = Array.from(this.#courseContainer.querySelectorAll(".course-label"));
        courseLabels.forEach(courseLabel => {
            courseLabel.classList.remove(..."border-violet-600 ring-2 ring-violet-600".split(" "));           
            const checkboxSvg = courseLabel.querySelector(".course-checkbox");
            checkboxSvg.classList.add("invisible");
            const labelBorder = courseLabel.querySelector(".course-border");
            labelBorder.classList.remove(..."border-2 border-violet-600".split(" "));
            labelBorder.classList.add("border-transparent");
        });      
        clickedCourse.classList.add(..."border-violet-600 ring-2 ring-violet-600".split(" "));     
        const checkboxSvg = clickedCourse.querySelector(".course-checkbox");
        checkboxSvg.classList.remove("invisible");
        const labelBorder = clickedCourse.querySelector(".course-border");
        labelBorder.classList.remove("border-transparent");
        labelBorder.classList.add(..."border-2 border-violet-600".split(" "));  
    }
}