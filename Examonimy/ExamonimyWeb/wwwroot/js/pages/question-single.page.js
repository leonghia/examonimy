// Imports

import { ConfirmModalComponent } from "../components/confirm-modal.component.js";
import { BASE_URL } from "../config.js";
import { deleteData } from "../helpers/ajax.helper.js";

// DOM selectors
const deleteButton = document.querySelector("#delete-btn");
const modalContainer = document.querySelector("#modal-container");


// States
const temp = document.location.href.split("/");
const questionId = temp[temp.length - 1];


// Function expressions


// Event listeners
deleteButton.addEventListener("click", event => {
    let confirmModalComponent = new ConfirmModalComponent(modalContainer, {
        title: "Xóa câu hỏi",
        description: "Bạn có chắc muốn xóa câu hỏi này? Câu hỏi sau khi bị xóa sẽ không thể khôi phục lại.",
        ctaText: "Xác nhận",
        href: `/question/delete/${questionId}`
    });
    confirmModalComponent.connectedCallback();
    confirmModalComponent.subscribe("cancel", () => {
        confirmModalComponent = undefined;
    });
    confirmModalComponent.subscribe("confirm", async () => {
        try {
            await deleteData("question", questionId);
            window.location.href = `${BASE_URL}/question`;
        } catch (err) {
            console.error(err);
        }
    });
});
// On load