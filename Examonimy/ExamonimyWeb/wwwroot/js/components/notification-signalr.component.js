import { Notification } from "../models/notification.model.js";

export class NotificationSignalRComponent {
    #container;
    #notification;

    constructor(container = new HTMLElement(), notification = new Notification()) {
        this.#container = container;
        this.#notification = notification;
    }

    connectedCallback() {
        this.#container.innerHTML = this.#render();
    }

    disconnectedCallback() {
        this.#container.innerHTML = "";
    }

    #render() {
        return `
<a data-is-read="false" href="${this.#notification.href}" data-notification-id="${this.#notification.id}" class="absolute right-8 bottom-8 noti bg-white w-80 flex p-2 dark:hover:bg-gray-700 rounded-md">
    <div class="flex p-2">
        <div class="flex-shrink-0">
            <img class="rounded-full w-11 h-11" src="${this.#notification.actorProfilePicture}" alt="user profile picture">
            ${this.#notification.iconMarkup}
        </div>
        <div class="w-full ps-3">
            ${this.#notification.messageMarkup}
            <div class="text-xs text-blue-600 font-medium flex items-center justify-between"><span>vừa mới đây</span><div class="flex-none rounded-full p-1 text-blue-500 bg-blue-500/10"><div class="h-2 w-2 rounded-full bg-current"></div></div></div>
        </div>
    </div>
</a>
        `;
    }
}