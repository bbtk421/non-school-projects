const nav_btn = document.querySelector('.hamburger');
const mobile_menu = document.querySelector('.mobile-nav')

/* opens and animates burger menu */
nav_btn.addEventListener('click', () => {
    nav_btn.classList.toggle('is-active');
    mobile_menu.classList.toggle('is-active');
});

/* closes and animates burger menu */
mobile_menu.addEventListener('click', () => {
    mobile_menu.classList.toggle('is-active');
    nav_btn.classList.toggle('is-active');
});

