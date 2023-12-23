import { User } from "../models/user.model.js";
import { BaseComponent } from "./base.component.js";


export class TeacherStackedListComponent extends BaseComponent {
    #container;
    _teachers;
    #closeButton;
    _events = {
        close: [],
        confirm: []
    }

    constructor(container = new HTMLElement(), teachers = [new User()]) {
        super();
        this.#container = container;
        this._teachers = teachers;
    }

    connectedCallback() {
        this.#container.innerHTML = this.render();
        this.#closeButton = this.#container.querySelector("#close-btn");

        this.#closeButton.addEventListener("click", () => {
            this.#container.innerHTML = "";
            this._trigger("close");
        });
    }

    

    render() {
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
        <div class="flex min-h-full items-end justify-center p-4 sm:items-center sm:p-0">
            <!--
              Modal panel, show/hide based on modal state.

              Entering: "ease-out duration-300"
                From: "opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
                To: "opacity-100 translate-y-0 sm:scale-100"
              Leaving: "ease-in duration-200"
                From: "opacity-100 translate-y-0 sm:scale-100"
                To: "opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
            -->
            <div class="w-[30rem] bg-white rounded-lg">
                <div class="w-full p-4 flex items-center justify-between">
                    <div class="space-y-2">
                        <h6 class="text-gray-700 font-semibold text-sm">Thêm kiểm duyệt viên</h6>
                        <p class="text-gray-500 text-xs italic">*Bạn có thể thêm tối đa 3 kiểm duyệt viên</p>
                    </div>                  
                    <button type="button" title="Đóng" id="close-btn" class="p-2 bg-gray-200 text-gray-800 hover:bg-gray-300 hover:text-gray-900 rounded">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="w-4 h-4">
                            <path fill-rule="evenodd" d="M5.47 5.47a.75.75 0 0 1 1.06 0L12 10.94l5.47-5.47a.75.75 0 1 1 1.06 1.06L13.06 12l5.47 5.47a.75.75 0 1 1-1.06 1.06L12 13.06l-5.47 5.47a.75.75 0 0 1-1.06-1.06L10.94 12 5.47 6.53a.75.75 0 0 1 0-1.06Z" clip-rule="evenodd" />
                        </svg>
                    </button>
                </div>
                <nav class="max-h-96 w-full overflow-y-auto">
                    <div class="relative">
                        <div class="sticky top-0 z-10 border-y border-b-gray-200 border-t-gray-100 bg-gray-50 px-3 py-1.5 text-sm font-semibold leading-6 text-gray-900">
                            <h3>A</h3>
                        </div>
                        <ul role="list" class="divide-y divide-gray-100">
                            <li class="flex items-center justify-between p-4">
                                <div class="flex gap-x-4">
                                    <img class="h-12 w-12 flex-none rounded-full bg-gray-50" src="https://images.unsplash.com/photo-1494790108377-be9c29b29330?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80" alt="">
                                    <div class="min-w-0">
                                        <p class="text-sm font-semibold leading-6 text-gray-900">Leslie Abbott</p>
                                        <p class="mt-1 truncate text-xs leading-5 text-gray-500">leslie.abbott@example.com</p>
                                    </div>
                                </div>
                                <input id="default-checkbox" type="checkbox" value="" class="w-5 h-5 text-teal-500 bg-gray-300 border-none rounded focus:ring-0 focus:ring-offset-0">
                            </li>     
                            <li class="flex items-center justify-between p-4">
                                <div class="flex gap-x-4">
                                    <img class="h-12 w-12 flex-none rounded-full bg-gray-50" src="https://images.unsplash.com/photo-1494790108377-be9c29b29330?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80" alt="">
                                    <div class="min-w-0">
                                        <p class="text-sm font-semibold leading-6 text-gray-900">Leslie Abbott</p>
                                        <p class="mt-1 truncate text-xs leading-5 text-gray-500">leslie.abbott@example.com</p>
                                    </div>
                                </div>
                                <input id="default-checkbox" type="checkbox" value="" class="w-5 h-5 text-teal-500 bg-gray-300 border-none rounded focus:ring-0 focus:ring-offset-0">
                            </li>     
                            <li class="flex items-center justify-between p-4">
                                <div class="flex gap-x-4">
                                    <img class="h-12 w-12 flex-none rounded-full bg-gray-50" src="https://images.unsplash.com/photo-1494790108377-be9c29b29330?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80" alt="">
                                    <div class="min-w-0">
                                        <p class="text-sm font-semibold leading-6 text-gray-900">Leslie Abbott</p>
                                        <p class="mt-1 truncate text-xs leading-5 text-gray-500">leslie.abbott@example.com</p>
                                    </div>
                                </div>
                                <input id="default-checkbox" type="checkbox" value="" class="w-5 h-5 text-teal-500 bg-gray-300 border-none rounded focus:ring-0 focus:ring-offset-0">
                            </li>
                        </ul>
                    </div>                                  
                </nav>
                <div class="w-full p-4 flex items-center justify-end">
                    <button type="button" class="text-sm font-semibold bg-violet-300 hover:bg-violet-400 text-violet-800 hover:text-violet-900 rounded-md py-2 px-4">Xác nhận</button>
                </div>
            </div>
            
        </div>
    </div>
</div>
        `;
    }
}