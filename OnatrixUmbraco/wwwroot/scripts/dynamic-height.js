document.addEventListener("DOMContentLoaded", function () {
    const backgroundDivs = document.querySelectorAll('.background-color');
    backgroundDivs.forEach(function (backgroundDiv) {
        let height = backgroundDiv.getAttribute('data-height');
        if (height) {
            backgroundDiv.style.setProperty('--dynamic-height', height + '%');
        }
    });


    const imageDivs = document.querySelectorAll('.section-bg-image');
    imageDivs.forEach(function (imageDiv) {
        let height = imageDiv.getAttribute('data-height');
        if (height) {
            imageDiv.style.setProperty('--image-height', height + '%');
        }
    });

    const sectionWrappers = document.querySelectorAll('.section-wrapper');
    sectionWrappers.forEach(function (sectionWrapper) {
        let height = sectionWrapper.getAttribute('data-height');
        if (height === '100') {
            sectionWrapper.style.setProperty('--section-height', height + '%');
        } else if (height) {
            sectionWrapper.style.setProperty('--section-height', height + 'px');
        }
    });
 
});




