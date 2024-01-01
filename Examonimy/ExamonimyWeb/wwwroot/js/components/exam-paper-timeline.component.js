import { ExamPaperReviewHistory, ExamPaperReviewHistoryComment, ExamPaperReviewHistoryAddReviewer, ExamPaperReviewHistoryEdit } from "../models/exam-paper.model.js";
import { convertToAgo } from "../helpers/datetime.helper.js";
import { Operation } from "../helpers/operation.helper.js";

export class ExamPaperTimelineComponent {
    #container;
    #examPaperReviewHistories;

    constructor(container = new HTMLElement(), examPaperReviewHistories = [new ExamPaperReviewHistory()]) {
        this.#container = container;
        this.#examPaperReviewHistories = examPaperReviewHistories;
    }

    connectedCallback() {
        this.populate();
    }

    populate() {
        this.#container.innerHTML = this.#render();
    }

    insertHistory(data = new ExamPaperReviewHistory()) {
        this.#examPaperReviewHistories.push(data);
    }

    set examPaperReviewHistories(value) {
        this.#examPaperReviewHistories = value;
    }

    #render() {
        return `
<ul id="review-history-list" class="-mb-8">
    ${this.#examPaperReviewHistories.reduce((acc, v, i) => {
        switch (v.operationId) {
            case Operation.CreateExamPaper:
                return acc + `
<li>
    <div class="relative pb-8">
        ${i === this.#examPaperReviewHistories.length - 1 ? `` : `<span class="absolute left-5 top-5 -ml-px h-full w-0.5 bg-gray-200" aria-hidden="true"></span>`}
        <div class="relative flex items-start space-x-3">
            <div>
                <div class="relative px-1">
                    <div class="flex h-8 w-8 items-center justify-center rounded-full bg-gray-100 ring-8 ring-white">                                               
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="h-5 w-5 text-gray-500">
                            <path fill-rule="evenodd" d="M5.625 1.5c-1.036 0-1.875.84-1.875 1.875v17.25c0 1.035.84 1.875 1.875 1.875h12.75c1.035 0 1.875-.84 1.875-1.875V12.75A3.75 3.75 0 0 0 16.5 9h-1.875a1.875 1.875 0 0 1-1.875-1.875V5.25A3.75 3.75 0 0 0 9 1.5H5.625ZM7.5 15a.75.75 0 0 1 .75-.75h7.5a.75.75 0 0 1 0 1.5h-7.5A.75.75 0 0 1 7.5 15Zm.75 2.25a.75.75 0 0 0 0 1.5H12a.75.75 0 0 0 0-1.5H8.25Z" clip-rule="evenodd" />
                            <path d="M12.971 1.816A5.23 5.23 0 0 1 14.25 5.25v1.875c0 .207.168.375.375.375H16.5a5.23 5.23 0 0 1 3.434 1.279 9.768 9.768 0 0 0-6.963-6.963Z" />
                        </svg>
                    </div>
                </div>
            </div>
            <div class="min-w-0 flex-1 py-1.5">
                <div class="text-sm text-gray-500">
                    <span class="font-medium text-gray-900">${v.actorName}</span>
                    đã tạo đề thi                   
                    <span class="whitespace-nowrap">${convertToAgo(new Date(v.createdAt))}</span>
                </div>
            </div>
        </div>
    </div>
</li>
                `;
            case Operation.EditExamPaper:
                const eprhe = new ExamPaperReviewHistoryEdit();
                Object.assign(eprhe, v);
                return acc + `
<li>
    <div class="relative pb-8">
        ${i === this.#examPaperReviewHistories.length - 1 ? `` : `<span class="absolute left-5 top-5 -ml-px h-full w-0.5 bg-gray-200" aria-hidden="true"></span>`}
        <div class="relative flex items-start space-x-3">
            <div>
                <div class="relative px-1">
                    <div class="flex h-8 w-8 items-center justify-center rounded-full bg-gray-100 ring-8 ring-white">                      
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="h-5 w-5 text-gray-500">
                            <path d="m5.433 13.917 1.262-3.155A4 4 0 0 1 7.58 9.42l6.92-6.918a2.121 2.121 0 0 1 3 3l-6.92 6.918c-.383.383-.84.685-1.343.886l-3.154 1.262a.5.5 0 0 1-.65-.65Z" />
                            <path d="M3.5 5.75c0-.69.56-1.25 1.25-1.25H10A.75.75 0 0 0 10 3H4.75A2.75 2.75 0 0 0 2 5.75v9.5A2.75 2.75 0 0 0 4.75 18h9.5A2.75 2.75 0 0 0 17 15.25V10a.75.75 0 0 0-1.5 0v5.25c0 .69-.56 1.25-1.25 1.25h-9.5c-.69 0-1.25-.56-1.25-1.25v-9.5Z" />
                        </svg>
                    </div>
                </div>
            </div>
            <div class="min-w-0 flex-1 py-1.5">
                <div class="text-sm text-gray-500">
                    <span class="font-medium text-gray-900">${eprhe.actorName}</span>
                    đã cập nhật đề thi với nội dung: <span class="font-medium text-gray-700">${eprhe.commitMessage}</span>                  
                    <span class="whitespace-nowrap">${convertToAgo(new Date(eprhe.createdAt))}</span>
                </div>
            </div>
        </div>
    </div>
</li>
                `;
            case Operation.AskForReviewForExamPaper:
                const eprhar = new ExamPaperReviewHistoryAddReviewer();
                Object.assign(eprhar, v);
                return acc + `
<li>
    <div class="relative pb-8">
        ${i === this.#examPaperReviewHistories.length - 1 ? `` : `<span class="absolute left-5 top-5 -ml-px h-full w-0.5 bg-gray-200" aria-hidden="true"></span>`}
        <div class="relative flex items-start space-x-3">
            <div>
                <div class="relative px-1">
                    <div class="flex h-8 w-8 items-center justify-center rounded-full bg-gray-100 ring-8 ring-white">
                        <svg class="h-5 w-5 text-gray-500" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                            <path fill-rule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-5.5-2.5a2.5 2.5 0 11-5 0 2.5 2.5 0 015 0zM10 12a5.99 5.99 0 00-4.793 2.39A6.483 6.483 0 0010 16.5a6.483 6.483 0 004.793-2.11A5.99 5.99 0 0010 12z" clip-rule="evenodd"></path>
                        </svg>
                    </div>
                </div>
            </div>
            <div class="min-w-0 flex-1 py-1.5">
                <div class="text-sm text-gray-500">
                    <span class="font-medium text-gray-900">${eprhar.actorName}</span>
                    đã thêm
                    <span class="font-medium text-gray-900">${eprhar.reviewerName}</span> 
                    làm kiểm duyệt viên
                    <span class="whitespace-nowrap">${convertToAgo(new Date(eprhar.createdAt))}</span>
                </div>
            </div>
        </div>
    </div>
</li>
                `;
            case Operation.CommentExamPaper:
                const eprhc = new ExamPaperReviewHistoryComment();
                Object.assign(eprhc, v);
                return acc + `
<li>
    <div class="relative pb-8">
        ${i === this.#examPaperReviewHistories.length - 1 ? `` : `<span class="absolute left-5 top-5 -ml-px h-full w-0.5 bg-gray-200" aria-hidden="true"></span>`}
        <div class="relative flex items-start space-x-3">
            <div class="relative">
                <img class="flex h-10 w-10 items-center justify-center rounded-full bg-gray-400 ring-8 ring-white" src="${eprhc.actorProfilePicture}" alt="user profile picture">

                <span class="absolute -bottom-0.5 -right-1 rounded-tl bg-white px-0.5 py-px">
                    <svg class="h-5 w-5 text-gray-400" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                        <path fill-rule="evenodd" d="M10 2c-2.236 0-4.43.18-6.57.524C1.993 2.755 1 4.014 1 5.426v5.148c0 1.413.993 2.67 2.43 2.902.848.137 1.705.248 2.57.331v3.443a.75.75 0 001.28.53l3.58-3.579a.78.78 0 01.527-.224 41.202 41.202 0 005.183-.5c1.437-.232 2.43-1.49 2.43-2.903V5.426c0-1.413-.993-2.67-2.43-2.902A41.289 41.289 0 0010 2zm0 7a1 1 0 100-2 1 1 0 000 2zM8 8a1 1 0 11-2 0 1 1 0 012 0zm5 1a1 1 0 100-2 1 1 0 000 2z" clip-rule="evenodd"></path>
                    </svg>
                </span>
            </div>
            <div class="min-w-0 flex-1">
                <div>
                    <div class="text-sm flex items-center">
                        <span class="font-medium text-gray-900 mr-2">${eprhc.actorName}</span>     
                        ${eprhc.isAuthor ? `<span class="inline-flex items-center rounded-md bg-emerald-200 px-2 py-1 text-xs font-medium text-emerald-700">Tác giả đề thi</span>` : `<span class="inline-flex items-center rounded-md bg-gray-200 px-2 py-1 text-xs font-medium text-gray-600">Kiểm duyệt viên</span>`}
                    </div>
                    <p class="mt-0.5 text-sm text-gray-500">Đã bình luận ${convertToAgo(new Date(eprhc.createdAt))}</p>
                </div>
                <div class="mt-2 text-sm text-gray-700">
                    <p>${eprhc.comment}</p>
                </div>
            </div>
        </div>
    </div>
</li>
                `;           
            default:
                return;
        }
    }, "")}
</ul>
        `;
    }
}