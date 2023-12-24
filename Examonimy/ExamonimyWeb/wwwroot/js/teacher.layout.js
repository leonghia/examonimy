// Imports


// DOM selectors
const openUserMenuButton = document.querySelector("#open-user-menu-btn");
const userMenu = document.querySelector("#user-menu");
const viewNotificationButton = document.querySelector("#view-notification-btn");
const notifcationDropdown = document.querySelector("#notification-dropdown");
// States


// Function expressions


// Event listeners
openUserMenuButton.addEventListener("click", () => {
    userMenu.classList.toggle("hidden");
});

viewNotificationButton.addEventListener("click", () => {
    notifcationDropdown.classList.toggle("hidden");
});

// On load