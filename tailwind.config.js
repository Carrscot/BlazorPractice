/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html,cshtml}"],
  theme: {
    extend: {},
  },
    plugins: [],
    prefix: "tw-",
    safelist: [
        'tw-bg-blue-500', 'tw-bg-blue-600',
        'tw-bg-green-500', 'tw-bg-green-600',
        'tw-bg-gray-500', 'tw-bg-gray-600',
        'tw-bg-yellow-500', 'tw-bg-yellow-600',
        'tw-bg-red-500', 'tw-bg-red-600',
        'tw-bg-gray-100',
        'tw-shadow-md',
        'tw-hover:bg-blue-600',
        'tw-hover:bg-green-600',
        'tw-hover:bg-gray-600',
        'tw-hover:bg-yellow-600',
        'tw-hover:bg-red-600',
        'tw-hover:bg-gray-100',
        'tw-hover:shadow-md',
    ],
}

