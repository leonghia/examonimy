import { SectionHeadingComponent } from "../components/section-heading.component.js";
import { getTinyMCEOption } from "../helpers/tinymce.helper.js";

const renderSingleChoiceQuestionForCreation = (questionOptionContainer = new HTMLElement()) => {
    tinymce.remove("#option-a");
    tinymce.remove("#option-b");
    tinymce.remove("#option-c");
    tinymce.remove("#option-d");
    clearQuestionOptionContainer(questionOptionContainer);
    questionOptionContainer.insertAdjacentHTML("beforeend", `
    ${new SectionHeadingComponent("Nhập phương án", "Vui lòng đánh dấu vào ô đáp án cho câu hỏi mà bạn đã tạo.").render(true)}
<fieldset>
  <legend class="sr-only">Plan</legend>
  <div class="space-y-5">
    <div class="relative flex items-start">
      <div class="flex h-6 items-center">
        <input id="small" aria-describedby="small-description" name="options" type="radio" class="h-4 w-4 border-gray-300 text-violet-600 focus:ring-violet-600">
      </div>
      <div class="ml-3 text-sm leading-6 grow">
        <label for="small" class="font-medium text-gray-700">1. Phương án A</label>
        <div class="mt-2">
            <textarea id="option-a"></textarea>
        </div>
      </div>
    </div>
    <div class="relative flex items-start">
      <div class="flex h-6 items-center">
        <input id="medium" aria-describedby="medium-description" name="options" type="radio" class="h-4 w-4 border-gray-300 text-violet-600 focus:ring-violet-600">
      </div>
      <div class="ml-3 text-sm leading-6 grow">
        <label for="medium" class="font-medium text-gray-700">2. Phương án B</label>
        <div class="mt-2">
            <textarea id="option-b"></textarea>
        </div>
      </div>
    </div>
    <div class="relative flex items-start">
      <div class="flex h-6 items-center">
        <input id="large" aria-describedby="large-description" name="options" type="radio" class="h-4 w-4 border-gray-300 text-violet-600 focus:ring-violet-600">
      </div>
      <div class="ml-3 text-sm leading-6 grow">
        <label for="large" class="font-medium text-gray-700">3. Phương án C</label>
        <div class="mt-2">
            <textarea id="option-c"></textarea>
        </div>
      </div>
    </div>
    <div class="relative flex items-start">
      <div class="flex h-6 items-center">
        <input id="large" aria-describedby="large-description" name="options" type="radio" class="h-4 w-4 border-gray-300 text-violet-600 focus:ring-violet-600">
      </div>
      <div class="ml-3 text-sm leading-6 grow">
        <label for="large" class="font-medium text-gray-700">4. Phương án D</label>
        <div class="mt-2">
            <textarea id="option-d"></textarea>
        </div>
      </div>
    </div>
  </div>
</fieldset>
    `);
    tinymce.init(getTinyMCEOption("#option-a", 200));
    tinymce.init(getTinyMCEOption("#option-b", 200));
    tinymce.init(getTinyMCEOption("#option-c", 200));
    tinymce.init(getTinyMCEOption("#option-d", 200));
}

const renderMultipleChoiceQuestionForCreation = (questionOptionContainer = new HTMLElement()) => {
    tinymce.remove("#option-a");
    tinymce.remove("#option-b");
    tinymce.remove("#option-c");
    tinymce.remove("#option-d");
    clearQuestionOptionContainer(questionOptionContainer);
    questionOptionContainer.insertAdjacentHTML("beforeend", `
    ${new SectionHeadingComponent("Nhập phương án", "Vui lòng đánh dấu vào ô đáp án cho câu hỏi mà bạn đã tạo.").render(true) }
<fieldset>
  <legend class="sr-only">Plan</legend>
  <div class="space-y-5">
    <div class="relative flex items-start">
      <div class="flex h-6 items-center">
        <input id="small" aria-describedby="small-description" name="options" type="checkbox" class="rounded h-4 w-4 border-gray-300 text-violet-600 focus:ring-violet-600">
      </div>
      <div class="ml-3 text-sm leading-6 grow">
        <label for="small" class="font-medium text-gray-700">1. Phương án A</label>
        <div class="mt-2">
            <textarea id="option-a"></textarea>
        </div>
      </div>
    </div>
    <div class="relative flex items-start">
      <div class="flex h-6 items-center">
        <input id="medium" aria-describedby="medium-description" name="options" type="checkbox" class="rounded h-4 w-4 border-gray-300 text-violet-600 focus:ring-violet-600">
      </div>
      <div class="ml-3 text-sm leading-6 grow">
        <label for="medium" class="font-medium text-gray-700">2. Phương án B</label>
        <div class="mt-2">
            <textarea id="option-b"></textarea>
        </div>
      </div>
    </div>
    <div class="relative flex items-start">
      <div class="flex h-6 items-center">
        <input id="large" aria-describedby="large-description" name="options" type="checkbox" class="rounded h-4 w-4 border-gray-300 text-violet-600 focus:ring-violet-600">
      </div>
      <div class="ml-3 text-sm leading-6 grow">
        <label for="large" class="font-medium text-gray-700">3. Phương án C</label>
        <div class="mt-2">
            <textarea id="option-c"></textarea>
        </div>
      </div>
    </div>
    <div class="relative flex items-start">
      <div class="flex h-6 items-center">
        <input id="large" aria-describedby="large-description" name="plan" type="checkbox" class="rounded h-4 w-4 border-gray-300 text-violet-600 focus:ring-violet-600">
      </div>
      <div class="ml-3 text-sm leading-6 grow">
        <label for="large" class="font-medium text-gray-700">4. Phương án D</label>
        <div class="mt-2">
            <textarea id="option-d"></textarea>
        </div>
      </div>
    </div>
  </div>
</fieldset>
    `);
    tinymce.init(getTinyMCEOption("#option-a", 200));
    tinymce.init(getTinyMCEOption("#option-b", 200));
    tinymce.init(getTinyMCEOption("#option-c", 200));
    tinymce.init(getTinyMCEOption("#option-d", 200));
}

