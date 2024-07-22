// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function MostrarRespuesta(id,flag) {
    var element = document.getElementById(id);
    if (flag) {
        element.classList.remove("visually-hidden");
    }
    else {
        element.classList.add("visually-hidden");
    }
}
