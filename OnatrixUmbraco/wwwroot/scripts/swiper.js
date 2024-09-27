document.addEventListener('DOMContentLoaded', function () {
    var swiper = new Swiper(".mySwiper", {
        slidesPerView: 1.1,  // Number of cards per row
        spaceBetween: 20,  // Space between the cards
       
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
            // När skärmbredden är större än 768px (t.ex. tablets och desktops)
            768: {
                slidesPerView: 2,  // Antal slides per rad
                grid: {
                    rows: 2,  // Antal rader för större skärmar
                },
                spaceBetween: 30,  // Space between the cards for larger screens
            },
            1024: {
                slidesPerView: 3,
                grid: {
                    rows: 2,
                },
                spaceBetween: 30,
            }
            
        }
    });

    var swiper = new Swiper(".projectsSwiper", {

        slidesPerView: 1.1,  // Number of cards per row
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
            // När skärmbredden är större än 768px (t.ex. tablets och desktops)
            768: {
                slidesPerView: 1,  // Antal slides per rad
                grid: {
                    rows: 4,  // Antal rader för större skärmar
                },
                spaceBetween: 30,  // Space between the cards for larger screens
            },
            
        }
    });


   


  

});