const renderEssayQuestionForCreation = (questionOptionContainer = new HTMLElement()) => {
    tinymce.remove("#answer");
    clearQuestionOptionContainer(questionOptionContainer);
    questionOptionContainer.insertAdjacentHTML("beforeend", `
    ${new SectionHeadingComponent("Nhập đáp án", "Vui lòng viết đáp án cho câu hỏi mà bạn đã tạo.").render(true) }
    <div class="mt-2">
        <textarea id="answer"></textarea>
    </div>
    `);
    tinymce.init(getTinyMCEOption("#answer", 300));
}

const renderTrueFalseQuestionForCreation = (questionOptionContainer = new HTMLElement()) => {
    clearQuestionOptionContainer(questionOptionContainer);
    questionOptionContainer.insertAdjacentHTML("beforeend", `
    ${new SectionHeadingComponent("Nhập đáp án", "Vui lòng chọn đáp án cho câu hỏi mà bạn đã tạo.").render(true)}
<fieldset>
  <legend class="sr-only">Plan</legend>
  <div class="space-y-5">
    <div class="relative flex items-start">
      <div class="flex h-6 items-center">
        <input id="small" aria-describedby="small-description" name="plan" type="radio" class="h-4 w-4 border-gray-300 text-violet-600 focus:ring-violet-600">
      </div>
      <div class="ml-3 text-sm leading-6">
        <label for="small" class="font-medium text-gray-700">Đúng</label>      
      </div>
    </div>
    <div class="relative flex items-start">
      <div class="flex h-6 items-center">
        <input id="medium" aria-describedby="medium-description" name="plan" type="radio" class="h-4 w-4 border-gray-300 text-violet-600 focus:ring-violet-600">
      </div>
      <div class="ml-3 text-sm leading-6">
        <label for="medium" class="font-medium text-gray-700">Sai</label>       
      </div>
    </div>
    <div class="relative flex items-start">
      <div class="flex h-6 items-center">
        <input id="large" aria-describedby="large-description" name="plan" type="radio" class="h-4 w-4 border-gray-300 text-violet-600 focus:ring-violet-600">
      </div>
      <div class="ml-3 text-sm leading-6">
        <label for="large" class="font-medium text-gray-700">Không đủ kết luận</label>     
      </div>
    </div>
  </div>
</fieldset>
    `);

}

const renderFillInBlankQuestionForCreation = (questionOptionContainer = new HTMLElement()) => {
    tinymce.remove("#blank-1");
    tinymce.remove("#blank-2");
    clearQuestionOptionContainer(questionOptionContainer);
    questionOptionContainer.insertAdjacentHTML("beforeend", `
    ${new SectionHeadingComponent("Nhập đáp án", "Vui lòng viết đáp án cho câu hỏi mà bạn đã tạo.").render(true)}
        <div class="mt-6 mb-10">
            <label for="question-content" class="block text-sm leading-6 text-gray-700 font-medium">Đáp án cho chỗ trống 1</label>
            <div class="mt-2">
                <textarea id="blank-1"></textarea>
            </div>
        </div>
        <div class="mt-6 mb-10">
            <label for="question-content" class="block text-sm leading-6 text-gray-700 font-medium">Đáp án cho chỗ trống 2</label>
            <div class="mt-2">
                <textarea id="blank-2"></textarea>
            </div>
        </div>
    `);
    tinymce.init(getTinyMCEOption("#blank-1", 200));
    tinymce.init(getTinyMCEOption("#blank-2", 200));
}

const clearQuestionOptionContainer = (optionQuestionContainer = new HTMLElement()) => {
    if (optionQuestionContainer.firstChild) {
        optionQuestionContainer.innerHTML = "";
    }
}

export const QuestionTypeRenderers = {
    singleChoice: renderSingleChoiceQuestionForCreation,
    multipleChoice: renderMultipleChoiceQuestionForCreation,
    essay: renderEssayQuestionForCreation,
    trueFalse: renderTrueFalseQuestionForCreation,
    fillInBlank: renderFillInBlankQuestionForCreation
}