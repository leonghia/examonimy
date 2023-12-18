import { BaseComponent } from "./base.component.js";

export class AnswerRadioMultipleChoiceComponent extends BaseComponent {
    #container;
    #answer = "";
    _events = {
        click: []
    }

    constructor(container = new HTMLElement(), answer = "") {
        super();
        this.#container = container;
        this.#answer = answer;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();
        this.#highlightSavedAnswer(this.#answer);

        this.#container.addEventListener("click", event => {
            const clicked = event.target.closest(".answer");
            if (!clicked)
                return;
            Array.from(this.#container.querySelectorAll(".answer")).forEach(e => {
                e.classList.remove(..."bg-green-600 hover:bg-green-700 text-white".split(" "));
                e.classList.add(..."bg-gray-100 hover:bg-gray-200 text-gray-900".split(" "));
            });
            clicked.classList.remove(..."bg-gray-100 hover:bg-gray-200 text-gray-900".split(" "));
            clicked.classList.add(..."bg-green-600 hover:bg-green-700 text-white".split(" "));
            this._trigger("click", clicked.dataset.value);
        });
    }

    #highlightSavedAnswer(savedAnswer) {
        const label = this.#container.querySelector(`.answer[data-value="${savedAnswer}"]`);
        label.classList.remove(..."bg-gray-100 hover:bg-gray-200 text-gray-900".split(" "));
        label.classList.add(..."bg-green-600 hover:bg-green-700 text-white".split(" "));
    }

    #render() {
        return `
<label class="block text-sm leading-6 text-gray-700 font-medium mb-4">Đáp án</label>
<fieldset class="mt-2">
    <legend class="sr-only">Choose a correct answer</legend>
    <div class="flex gap-4">      
        <label data-value="A" class="answer w-10 h-10 bg-gray-100 text-gray-900 hover:bg-gray-200 flex items-center justify-center rounded-full py-3 px-3 text-sm font-semibold uppercase cursor-pointer focus:outline-none">
            <span>A</span>
        </label>       
        <label data-value="B" class="answer w-10 h-10 bg-gray-100 text-gray-900 hover:bg-gray-200 flex items-center justify-center rounded-full py-3 px-3 text-sm font-semibold uppercase cursor-pointer focus:outline-none">
            <span>B</span>
        </label>       
        <label data-value="C" class="answer w-10 h-10 bg-gray-100 text-gray-900 hover:bg-gray-200 flex items-center justify-center rounded-full py-3 px-3 text-sm font-semibold uppercase cursor-pointer focus:outline-none">
            <span>C</span>
        </label>      
        <label data-value="D" class="answer w-10 h-10 bg-gray-100 text-gray-900 hover:bg-gray-200 flex items-center justify-center rounded-full py-3 px-3 text-sm font-semibold uppercase cursor-pointer focus:outline-none">
            <span>D</span>
        </label>
    </div>
</fieldset>
        `;
    }
}