// Imports
import { NotificationDropdownComponent } from "./components/notification-dropdown.component.js";
import { NotificationSignalRComponent } from "./components/notification-signalr.component.js";
import { fetchData } from "./helpers/ajax.helper.js";
import { Notification } from "./models/notification.model.js";
import { RequestParams } from "./models/request-params.model.js";

// DOM selectors
const openUserMenuButton = document.querySelector("#open-user-menu-btn");
const userMenu = document.querySelector("#user-menu");
const viewNotificationButton = document.querySelector("#view-notification-btn");
const notifcationDropdownContainer = document.querySelector("#notification-dropdown-container");
const notiDot = document.querySelector("#noti-dot");

// States
let notificationDropdownComponent;
let notificationSignalRComponent;
const signalRConnection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

// Function expressions
const startSignalR = async () => {
    try {
        await signalRConnection.start();
        console.log("SignalR connected :)");
    } catch (err) {
        console.error(err);
    }
}

const toggleNotiDot = (notifications = [new Notification]) => {
    if (notifications.some(n => !n.isRead))
        notiDot.classList.remove("hidden");
    else
        notiDot.classList.add("hidden");
}

const init = async () => {
    const res = await fetchData("notification", new RequestParams(null, 5, 1));
    const notifications = res.data;
    notificationDropdownComponent = new NotificationDropdownComponent(notifcationDropdownContainer, notifications);
    notificationDropdownComponent.connectedCallback();
    toggleNotiDot(notifications);
}

// Event listeners
openUserMenuButton.addEventListener("click", () => {
    userMenu.classList.toggle("hidden");
});

viewNotificationButton.addEventListener("click", async () => {
    notifcationDropdownContainer.classList.toggle("hidden");
    notificationDropdownComponent.populate();
    notiDot.classList.add("hidden");
});

// On load
init();

signalRConnection.onclose(async () => {
    await startSignalR();
});

signalRConnection.on("ReceiveNotification", (notification = Notification()) => {
    notificationSignalRComponent = new NotificationSignalRComponent(document.querySelector("#notification-signalr-container"), notification);
    notificationSignalRComponent.connectedCallback();
    notificationDropdownComponent.insertNoti(notification);
    notiDot.classList.remove("hidden");
    setTimeout(() => {
        notificationSignalRComponent.disconnectedCallback();
        notificationSignalRComponent = undefined;
    }, 10000);
});

startSignalR();
