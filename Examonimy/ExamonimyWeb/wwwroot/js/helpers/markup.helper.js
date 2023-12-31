﻿import { MAX_LENGTH_FOR_QUESTION_CONTENT } from "../config.js";
import { Notification } from "../models/notification.model.js";
import { ProblemDetails } from "../models/problem-details.model.js";
import { SpinnerOption } from "../models/spinner-option.model.js";
import { Operation } from "./operation.helper.js";

export const getErrorMessageMarkup = (errorMessage = "") => {
    return `<p class="error-message mt-2 text-xs text-red-600 dark:text-red-400"><span class="font-medium">${errorMessage}</span></p>`;
}

export const showSpinnerForButton = (button = new HTMLButtonElement(), spinnerOption = new SpinnerOption()) => {   
    button.classList.remove(spinnerOption.fill);
    button.classList.add(..."bg-gray-300 pointer-events-none".split(" "));
    button.innerHTML = `
    <svg aria-hidden="true" id="spinner" class="inline ${spinnerOption.width} ${spinnerOption.height} text-white animate-spin ${spinnerOption.fill}" viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z" fill="currentColor"/>
        <path d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z" fill="currentFill"/>
    </svg>
    `;
}

export const hideSpinnerForButtonWithCheckmark = (button = new HTMLButtonElement(), spinnerOption = new SpinnerOption()) => {
    button.classList.remove("bg-gray-300");
    button.classList.add(spinnerOption.fill);
    button.classList.add("pointer-events-none");
    button.innerHTML = `
<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="${spinnerOption.width} ${spinnerOption.height} text-violet-800">
  <path stroke-linecap="round" stroke-linejoin="round" d="m4.5 12.75 6 6 9-13.5" />
</svg>
    `;
}

export const hideSpinnerForButtonWithoutCheckmark = (button = new HTMLButtonElement(), textContent = "", spinnerOption = new SpinnerOption()) => {
    button.classList.remove(..."bg-gray-300 pointer-events-none".split(" "));
    button.classList.add(spinnerOption.fill);
    button.innerHTML = "";
    button.textContent = textContent;
}

export const showErrorMessagesForInputsFromResponse = (responseBody = {}) => {
    const errorMessageElements = Array.from(document.querySelectorAll(".error-message"));
    errorMessageElements.forEach(e => e.remove());
    let problemDetails = new ProblemDetails();
    Object.assign(problemDetails, responseBody);
    const errors = Object.entries(problemDetails.errors);
    for (const error of errors) {
        const errorName = error[0];
        const errorMessage = error[1][0];
        const input = document.querySelector(`#${errorName}`);
        input.parentElement.parentElement.querySelector(".error-message")?.remove();
        input.parentElement.parentElement.insertAdjacentHTML("beforeend", getErrorMessageMarkup(errorMessage));
    }
}

export const hideErrorMessageWhenInput = (event = new Event()) => {
    const clicked = event.target.closest("input");
    if (!clicked)
        return;
    const errorMessageElement = clicked.parentElement.parentElement.querySelector(".error-message");
    if (!errorMessageElement)
        return;
    errorMessageElement.remove();
}

export const highlightSidebarLink = (className = "") => {
    const sidebarLinkElements = Array.from(document.querySelectorAll(className));
    sidebarLinkElements.forEach(e => {
        e.classList.remove(..."text-gray-400 hover:text-white hover:bg-gray-800".split(" "));
        e.classList.add(..."bg-gray-800 text-white".split(" "));
    });  
}

export const toggleDropdown = (dropdown = new HTMLElement()) => {
    dropdown.classList.toggle("hidden");   
}

export const selectDropdownItem = (event) => {
    const clicked = (event.target.closest(".dropdown-item"));
    if (!clicked)
        return;
    const dropdownItemName = clicked.querySelector(".dropdown-item-name");
    const dropdownContainer = clicked.closest(".dropdown-container");
    dropdownContainer.querySelector(".selected-item").textContent = dropdownItemName.textContent;
    const dropdown = clicked.closest(".dropdown");
    if (!dropdown?.querySelectorAll(".dropdown-item"))
        return;
    const dropdownItems = Array.from(dropdown.querySelectorAll(".dropdown-item"));
    dropdownItems.forEach(dropdownItem => {
        const itemName = dropdownItem.querySelector(".dropdown-item-name");
        itemName.classList.remove("font-semibold");
        itemName.classList.add("font-normal");
        const itemCheckmark = dropdownItem.querySelector(".dropdown-item-checkmark");
        itemCheckmark.classList.remove("text-violet-600");
        itemCheckmark.classList.add("text-white");
    });
    dropdownItemName.classList.remove("font-normal");
    dropdownItemName.classList.add("font-semibold");
    const dropdownItemCheckmark = clicked.querySelector(".dropdown-item-checkmark");
    dropdownItemCheckmark.classList.remove("text-white");
    dropdownItemCheckmark.classList.add("text-violet-600");
    toggleDropdown(dropdown);
}

