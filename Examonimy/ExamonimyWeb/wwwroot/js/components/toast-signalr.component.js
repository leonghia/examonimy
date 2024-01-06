import { renderNotiIconMarkup } from "../helpers/markup.helper.js";
import { Notification } from "../models/notification.model.js";

export class ToastSignalRComponent {
    #container;
    #notification;

    constructor(container = new HTMLElement(), notification = new Notification()) {
        this.#container = container;
        this.#notification = notification;
    }  

    connectedCallback() {
        this.#container.insertAdjacentHTML("beforeend", this.#render());
    }

    disconnectedCallback() {
        this.#container.querySelector(".noti-signalr").remove();
    }
    

    #render() {
        return `
<a data-is-read="false" href="${this.#notification.href}" data-notification-id="${this.#notification.id}" class="noti-signalr flex sticky ml-auto right-8 bottom-8 noti bg-white w-80 p-4 dark:hover:bg-gray-700 rounded-md">
    <div>
        <span class="text-sm font-semibold text-gray-700">Thông báo</span>
        <div class="mt-3 flex">
            ${renderNotiIconMarkup(this.#notification)}
            <div class="w-full ps-3">
                ${this.#notification.messageMarkup}
                <div class="text-xs text-blue-600 font-medium flex items-center justify-between"><span>vừa mới đây</span><div class="flex-none rounded-full p-1 text-blue-500 bg-blue-500/10"><div class="h-2 w-2 rounded-full bg-current"></div></div></div>
            </div>
        </div>       
    </div>   
</a>
        `;
    }
}