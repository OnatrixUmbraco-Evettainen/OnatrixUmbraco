document.addEventListener('DOMContentLoaded', function () {

    // const btnOpen = document.querySelector('#btnOpen');
    // const btnClose = document.querySelector('#btnClose');

    const btnToggle = document.querySelector('#btnToggle');

    const media = window.matchMedia('(max-width: 768px)');
    const mainNavMenu = document.querySelector('.mainnav__menu');
    const main = document.querySelector('main');
    const body = document.querySelector('body');


    function setupMainNav(e) {
        if (e.matches) {
            console.log('is mobile');
            mainNavMenu.setAttribute('inert', '');
            mainNavMenu.style.transition = 'none';
        } else {
            console.log('is desktop');
            closeMobileMenu();
            mainNavMenu.removeAttribute('inert');
       
            
        }
    }

    function toggleMobileMenu() {
        const isExpanded = btnToggle.getAttribute('aria-expanded') === 'true';

        if (isExpanded) {
            closeMobileMenu();
        } else {
            openMobileMenu();
        }
    }




    function openMobileMenu() {
        btnToggle.setAttribute('aria-expanded', 'true');
        mainNavMenu.removeAttribute('inert');
        mainNavMenu.removeAttribute('style');
        main.setAttribute('inert', '');
        bodyScrollLockUpgrade.disableBodyScroll(body);

        btnToggle.classList.add('open');

        // btnClose.focus();
    }

    function closeMobileMenu() {
        btnToggle.setAttribute('aria-expanded', 'false');
        mainNavMenu.setAttribute('inert', '');
        main.removeAttribute('inert');
        bodyScrollLockUpgrade.enableBodyScroll(body);
        // btnOpen.focus();

        btnToggle.classList.remove('open');


        setTimeout(() => {
            mainNavMenu.style.transition = 'none'
        }, 500);
    }

    setupMainNav(media);

    btnToggle.addEventListener('click', toggleMobileMenu)

    // btnOpen.addEventListener('click', openMobileMenu);
    // btnClose.addEventListener('click', closeMobileMenu);

    media.addEventListener('change', function (e) {
        setupMainNav(e);
    });


});