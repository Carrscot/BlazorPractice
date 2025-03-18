/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html,cshtml}"],
  theme: {
    extend: {},
  },
    plugins: [],
    prefix: "tw-",
    safelist: [
        'tw-bg-blue-500',
        'tw-bg-green-500',
        'tw-bg-gray-500',
    ],
}

