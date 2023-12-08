import { Course } from "../models/course.model.js";

export class CourseGridComponent {

    #container;

    constructor(container = new HTMLElement()) {
        this.#container = container;
    }

    populate(courses = [new Course()]) {
        this.#container.innerHTML = "";
        courses.forEach(course => {
            this.#container.insertAdjacentHTML("beforeend", `
        <!-- Active: "border-violet-600 ring-2 ring-violet-600", Not Active: "border-gray-300" -->
        <label data-id="${course.id}" class="course relative flex cursor-pointer rounded-lg bg-violet-50 p-4 focus:outline-none">
            <input type="radio" name="project-type" value="Newsletter" class="sr-only" aria-labelledby="project-type-0-label" aria-describedby="project-type-0-description-0 project-type-0-description-1">
            <span class="flex flex-1">
                <span class="flex flex-col">                  
                    <span class="course-name block text-sm font-medium text-violet-800">${course.name}</span>
                    <span class="mt-1 flex items-center text-sm text-gray-500">Chưa có câu hỏi nào được tạo</span>
                    <span class="mt-6 text-sm font-medium text-gray-900">0 câu hỏi</span>
                </span>
            </span>
            <!-- Not Checked: "invisible" -->
            <svg class="checkbox h-5 w-5 text-violet-600 invisible" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z" clip-rule="evenodd" />
            </svg>
            <!--
              Active: "border", Not Active: "border-2"
              Checked: "border-violet-600", Not Checked: "border-transparent"
            -->
            <span class="pointer-events-none absolute -inset-px rounded-lg border-transparent" aria-hidden="true"></span>
        </label>
            `);
        });
    }
}