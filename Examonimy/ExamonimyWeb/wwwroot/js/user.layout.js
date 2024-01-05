// Imports
import { NotificationDropdownComponent } from "./components/notification-dropdown.component.js";
import { ToastSignalRComponent } from "./components/toast-signalr.component.js";
import { fetchData } from "./helpers/ajax.helper.js";
import { Notification } from "./models/notification.model.js";
import { RequestParams } from "./models/request-params.model.js";

// DOM selectors
const openUserMenuButton = document.querySelector("#open-user-menu-btn");
const userMenu = document.querySelector("#user-menu");
const viewNotificationButton = document.querySelector("#view-notification-btn");
const notifcationDropdownContainer = document.querySelector("#notification-dropdown-container");
const notiDot = document.querySelector("#noti-dot");
const pageContainer = document.querySelector("#page-container");
const currentPage = pageContainer.dataset.page;



// States
const currentPageLink = document.querySelector(`.page-link[data-page="${currentPage}"]`);
let notificationDropdownComponent;
let toastSignalRComponent;
const notiHubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();
let isNotificationShown = false;

// Function expressions
const startNotiHubConnection = async () => {
    try {
        await notiHubConnection.start();
        console.log("notificationHub connected :)");
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
    notificationDropdownComponent.toggleDisplay();
    notificationDropdownComponent.populate();    
    notiDot.classList.add("hidden");
});

notiHubConnection.onclose(async () => {
    await startNotiHubConnection();
});

notiHubConnection.on("ReceiveNotification", (notification = Notification()) => {
    toastSignalRComponent = new ToastSignalRComponent(document.body, notification);
    toastSignalRComponent.connectedCallback();

    notificationDropdownComponent.insertNoti(notification);
    notiDot.classList.remove("hidden");
    setTimeout(() => {
        toastSignalRComponent.disconnectedCallback();
        toastSignalRComponent = undefined;
    }, 10000);
});

// On load
currentPageLink?.classList.remove(..."text-gray-300 hover:bg-gray-700 hover:text-white".split(" "));
currentPageLink?.classList.add(..."bg-gray-900 text-white".split(" "));
startNotiHubConnection();
init();
