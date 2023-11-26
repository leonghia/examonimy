/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./wwwroot/**/*.{html,js}", "./Views/**/*.cshtml", "./node_modules/flowbite/**/*.js"],
    theme: {
        extend: {},
    },
    corePlugins: {
        aspectRatio: false,
    },
    plugins: [
        require('@tailwindcss/aspect-ratio'),
        require('@tailwindcss/forms'),
        require('flowbite/plugin')
    ],
}

