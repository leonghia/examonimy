// Imports
import { BASE_API_URL } from "../config.js";
import { hideErrorMessageWhenInput, hideSpinnerForButton, showErrorMessagesForInputsFromResponse, showSpinnerForButton } from "../helpers/markup.helper.js";
import { UserLogin } from "../models/user-login.model.js";
import { StatusCodes } from "../helpers/status-code.helper.js";
import { ProblemDetails } from "../models/problem-details.model.js";
import { SpinnerOption } from "../models/spinner-option.model.js";
// DOM selectors
const htmlElement = document.documentElement;
const loginForm = document.querySelector("#login-form");
const emailInput = document.querySelector("#email");
const passwordInput = document.querySelector("#password");
const loginButton = document.querySelector("#login-btn");
const loginButtonText = document.querySelector("#login-btn-text");
const rememberMeCheckbox = document.querySelector("#remember-me");
// States and rules

// Function expressions

// Event listeners
loginButton.addEventListener("click", async () => {
    showSpinnerForButton(loginButtonText, loginButton, new SpinnerOption());
    const userLogin = new UserLogin(
        emailInput.value,
        passwordInput.value,
        rememberMeCheckbox.checked
    );
    const response = await fetch(`${BASE_API_URL}/auth/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json; application/problem+json"
        },
        body: JSON.stringify(userLogin)
    });

    hideSpinnerForButton(loginButton, loginButtonText);

    if (response.status === StatusCodes.Status422UnprocessableEntity) {
        const responseBody = await response.json();
        showErrorMessagesForInputsFromResponse(responseBody);     
    } else if (response.status === StatusCodes.Status401Unauthorized) {
        const responseBody = await response.json();     
        const problemDetails = new ProblemDetails();
        Object.assign(problemDetails, responseBody);
        loginForm.parentElement.insertAdjacentHTML("afterbegin", `
<div id="invalid-credentials-error-message" class="flex items-center p-4 mb-4 text-sm rounded-lg bg-red-50 dark:bg-gray-800 dark:text-red-400" role="alert">
  <svg class="flex-shrink-0 inline w-4 h-4 me-3 text-red-400" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 20">
    <path d="M10 .5a9.5 9.5 0 1 0 9.5 9.5A9.51 9.51 0 0 0 10 .5ZM9.5 4a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3ZM12 15H8a1 1 0 0 1 0-2h1v-3H8a1 1 0 0 1 0-2h2a1 1 0 0 1 1 1v4h1a1 1 0 0 1 0 2Z"/>
  </svg>
  <span class="sr-only">Info</span>
  <div>
    <span class="font-medium text-red-700">${Object.entries(problemDetails.errors)[0][1]}</span>
  </div>
</div>
        `);
    }  

    if (response.redirected)
        document.location.href = response.url;
});

loginForm.addEventListener("click", event => {
    document.querySelector("#invalid-credentials-error-message")?.remove();
    hideErrorMessageWhenInput(event);
});

// Onload