export const getTinyMCEOption = (selector = "", height = 0) => {
    return {
        selector,
        skin: "oxide-dark",
        content_css: "dark",
        height,
        menubar: false,
        plugins: ["lists", "link", "image", "codesample", "preview"],
        toolbar: "styles | bold italic underline | forecolor | bullist numlist | image codesample link "
    }
}