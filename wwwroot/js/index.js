// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const buttons = document.querySelectorAll(".nav-link"); // Lee los elementos que tiene la clase 

buttons.forEach(button => { // Por cada elemento del array
    button.addEventListener("mousemove", (e) => { // Añade el evento
        const { x, y } = button.getBoundingClientRect();
        button.style.setProperty("--x", (e.clientX - x) + "px");
        button.style.setProperty("--y", (e.clientY - y) + "px");
    });
});