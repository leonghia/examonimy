import { toggleSegment } from "../helpers/markup.helper.js";
import { BaseComponent } from "./base.component.js";

export class StepperComponent extends BaseComponent {

    #steps;
    #container;
    _events = {
        click: [],
        clickStep3: [],
        clickStep4: []
    }

    constructor(container = new HTMLElement(), steps = [""]) {
        super();
        this.#container = container;
        this.#steps = steps;
        
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();

        const step3 = this.#container.querySelector("#step-3");
        const step4 = this.#container.querySelector("#step-4");

        this.#markStepAsCurrent(this.#container.querySelector("#step-1"));

        this.#container.addEventListener("click", event => {
            const clickedStepButton = event.target.closest(".step-btn");
            if (!clickedStepButton)
                return;

            const clickedStep = clickedStepButton.closest(".step");

            if (Number(clickedStep.dataset.order) === this.#steps.length) {
                this.#markStepAsCompleted(clickedStep);
                
            } else {               
                
                if (!clickedStep.dataset.completed)
                    this.#markStepAsCurrent(clickedStep);
            }
            
            if (clickedStep.previousElementSibling) {
                this.#markStepAsCompleted(clickedStep.previousElementSibling);
            }    
           

            const order = Number(clickedStep.dataset.order);
            const segments = Array.from(document.querySelectorAll(".segment"));
            toggleSegment(segments, order);

            this._trigger("click", order);
        });

        step3.addEventListener("click", () => {
            this._trigger("clickStep3");
        });

        step4.addEventListener("click", () => {
            this._trigger("clickStep4");
        });
    }

    disconnectedCallback() {
        this.#container.innerHTML = "";
    }

    #markStepAsCurrent(step = new HTMLElement()) {
        const stepOrder = Number(step.dataset.order);
        const stepName = this.#steps[stepOrder - 1];
        step.innerHTML = `
<!-- Current Step -->
<div class="absolute inset-0 flex items-center" aria-hidden="true">
    <div class="h-0.5 w-full ${step.dataset.completed ? "bg-green-500" : "bg-gray-200"}"></div>
</div>
<button type="button" class="step-btn relative flex h-8 w-8 items-center justify-center rounded-full border-2 border-green-500 bg-white">
    <span class="h-2.5 w-2.5 rounded-full bg-green-500" aria-hidden="true"></span>
    <span class="step-name absolute top-10 p-0 overflow-hidden whitespace-nowrap border-0 text-base font-bold text-green-500">${stepOrder}. ${stepName}</span>
</button>
        `;
    }

    #markStepAsCompleted(step = new HTMLElement()) {
        const stepOrder = Number(step.dataset.order);
        const stepName = this.#steps[stepOrder - 1];
        step.innerHTML = `
<!-- Completed Step -->
<div class="absolute inset-0 flex items-center" aria-hidden="true">
    <div class="h-0.5 w-full bg-green-500"></div>
</div>
<button type="button" class="step-btn relative flex h-8 w-8 items-center justify-center rounded-full bg-green-500 hover:bg-green-600">
    <svg class="h-5 w-5 text-white" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
        <path fill-rule="evenodd" d="M16.704 4.153a.75.75 0 01.143 1.052l-8 10.5a.75.75 0 01-1.127.075l-4.5-4.5a.75.75 0 011.06-1.06l3.894 3.893 7.48-9.817a.75.75 0 011.05-.143z" clip-rule="evenodd" />
    </svg>
    <span class="step-name absolute top-10 p-0 overflow-hidden whitespace-nowrap border-0 text-base font-bold text-green-500">${stepOrder}. ${stepName}</span>
</button>
        `;
        step.dataset.completed = "true";
    }

    #markStepAsUpcoming(step = new HTMLElement()) {
        const stepOrder = Number(step.dataset.order);
        const stepName = this.#steps[stepOrder - 1];
        step.innerHTML = `
    <!-- Upcoming Step -->
    <div class="absolute inset-0 flex items-center" aria-hidden="true">
        <div class="h-0.5 w-full bg-gray-200"></div>
    </div>
    <button type="button" class="step-btn group relative flex h-8 w-8 items-center justify-center rounded-full border-2 border-gray-300 bg-white hover:border-gray-400">
        <span class="h-2.5 w-2.5 rounded-full bg-transparent group-hover:bg-gray-300" aria-hidden="true"></span>
        <span class="step-name absolute top-10 p-0 overflow-hidden whitespace-nowrap border-0 text-base font-bold text-gray-300">${stepOrder}. ${stepName}</span>
    </button>
        `;
    }

    #render() {
        return this.#steps.reduce((accumulator, currentValue, currentIndex) => {
            return currentIndex === this.#steps.length - 1 ? accumulator + `
<li class="step relative" data-order="${currentIndex + 1}" id="step-${currentIndex + 1}">
    <!-- Upcoming Step -->
    <div class="absolute inset-0 flex items-center" aria-hidden="true">
        <div class="h-0.5 w-full bg-gray-200"></div>
    </div>
    <button type="button" class="step-btn group relative flex h-8 w-8 items-center justify-center rounded-full border-2 border-gray-300 bg-white hover:border-gray-400">
        <span class="h-2.5 w-2.5 rounded-full bg-transparent group-hover:bg-gray-300" aria-hidden="true"></span>
        <span class="step-name absolute top-10 p-0 overflow-hidden whitespace-nowrap border-0 text-base font-bold text-gray-300">${currentIndex + 1}. ${currentValue}</span>
    </button>
</li>
            ` : accumulator + `
<li class="step relative pr-8 sm:pr-96" data-order="${currentIndex + 1}" id="step-${currentIndex + 1}">
    <!-- Upcoming Step -->
    <div class="absolute inset-0 flex items-center" aria-hidden="true">
        <div class="h-0.5 w-full bg-gray-200"></div>
    </div>
    <button type="button" class="step-btn group relative flex h-8 w-8 items-center justify-center rounded-full border-2 border-gray-300 bg-white hover:border-gray-400">
        <span class="h-2.5 w-2.5 rounded-full bg-transparent group-hover:bg-gray-300" aria-hidden="true"></span>
        <span class="step-name absolute top-10 p-0 overflow-hidden whitespace-nowrap border-0 text-base font-bold text-gray-300">${currentIndex + 1}. ${currentValue}</span>
    </button>
</li>
            `;
        }, "");        
    }
}