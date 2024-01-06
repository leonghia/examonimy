import { Notification } from "../models/notification.model.js";
import { putData } from "../helpers/ajax.helper.js";
import { convertToAgo } from "../helpers/datetime.helper.js";
import { renderNotiIconMarkup } from "../helpers/markup.helper.js";

export class NotificationDropdownComponent {
    #container;
    #notifications;
    #isDisplayed = false;

    constructor(container = new HTMLElement(), notification = [new Notification()]) {
        this.#container = container;
        this.#notifications = notification;
    }

    connectedCallback() {
        this.populate();

        this.#container.addEventListener("click", async event => {
            event.preventDefault();
            const clickedNoti = event.target.closest(".noti");
            if (clickedNoti && clickedNoti.dataset.isRead === "false") {
                try {
                    await putData("notification", Number(clickedNoti.dataset.notificationId));                   
                } catch (err) {
                    console.error(err);
                }               
            }
            document.location.href = clickedNoti.href;
        });
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

    insertNoti(notification = new Notification()) {
        if (this.#notifications.length === 5)
            this.#notifications.pop();
        this.#notifications.unshift(notification);
        this.populate();
    }

    populate() {
        this.#container.innerHTML = this.#render();
    }

    toggleDisplay() {
        this.#isDisplayed = !this.#isDisplayed;
    }

    renderNotifications(notifications = [new Notification()]) {
        return notifications.reduce((pV, cV) => {
            return pV + `
        <a data-is-read="${cV.isRead}" href="${cV.href}" data-notification-id="${cV.id}" class="noti flex p-2">
            <div class="flex p-2 hover:bg-gray-100 rounded-md">
                ${renderNotiIconMarkup(cV)}
                <div class="w-full ps-3">
                    ${cV.messageMarkup}
                    ${cV.isRead ? `<div class='text-xs text-gray-500 font-normal'>${convertToAgo(new Date(cV.notifiedAt))}</div>` : `<div class='text-xs text-blue-600 font-medium flex items-center justify-between'><span>${convertToAgo(new Date(cV.notifiedAt))}</span><div class='flex-none rounded-full p-1 text-blue-500 bg-blue-500/10'><div class='h-2 w-2 rounded-full bg-current'></div></div></div>`}
                </div>
            </div>
        </a>
            `;
        }, "")
    }   

    #render() {
        return `
<div id="notification-dropdown" class="${this.#isDisplayed ? "" : "hidden" } absolute right-0 z-20 mt-2 w-96 top-16 origin-top-right bg-white shadow-md divide-y divide-gray-100 rounded-lg" aria-labelledby="dropdownNotificationButton">
    <div class="block px-4 py-2 font-semibold text-center text-sm text-gray-700 rounded-t-lg">
        Thông báo
    </div>
    <div class="divide-y divide-gray-100">
        ${this.#notifications.length ? this.renderNotifications(this.#notifications) : this.renderEmptyState()}
    </div>
    <a href="#" class="${this.#notifications.length ? "" : "hidden"} block py-2 text-sm font-medium text-center text-gray-900 rounded-b-lg">
        <div class="inline-flex items-center ">
            <svg class="w-4 h-4 me-2 text-gray-500" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 14">
                <path d="M10 0C4.612 0 0 5.336 0 7c0 1.742 3.546 7 10 7 6.454 0 10-5.258 10-7 0-1.664-4.612-7-10-7Zm0 10a3 3 0 1 1 0-6 3 3 0 0 1 0 6Z" />
            </svg>
            Xem tất cả
        </div>
    </a>
</div>
        `;
    }


}