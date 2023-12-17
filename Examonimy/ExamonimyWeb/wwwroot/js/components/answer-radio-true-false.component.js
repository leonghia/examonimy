import { BaseComponent } from "./base.component.js";

export class AnswerRadioTrueFalseComponent extends BaseComponent {
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
        this.#highlightAnswer(this.#answer);

        this.#container.addEventListener("click", event => {
            const clickedLabel = event.target.closest(".answer");
            if (!clickedLabel)
                return;
            Array.from(this.#container.querySelectorAll(".answer")).forEach(label => {
                label.classList.remove(..."bg-green-600 text-white hover:bg-green-700".split(" "));
                label.classList.add(..."bg-gray-100 text-gray-900 hover:bg-gray-200".split(" "));           
            });

            clickedLabel.classList.remove(..."bg-gray-100 text-gray-900 hover:bg-gray-200".split(" "));
            clickedLabel.classList.add(..."bg-green-600 text-white hover:bg-green-700".split(" "));

            this.#answer = clickedLabel.dataset.value;
            this._trigger("click", this.#answer);
        });
    }

    #highlightAnswer(answer = "") {
        const label = this.#container.querySelector(`.answer[data-value="${answer}"]`);
        label.classList.remove(..."bg-gray-100 text-gray-900 hover:bg-gray-200".split(" "));
        label.classList.add(..."bg-green-600 text-white hover:bg-green-700".split(" "));
    }

    #render() {
        return `
<label class="block text-sm leading-6 text-gray-700 font-medium mb-4">Đáp án</label>
<fieldset class="mt-2">
    <legend class="sr-only">Choose a correct answer</legend>
    <div class="flex gap-4">       
        <label data-value="Đ" class="answer w-10 h-10 bg-gray-100 text-gray-900 hover:bg-gray-200 flex items-center justify-center rounded-full py-3 px-3 text-sm font-semibold uppercase cursor-pointer focus:outline-none">
            <span>Đ</span>
        </label>       
        <label data-value="S" class="answer w-10 h-10 bg-gray-100 text-gray-900 hover:bg-gray-200 flex items-center justify-center rounded-full py-3 px-3 text-sm font-semibold uppercase cursor-pointer focus:outline-none">
            <span>S</span>
        </label>
    </div>
</fieldset>
        `;
    }
}