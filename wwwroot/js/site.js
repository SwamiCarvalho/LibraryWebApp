// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showDiv(divId, element) {
    /*var libCodeElement = document.getElementsByClassName(divClass);
    libCodeElement.style.display = element.value == "Librarian" ? 'block' : 'none';*/

    document.getElementById(divId).style.display = element.value == "Librarian" ? "block" : "none";
}
