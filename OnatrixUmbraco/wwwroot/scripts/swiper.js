document.addEventListener('DOMContentLoaded', function () {
    var swiper = new Swiper(".mySwiper", {
        slidesPerView: 3,  // Antal slides per rad
        grid: {
            rows: 2,  // Antal rader
        },
        spaceBetween: 30,  // Mellanrum mellan slides
        navigation: {
            nextEl: ".swiper-button-next",  // Anpassad Next-knapp
            prevEl: ".swiper-button-prev",  // Anpassad Prev-knapp
        },
        pagination: {
            el: ".swiper-pagination",  // Koppla till pagination
            clickable: true,
            renderBullet: function (index, className) {
                // Skapa numrerade pagination bullets
                return '<span class="' + className + '">' + (index + 1) + "</span>";
            }
        }
    });
});