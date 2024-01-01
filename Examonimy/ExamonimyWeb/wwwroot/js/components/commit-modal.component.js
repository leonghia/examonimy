import { hideSpinnerForButtonWithCheckmark, hideSpinnerForButtonWithoutCheckmark, showSpinnerForButton } from "../helpers/markup.helper.js";
import { BaseComponent } from "./base.component.js";

export class CommitModalComponent extends BaseComponent {
    #container;
    #option = {
        title: "",
        ctaText: ""
    }

    _events = {
        cancel: [],
        confirm: [],
        success: []
    }

    #cancelButton;
    #confirmButton;

    constructor(container = new HTMLElement(), option = { title: "", ctaText: "" }) {
        super();
        this.#container = container;
        this.#option = option;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();
        this.#cancelButton = this.#container.querySelector("#cancel-btn");
        this.#confirmButton = this.#container.querySelector("#confirm-btn");

        this.#confirmButton.addEventListener("click", async () => {
            showSpinnerForButton(this.#confirmButton);
            try {
                // get the textarea value
                await this._trigger("confirm");
                hideSpinnerForButtonWithCheckmark(this.#confirmButton);
                setTimeout(() => {
                    this._trigger("success");
                }, 1500);
            } catch (err) {
                console.error(err);
                hideSpinnerForButtonWithoutCheckmark(this.#confirmButton, this.#option.ctaText);
            }
        });

        this.#cancelButton.addEventListener("click", () => {
            this._trigger("cancel");
        });
    }

    disconnectedCallback() {
        this.#container.innerHTML = "";
    }

    #render() {
        return `
<div class="relative z-[999]" aria-labelledby="modal-title" role="dialog" aria-modal="true">
  <!--
    Background backdrop, show/hide based on modal state.

    Entering: "ease-out duration-300"
      From: "opacity-0"
      To: "opacity-100"
    Leaving: "ease-in duration-200"
      From: "opacity-100"
      To: "opacity-0"
  -->
  <div class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity"></div>

  <div class="fixed inset-0 z-10 w-screen overflow-y-auto">
    <div class="flex min-h-full items-end justify-center p-4 text-center sm:items-center sm:p-0">
      <!--
        Modal panel, show/hide based on modal state.

        Entering: "ease-out duration-300"
          From: "opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
          To: "opacity-100 translate-y-0 sm:scale-100"
        Leaving: "ease-in duration-200"
          From: "opacity-100 translate-y-0 sm:scale-100"
          To: "opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
      -->
      <div class="relative transform overflow-hidden rounded-lg bg-white text-left shadow-xl transition-all sm:my-8 sm:w-full sm:max-w-lg">
        <div class="bg-white px-4 pb-4 pt-5 sm:p-6 sm:pb-4">
          <div class="sm:flex sm:items-start">
            <div class="mx-auto flex h-12 w-12 flex-shrink-0 items-center justify-center rounded-full bg-purple-100 sm:mx-0 sm:h-10 sm:w-10">            
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-6 h-6 text-purple-600">
                    <path stroke-linecap="round" stroke-linejoin="round" d="m16.862 4.487 1.687-1.688a1.875 1.875 0 1 1 2.652 2.652L10.582 16.07a4.5 4.5 0 0 1-1.897 1.13L6 18l.8-2.685a4.5 4.5 0 0 1 1.13-1.897l8.932-8.931Zm0 0L19.5 7.125M18 14v4.75A2.25 2.25 0 0 1 15.75 21H5.25A2.25 2.25 0 0 1 3 18.75V8.25A2.25 2.25 0 0 1 5.25 6H10" />
                </svg>
            </div>
            <div class="mt-3 text-center sm:ml-4 sm:mt-0 sm:text-left grow">
              <h3 class="text-base font-semibold leading-6 text-gray-900" id="modal-title">${this.#option.title}</h3>
              <div class="mt-4 mb-2">
                <textarea rows="4" name="commit" id="commit" class="block w-full bg-gray-100 rounded-md border-0 py-1.5 text-gray-700 placeholder:text-gray-400 sm:text-sm sm:leading-6 focus:ring-0" placeholder="Nhập nội dung chỉnh sửa vào đây..."></textarea>
              </div>
            </div>
          </div>
        </div>
        <div class="bg-gray-50 px-4 py-3 sm:flex sm:flex-row-reverse sm:px-6">
          <button type="button" id="confirm-btn" class="inline-flex w-24 justify-center rounded-md bg-purple-300 px-3 py-2 text-sm font-semibold text-purple-800 shadow-sm hover:bg-purple-400 hover:text-purple-900 sm:ml-3 sm:w-auto"><span id="button-text">${this.#option.ctaText}</span></button>
          <button type="button" id="cancel-btn" class="mt-3 inline-flex w-full justify-center rounded-md bg-gray-200 px-4 py-2 text-sm font-semibold text-gray-800 hover:bg-gray-300 hover:text-gray-900 sm:mt-0 sm:w-auto">Hủy</button>
        </div>
      </div>
    </div>
  </div>
</div>
        `;
    }
}