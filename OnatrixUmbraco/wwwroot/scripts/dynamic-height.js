document.addEventListener("DOMContentLoaded", function () {
    const backgroundDivs = document.querySelectorAll('.background-color');
    backgroundDivs.forEach(function (backgroundDiv) {
        let height = backgroundDiv.getAttribute('data-height');
        if (height) {
            backgroundDiv.style.setProperty('--dynamic-height', height + '%');
        }
    });
});