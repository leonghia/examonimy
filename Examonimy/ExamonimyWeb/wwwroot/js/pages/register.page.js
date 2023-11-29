// Imports
import { UserRegister } from "../models/user-register.model.js";
import { BASE_API_URL } from "../config.js";
import { showSpinnerForButton, spinnerMarkupForButton, hideSpinnerForButton, showErrorMessagesForInputsFromResponse, hideErrorMessageWhenInput } from "../helpers/markup.helper.js";
// DOM selectors
const registerForm = document.querySelector("#register-form");
const fullNameInput = document.querySelector("#fullName");
const emailInput = document.querySelector("#email");
const usernameInput = document.querySelector("#username");
const passwordInput = document.querySelector("#password");
const registerButton = document.querySelector("#register-btn");
const registerButtonText = document.querySelector("#register-btn-text");
// States and rules

// Function expressions
const register = async (userRegister = new UserRegister()) => {
    const response = await fetch(`${BASE_API_URL}/auth/register`, {
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
    showSpinnerForButton(registerButtonText, registerButton, spinnerMarkupForButton);
    const userRegister = new UserRegister(
        fullNameInput.value,
        emailInput.value,
        usernameInput.value,
        passwordInput.value
    );   
    const response = await register(userRegister);
    hideSpinnerForButton(registerButton, registerButtonText);
    if (!response.ok) {
        const responseBody = await response.json();
        showErrorMessagesForInputsFromResponse(responseBody);
    }
});

registerForm.addEventListener("click", hideErrorMessageWhenInput);

// On load
(() => {
    document.documentElement.classList.remove("bg-gray-100");
    document.documentElement.classList.add("bg-white");   
})();