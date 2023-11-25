// Imports
import { UserRegister } from "../models/user-register.model.js";
import { BASE_URL } from "../config.js";
// DOM selectors
const fullNameInput = document.querySelector("#fullName");
const emailInput = document.querySelector("#email");
const usernameInput = document.querySelector("#username");
const passwordInput = document.querySelector("#password");
const registerButton = document.querySelector("#register-btn");
// States and rules

// Function expressions
const register = async (userRegister = new UserRegister()) => {
    const response = await fetch(`${BASE_URL}/auth/register`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json; application/problem+json"
        },
        body: JSON.stringify(userRegister)
    });
    return response;
}

// Event listeners
registerButton.addEventListener("click", async () => {
    const userRegister = new UserRegister(
        fullNameInput.value,
        emailInput.value,
        usernameInput.value,
        passwordInput.value
    );   
    const response = await register(userRegister);
    if (response.ok) {
        alert("Register successfully :)");
    } else {
        const body = await response.json();
        console.log(body);
    }
});
// On load