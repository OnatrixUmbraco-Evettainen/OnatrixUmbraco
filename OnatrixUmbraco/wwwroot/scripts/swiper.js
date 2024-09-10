document.addEventListener('DOMContentLoaded', function () {
    var swiper = new Swiper('.swiper-container', {
        loop: false, // Gör att slidern loopar
        slidesPerView: 1, // Hur många slides visas samtidigt
        spaceBetween: 30, // Mellanrum mellan slides
        
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        },
        breakpoints: {
            300: {
                
                slidesPerView: 1.2, // 1 slide på små skärmar
                centeredSlides: true,
                spaceBetween: 20,
            },
            768: {
                slidesPerView: 2, // 2 slides på medium skärmar
                spaceBetween: 30,
            },
            1024: {
                slidesPerView: 3, // 3 slides på stora skärmar
                spaceBetween: 30,
            },
        },
    });
});