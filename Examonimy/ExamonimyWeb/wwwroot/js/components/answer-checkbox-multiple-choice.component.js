import { BaseComponent } from "./base.component.js";

export class AnswerCheckboxMultipleChoiceComponent extends BaseComponent {
    #container;
    #answers = [""];
    _events = {
        click: []
    };

    constructor(container = new HTMLElement(), answers = [""]) {
        super();
        this.#container = container;
        this.#answers = answers;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();
        this.#highlightAnswers(this.#answers);

        this.#container.addEventListener("click", event => {
            const clickedLabel = event.target.closest(".answer");
            if (!clickedLabel)
                return;
            clickedLabel.classList.toggle("bg-gray-100");
            clickedLabel.classList.toggle("text-gray-900");
            clickedLabel.classList.toggle("hover:bg-gray-200");
            clickedLabel.classList.toggle("bg-green-600");
            clickedLabel.classList.toggle("text-white");
            clickedLabel.classList.toggle("hover:bg-green-700");

            if (clickedLabel.classList.contains("bg-green-600")) {
                this.#answers.push(clickedLabel.dataset.value);
            } else {
                const index = this.#answers.indexOf(clickedLabel.dataset.value);
                this.#answers.splice(index, 1);
            }
            this.#answers.sort((a, b) => a.localeCompare(b));
            this._trigger("click", this.#answers);
        });
    }

    #highlightAnswers(answers = [""]) {
        Array.from(this.#container.querySelectorAll(".answer")).forEach(label => {
            if (answers.indexOf(label.dataset.value) !== -1) {
                label.classList.remove(..."bg-gray-100 text-gray-900 hover:bg-gray-200".split(" "));
                label.classList.add(..."bg-green-600 text-white hover:bg-green-700".split(" "));
            }
        });
    }

    #render() {
        return `
<label class="block text-sm leading-6 text-gray-700 font-medium mb-4">Đáp án</label>
<fieldset class="mt-2">
    <legend class="sr-only">Choose correct answers</legend>
    <div class="flex gap-4">       
        <label data-value="A" class="answer w-10 h-10 bg-gray-100 text-gray-900 hover:bg-gray-200 flex items-center justify-center rounded-md py-3 px-3 text-sm font-semibold uppercase cursor-pointer focus:outline-none">
            <span>A</span>
        </label>       
        <label data-value="B" class="answer w-10 h-10 bg-gray-100 text-gray-900 hover:bg-gray-200 flex items-center justify-center rounded-md py-3 px-3 text-sm font-semibold uppercase cursor-pointer focus:outline-none">
            <span>B</span>
        </label>      
        <label data-value="C" class="answer w-10 h-10 bg-gray-100 text-gray-900 hover:bg-gray-200 flex items-center justify-center rounded-md py-3 px-3 text-sm font-semibold uppercase cursor-pointer focus:outline-none">
            <span>C</span>
        </label>       
        <label data-value="D" class="answer w-10 h-10 bg-gray-100 text-gray-900 hover:bg-gray-200 flex items-center justify-center rounded-md py-3 px-3 text-sm font-semibold uppercase cursor-pointer focus:outline-none">
            <span>D</span>
        </label>
    </div>
</fieldset>
        `;
    }


}