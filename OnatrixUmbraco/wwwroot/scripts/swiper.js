document.addEventListener('DOMContentLoaded', function () {
    var swiper = new Swiper(".mySwiper", {
        slidesPerView: 3,  // Number of cards per row
        grid: {
            rows: 2,  // Number of rows
        },
        spaceBetween: 30,  // Space between the cards
        pagination: {
            el: ".swiper-pagination",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + (index + 1) + "</span>";
            }
        },
        navigation: {
            nextEl: ".custom-next",
            prevEl: ".custom-prev",
        }
    });


});