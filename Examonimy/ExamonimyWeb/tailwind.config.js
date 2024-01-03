/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Views/**/*.cshtml",
        "./wwwroot/**/*.{html,js}"
    ],
    theme: {
        extend: {},
    },
    darkMode: "class",
    corePlugins: {
        aspectRatio: false,
    },
    plugins: [
        require('@tailwindcss/aspect-ratio'),
        require('@tailwindcss/forms'),
        require('flowbite/plugin'),
        require('@tailwindcss/typography')
    ],
}

