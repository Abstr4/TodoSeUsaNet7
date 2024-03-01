function toggleSidenav() {
    document.body.classList.toggle('sidenav-active');
    document.body.classList.toggle('noscroll');
}

// ==============
// Automated Demo

const checkbox = document.querySelector('input');
const interval = setInterval(toggleSidenav, 1500);

document.addEventListener('mousemove', removeInterval);
document.addEventListener('click', removeInterval);

function removeInterval() {
    clearInterval(interval);
    document.removeEventListener('mousemove', removeInterval);
    document.removeEventListener('click', removeInterval);
}

