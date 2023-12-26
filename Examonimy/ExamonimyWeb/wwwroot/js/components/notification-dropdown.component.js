import { Notification } from "../models/notification.model.js";

export class NotificationDropdownComponent {
    #container;
    #notifications;

    constructor(container = new HTMLElement(), notification = [new Notification()]) {
        this.#container = container;
        this.#notifications = notification;
    }

    connectedCallback() {
        this.#container.innerHTML = this.render();
    }

    renderEmptyState() {
        return `
        <div class="text-center py-12">        
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" class="mx-auto h-12 w-12 text-gray-400">
            <path stroke-linecap="round" stroke-linejoin="round" d="M3.75 9.776c.112-.017.227-.026.344-.026h15.812c.117 0 .232.009.344.026m-16.5 0a2.25 2.25 0 0 0-1.883 2.542l.857 6a2.25 2.25 0 0 0 2.227 1.932H19.05a2.25 2.25 0 0 0 2.227-1.932l.857-6a2.25 2.25 0 0 0-1.883-2.542m-16.5 0V6A2.25 2.25 0 0 1 6 3.75h3.879a1.5 1.5 0 0 1 1.06.44l2.122 2.12a1.5 1.5 0 0 0 1.06.44H18A2.25 2.25 0 0 1 20.25 9v.776" />
          </svg>
          <h3 class="mt-2 text-sm font-semibold text-gray-900">Không có thông báo nào</h3>               
        </div>
        `;
    }

    renderNotifications(notifications = [new Notification()]) {
        return notifications.reduce((pV, cV) => {
            return pV + `
        <a href="${cV.href}" class="flex p-2 dark:hover:bg-gray-700">
            <div class="flex p-2 hover:bg-gray-50 rounded-md">
                <div class="flex-shrink-0">
                    <img class="rounded-full w-11 h-11" src="${cV.actorProfilePicture}" alt="user profile picture">
                    ${cV.iconMarkup}
                </div>
                <div class="w-full ps-3">
                    <div class="text-gray-500 text-sm mb-1.5 dark:text-gray-400">
                        ${cV.messageMarkup}
                    </div>
                    <div class="text-xs text-blue-600 dark:text-blue-500">${cV.dateTimeAgoMarkup}</div>
                </div>
            </div>
        </a>
            `;
        }, "")
    }

    render() {
        return `
<div id="notification-dropdown" class="absolute right-0 z-20 mt-2 w-96 top-16 origin-top-right bg-white shadow-md divide-y divide-gray-100 rounded-lg dark:bg-gray-800 dark:divide-gray-700" aria-labelledby="dropdownNotificationButton">
    <div class="block px-4 py-2 font-semibold text-center text-sm text-gray-700 rounded-t-lg dark:bg-gray-800 dark:text-white">
        Thông báo
    </div>
    <div class="divide-y divide-gray-100 dark:divide-gray-700">
        ${this.#notifications.length ? this.renderNotifications(this.#notifications) : this.renderEmptyState()}
    </div>
    <a href="#" class="${this.#notifications.length ? "" : "hidden"} block py-2 text-sm font-medium text-center text-gray-900 rounded-b-lg dark:bg-gray-800 dark:hover:bg-gray-700 dark:text-white">
        <div class="inline-flex items-center ">
            <svg class="w-4 h-4 me-2 text-gray-500 dark:text-gray-400" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 14">
                <path d="M10 0C4.612 0 0 5.336 0 7c0 1.742 3.546 7 10 7 6.454 0 10-5.258 10-7 0-1.664-4.612-7-10-7Zm0 10a3 3 0 1 1 0-6 3 3 0 0 1 0 6Z" />
            </svg>
            Xem tất cả
        </div>
    </a>
</div>
        `;
    }


}