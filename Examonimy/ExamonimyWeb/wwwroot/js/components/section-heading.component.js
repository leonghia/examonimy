export class SectionHeadingComponent {
    #title = "";
    #description = "";

    constructor(title = "", description = "") {
        this.#title = title;
        this.#description = description;
    }

    render() {
        return `
<div class="mb-6">
  <h3 class="text-base font-semibold leading-6 text-gray-900">${this.#title}</h3>
  <p class="mt-2 max-w-4xl text-sm text-gray-500">${this.#description}</p>
</div>
        `;
    }
}