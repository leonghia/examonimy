import { BaseComponent } from "./base.component.js";
import { hideSpinnerForButton, showSpinnerForButton } from "../helpers/markup.helper.js";
import { SpinnerOption } from "../models/spinner-option.model.js";

export class ConfirmModalComponent extends BaseComponent {
    #container;
    #option = {
        title: "",
        description: "",
        ctaText: "",
        href: ""
    }

    _events = {
        cancel: [],
        confirm: []
    }

    #cancelButton;
    #confirmButton;

    constructor(container = new HTMLElement(), option = { title: "", description: "", ctaText: "", href: "" }) {
        super();
        this.#container = container;
        this.#option = option;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();
        this.#cancelButton = this.#container.querySelector("#cancel-btn");
        this.#confirmButton = this.#container.querySelector("#confirm-btn");

        this.#confirmButton.addEventListener("click", async () => {
            const spinnerOption = new SpinnerOption();
            spinnerOption.fill = "fill-red-600";
            showSpinnerForButton(this.#confirmButton.querySelector("#button-text"), this.#confirmButton, spinnerOption);
            await this._trigger("confirm");
            hideSpinnerForButton(this.#confirmButton, this.#confirmButton.querySelector("#button-text"), spinnerOption);
        });

        this.#cancelButton.addEventListener("click", () => {
            this.#container.innerHTML = "";
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
            <div class="mx-auto flex h-12 w-12 flex-shrink-0 items-center justify-center rounded-full bg-red-100 sm:mx-0 sm:h-10 sm:w-10">
              <svg class="h-6 w-6 text-red-600" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" aria-hidden="true">
                <path stroke-linecap="round" stroke-linejoin="round" d="M12 9v3.75m-9.303 3.376c-.866 1.5.217 3.374 1.948 3.374h14.71c1.73 0 2.813-1.874 1.948-3.374L13.949 3.378c-.866-1.5-3.032-1.5-3.898 0L2.697 16.126zM12 15.75h.007v.008H12v-.008z" />
              </svg>
            </div>
            <div class="mt-3 text-center sm:ml-4 sm:mt-0 sm:text-left">
              <h3 class="text-base font-semibold leading-6 text-gray-900" id="modal-title">${this.#option.title}</h3>
              <div class="mt-2">
                <p class="text-sm text-gray-500">${this.#option.description}</p>
              </div>
            </div>
          </div>
        </div>
        <div class="bg-gray-50 px-4 py-3 sm:flex sm:flex-row-reverse sm:px-6">
          <button type="button" id="confirm-btn" class="inline-flex w-full justify-center rounded-md bg-red-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-red-500 sm:ml-3 sm:w-auto"><span id="button-text">${this.#option.ctaText}</span></button>
          <button type="button" id="cancel-btn" class="mt-3 inline-flex w-full justify-center rounded-md bg-white px-3 py-2 text-sm font-semibold text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 hover:bg-gray-50 sm:mt-0 sm:w-auto">Hủy</button>
        </div>
      </div>
    </div>
  </div>
</div>

        `;
    }
}