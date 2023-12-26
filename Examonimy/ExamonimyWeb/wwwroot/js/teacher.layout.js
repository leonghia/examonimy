// Imports
import { NotificationDropdownComponent } from "./components/notification-dropdown.component.js";
import { fetchData } from "./helpers/ajax.helper.js";
import { RequestParams } from "./models/request-params.model.js";

// DOM selectors
const openUserMenuButton = document.querySelector("#open-user-menu-btn");
const userMenu = document.querySelector("#user-menu");
const viewNotificationButton = document.querySelector("#view-notification-btn");
const notifcationDropdownContainer = document.querySelector("#notification-dropdown-container");

// States
let notificationDropdownComponent;

// Function expressions


// Event listeners
openUserMenuButton.addEventListener("click", () => {
    userMenu.classList.toggle("hidden");
});

viewNotificationButton.addEventListener("click", () => {
    notifcationDropdownContainer.classList.toggle("hidden");
});

// On load
(async () => {
    const res = await fetchData("notification", new RequestParams(null, 5, 1));
    const notifications = res.data;
    notificationDropdownComponent = new NotificationDropdownComponent(notifcationDropdownContainer, notifications);
    notificationDropdownComponent.connectedCallback();
})();