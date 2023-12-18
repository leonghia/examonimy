// Imports

import { ConfirmModalComponent } from "../components/confirm-modal.component.js";

// DOM selectors
const deleteButton = document.querySelector("#delete-btn");
const modalContainer = document.querySelector("#modal-container");
// States

// Function expressions

// Event listeners
deleteButton.addEventListener("click", event => {
    const confirmModalComponent = new ConfirmModalComponent(modalContainer, {
        title: "Xóa câu hỏi",
        description: "Bạn có chắc muốn xóa câu hỏi này? Câu hỏi sau khi xóa sẽ không thể khôi phục lại.",
        ctaText: "Xác nhận"
    });
    confirmModalComponent.connectedCallback();
});
// On load