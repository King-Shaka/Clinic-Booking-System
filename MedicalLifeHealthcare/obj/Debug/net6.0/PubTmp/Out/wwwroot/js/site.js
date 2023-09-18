// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("scroll", () => {
    if (window.scrollY > 10) { // Adjust the scroll position as needed
        document.querySelector("header").style.background = "rgba(0, 0, 0, 0.7)";
    } else {
        document.querySelector("header").style.background = "transparent";
    }
});
