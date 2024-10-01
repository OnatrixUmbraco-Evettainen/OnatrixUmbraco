document.addEventListener('DOMContentLoaded', function () {
    var swiper = new Swiper(".mySwiper", {
        slidesPerView: 1.1,
        spaceBetween: 20, 
       
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
        },
        breakpoints: {
            768: {
                slidesPerView: 2,
                slidesPerGroup: 2,
                grid: {
                    rows: 2,
                },
                spaceBetween: 30,
            },
            992: {
                slidesPerView: 3,
                slidesPerGroup: 4,
                grid: {
                    rows: 2,
                },
                spaceBetween: 30,
            }
            
        }
    });

    var swiper = new Swiper(".projectsSwiper", {

        slidesPerView: 1.1,
        spaceBetween: 20,
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
        },

        breakpoints: {
            768: {
                slidesPerView: 1, 
                grid: {
                    rows: 4,
                },
                spaceBetween: 30, 
            },
            
        }
    });
});