export const changeHtmlBackgroundColorToWhite = () => {
    document.documentElement.classList.remove("bg-gray-100");
    document.documentElement.classList.add("bg-white");
}

export const changeHtmlBackgroundColorToGray = () => {
    document.documentElement.classList.remove("bg-white");
    document.documentElement.classList.add("bg-gray-100");
    document.querySelector("#navbar").classList.remove("bg-white");
    document.querySelector("#navbar").classList.add("bg-gray-100");
}

export const toggleSegment = (segments = [new HTMLElement()], segmentOrder = 0) => {
    segments.forEach(segment => {
        if (Number(segment.dataset.order) === segmentOrder)
            segment.classList.remove("hidden");
        else
            segment.classList.add("hidden");
    });
}

export const trimMarkup = (markup = "") => {
    const endTagPosition = markup.indexOf("</");

    if (endTagPosition > MAX_LENGTH_FOR_QUESTION_CONTENT) {
        const openingTag = markup.substring(0, markup.indexOf(">") + 1);
        return markup.substring(0, MAX_LENGTH_FOR_QUESTION_CONTENT).concat("......").concat(openingTag.replace(">", "/>"));
    }

    const temp = markup.substring(0, markup.indexOf(">") + 1).replace("<", "</");
    return markup.substring(0, markup.indexOf(temp) + temp.length);
}

