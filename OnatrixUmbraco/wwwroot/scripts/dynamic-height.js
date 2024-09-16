document.addEventListener("DOMContentLoaded", function () {
    const backgroundDivs = document.querySelectorAll('.background-color');
    backgroundDivs.forEach(function (backgroundDiv) {
        let height = backgroundDiv.getAttribute('data-height');
        if (height) {
            backgroundDiv.style.setProperty('--dynamic-height', height + '%');
        }
    });


    const formHeaders = document.querySelectorAll('.form-header');
    formHeaders.forEach(function (formHeader) {
        let color = formHeader.getAttribute('data-color');
        if (color) {
            formHeader.style.setProperty('--dynamic-color', color);
        }
    });
 
});