export const renderNotiIconMarkup = (notification = new Notification()) => {
    switch (notification.operation) {
        case Operation.ApproveExamPaper:
            return `
<div class="flex-shrink-0">
    <img class="rounded-full w-11 h-11" src="${notification.actorProfilePicture}" alt="user profile picture">
    <div class="absolute flex items-center justify-center w-5 h-5 ms-6 -mt-5 bg-purple-500 border border-white rounded-full"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="w-2 h-2 text-white"><path fill-rule="evenodd" d="M10 18a8 8 0 1 0 0-16 8 8 0 0 0 0 16Zm3.857-9.809a.75.75 0 0 0-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 1 0-1.06 1.061l2.5 2.5a.75.75 0 0 0 1.137-.089l4-5.5Z" clip-rule="evenodd" /></svg></div>
</div>
            `;
        case Operation.AskForReviewForExamPaper:
            return `
<div class="flex-shrink-0">
    <img class="rounded-full w-11 h-11" src="${notification.actorProfilePicture}" alt="user profile picture">
    <div class="absolute flex items-center justify-center w-5 h-5 ms-6 -mt-5 bg-yellow-600 border border-white rounded-full"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="w-2 h-2 text-white"><path d="M5.625 1.5c-1.036 0-1.875.84-1.875 1.875v17.25c0 1.035.84 1.875 1.875 1.875h12.75c1.035 0 1.875-.84 1.875-1.875V12.75A3.75 3.75 0 0 0 16.5 9h-1.875a1.875 1.875 0 0 1-1.875-1.875V5.25A3.75 3.75 0 0 0 9 1.5H5.625Z" /><path d="M12.971 1.816A5.23 5.23 0 0 1 14.25 5.25v1.875c0 .207.168.375.375.375H16.5a5.23 5.23 0 0 1 3.434 1.279 9.768 9.768 0 0 0-6.963-6.963Z" /></svg></div>
</div>
            `;
        case Operation.CommentExamPaper:
            return `
<div class="flex-shrink-0">
    <img class="rounded-full w-11 h-11" src="${notification.actorProfilePicture}" alt="user profile picture">
    <div class="absolute flex items-center justify-center w-5 h-5 ms-6 -mt-5 bg-green-500 border border-white rounded-full"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="w-2 h-2 text-white"><path fill-rule="evenodd" d="M3.43 2.524A41.29 41.29 0 0 1 10 2c2.236 0 4.43.18 6.57.524 1.437.231 2.43 1.49 2.43 2.902v5.148c0 1.413-.993 2.67-2.43 2.902a41.102 41.102 0 0 1-3.55.414c-.28.02-.521.18-.643.413l-1.712 3.293a.75.75 0 0 1-1.33 0l-1.713-3.293a.783.783 0 0 0-.642-.413 41.108 41.108 0 0 1-3.55-.414C1.993 13.245 1 11.986 1 10.574V5.426c0-1.413.993-2.67 2.43-2.902Z" clip-rule="evenodd" /></svg></div>
</div>            
            `;
        case Operation.EditExamPaper:
            return `
<div class="flex-shrink-0">
    <img class="rounded-full w-11 h-11" src="${notification.actorProfilePicture}" alt="user profile picture">
    <div class="absolute flex items-center justify-center w-5 h-5 ms-6 -mt-5 bg-blue-500 border border-white rounded-full"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="w-2 h-2 text-white"><path d="m5.433 13.917 1.262-3.155A4 4 0 0 1 7.58 9.42l6.92-6.918a2.121 2.121 0 0 1 3 3l-6.92 6.918c-.383.383-.84.685-1.343.886l-3.154 1.262a.5.5 0 0 1-.65-.65Z" /><path d="M3.5 5.75c0-.69.56-1.25 1.25-1.25H10A.75.75 0 0 0 10 3H4.75A2.75 2.75 0 0 0 2 5.75v9.5A2.75 2.75 0 0 0 4.75 18h9.5A2.75 2.75 0 0 0 17 15.25V10a.75.75 0 0 0-1.5 0v5.25c0 .69-.56 1.25-1.25 1.25h-9.5c-.69 0-1.25-.56-1.25-1.25v-9.5Z" /></svg></div>
</div>  
            `;
        case Operation.RejectExamPaper:
            return `
<div class="flex-shrink-0">
    <img class="rounded-full w-11 h-11" src="${notification.actorProfilePicture}" alt="user profile picture">
    <div class="absolute flex items-center justify-center w-5 h-5 ms-6 -mt-5 bg-red-500 border border-white rounded-full"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="w-3 h-3 text-white"><path d="M6.28 5.22a.75.75 0 0 0-1.06 1.06L8.94 10l-3.72 3.72a.75.75 0 1 0 1.06 1.06L10 11.06l3.72 3.72a.75.75 0 1 0 1.06-1.06L11.06 10l3.72-3.72a.75.75 0 0 0-1.06-1.06L10 8.94 6.28 5.22Z" /></svg></div>
</div> 
            `;
        case Operation.UpcomingExam:
            return `
<div class="inline-flex items-center justify-center flex-shrink-0 w-10 h-10 text-amber-500 bg-amber-100 rounded-lg">
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="w-5 h-5">
        <path d="M5.25 12a.75.75 0 0 1 .75-.75h.01a.75.75 0 0 1 .75.75v.01a.75.75 0 0 1-.75.75H6a.75.75 0 0 1-.75-.75V12ZM6 13.25a.75.75 0 0 0-.75.75v.01c0 .414.336.75.75.75h.01a.75.75 0 0 0 .75-.75V14a.75.75 0 0 0-.75-.75H6ZM7.25 12a.75.75 0 0 1 .75-.75h.01a.75.75 0 0 1 .75.75v.01a.75.75 0 0 1-.75.75H8a.75.75 0 0 1-.75-.75V12ZM8 13.25a.75.75 0 0 0-.75.75v.01c0 .414.336.75.75.75h.01a.75.75 0 0 0 .75-.75V14a.75.75 0 0 0-.75-.75H8ZM9.25 10a.75.75 0 0 1 .75-.75h.01a.75.75 0 0 1 .75.75v.01a.75.75 0 0 1-.75.75H10a.75.75 0 0 1-.75-.75V10ZM10 11.25a.75.75 0 0 0-.75.75v.01c0 .414.336.75.75.75h.01a.75.75 0 0 0 .75-.75V12a.75.75 0 0 0-.75-.75H10ZM9.25 14a.75.75 0 0 1 .75-.75h.01a.75.75 0 0 1 .75.75v.01a.75.75 0 0 1-.75.75H10a.75.75 0 0 1-.75-.75V14ZM12 9.25a.75.75 0 0 0-.75.75v.01c0 .414.336.75.75.75h.01a.75.75 0 0 0 .75-.75V10a.75.75 0 0 0-.75-.75H12ZM11.25 12a.75.75 0 0 1 .75-.75h.01a.75.75 0 0 1 .75.75v.01a.75.75 0 0 1-.75.75H12a.75.75 0 0 1-.75-.75V12ZM12 13.25a.75.75 0 0 0-.75.75v.01c0 .414.336.75.75.75h.01a.75.75 0 0 0 .75-.75V14a.75.75 0 0 0-.75-.75H12ZM13.25 10a.75.75 0 0 1 .75-.75h.01a.75.75 0 0 1 .75.75v.01a.75.75 0 0 1-.75.75H14a.75.75 0 0 1-.75-.75V10ZM14 11.25a.75.75 0 0 0-.75.75v.01c0 .414.336.75.75.75h.01a.75.75 0 0 0 .75-.75V12a.75.75 0 0 0-.75-.75H14Z"></path>
        <path fill-rule="evenodd" d="M5.75 2a.75.75 0 0 1 .75.75V4h7V2.75a.75.75 0 0 1 1.5 0V4h.25A2.75 2.75 0 0 1 18 6.75v8.5A2.75 2.75 0 0 1 15.25 18H4.75A2.75 2.75 0 0 1 2 15.25v-8.5A2.75 2.75 0 0 1 4.75 4H5V2.75A.75.75 0 0 1 5.75 2Zm-1 5.5c-.69 0-1.25.56-1.25 1.25v6.5c0 .69.56 1.25 1.25 1.25h10.5c.69 0 1.25-.56 1.25-1.25v-6.5c0-.69-.56-1.25-1.25-1.25H4.75Z" clip-rule="evenodd"></path>
    </svg>
</div>
                `;
        case Operation.ChangedExamSchedule:
            return `
<div class="inline-flex items-center justify-center flex-shrink-0 w-10 h-10 text-purple-500 bg-purple-100 rounded-lg">
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="w-5 h-5">
        <path d="M5.25 12a.75.75 0 0 1 .75-.75h.01a.75.75 0 0 1 .75.75v.01a.75.75 0 0 1-.75.75H6a.75.75 0 0 1-.75-.75V12ZM6 13.25a.75.75 0 0 0-.75.75v.01c0 .414.336.75.75.75h.01a.75.75 0 0 0 .75-.75V14a.75.75 0 0 0-.75-.75H6ZM7.25 12a.75.75 0 0 1 .75-.75h.01a.75.75 0 0 1 .75.75v.01a.75.75 0 0 1-.75.75H8a.75.75 0 0 1-.75-.75V12ZM8 13.25a.75.75 0 0 0-.75.75v.01c0 .414.336.75.75.75h.01a.75.75 0 0 0 .75-.75V14a.75.75 0 0 0-.75-.75H8ZM9.25 10a.75.75 0 0 1 .75-.75h.01a.75.75 0 0 1 .75.75v.01a.75.75 0 0 1-.75.75H10a.75.75 0 0 1-.75-.75V10ZM10 11.25a.75.75 0 0 0-.75.75v.01c0 .414.336.75.75.75h.01a.75.75 0 0 0 .75-.75V12a.75.75 0 0 0-.75-.75H10ZM9.25 14a.75.75 0 0 1 .75-.75h.01a.75.75 0 0 1 .75.75v.01a.75.75 0 0 1-.75.75H10a.75.75 0 0 1-.75-.75V14ZM12 9.25a.75.75 0 0 0-.75.75v.01c0 .414.336.75.75.75h.01a.75.75 0 0 0 .75-.75V10a.75.75 0 0 0-.75-.75H12ZM11.25 12a.75.75 0 0 1 .75-.75h.01a.75.75 0 0 1 .75.75v.01a.75.75 0 0 1-.75.75H12a.75.75 0 0 1-.75-.75V12ZM12 13.25a.75.75 0 0 0-.75.75v.01c0 .414.336.75.75.75h.01a.75.75 0 0 0 .75-.75V14a.75.75 0 0 0-.75-.75H12ZM13.25 10a.75.75 0 0 1 .75-.75h.01a.75.75 0 0 1 .75.75v.01a.75.75 0 0 1-.75.75H14a.75.75 0 0 1-.75-.75V10ZM14 11.25a.75.75 0 0 0-.75.75v.01c0 .414.336.75.75.75h.01a.75.75 0 0 0 .75-.75V12a.75.75 0 0 0-.75-.75H14Z"></path>
        <path fill-rule="evenodd" d="M5.75 2a.75.75 0 0 1 .75.75V4h7V2.75a.75.75 0 0 1 1.5 0V4h.25A2.75 2.75 0 0 1 18 6.75v8.5A2.75 2.75 0 0 1 15.25 18H4.75A2.75 2.75 0 0 1 2 15.25v-8.5A2.75 2.75 0 0 1 4.75 4H5V2.75A.75.75 0 0 1 5.75 2Zm-1 5.5c-.69 0-1.25.56-1.25 1.25v6.5c0 .69.56 1.25 1.25 1.25h10.5c.69 0 1.25-.56 1.25-1.25v-6.5c0-.69-.56-1.25-1.25-1.25H4.75Z" clip-rule="evenodd"></path>
    </svg>
</div>
            `;
        default:
            throw new Error("invalid notification operation");
    }
}