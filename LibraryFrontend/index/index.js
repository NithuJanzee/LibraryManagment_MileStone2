let slideIndex = 0;
const slides = document.querySelectorAll('.slide');
const modal = document.getElementById("loginModal");
const btn = document.getElementById("signinBtn");
const span = document.getElementsByClassName("close")[0];

function showSlides() {
    for (let slide of slides) {
        slide.style.display = "none";  
    }
    slideIndex++;
    if (slideIndex > slides.length) {slideIndex = 1}    
    slides[slideIndex-1].style.display = "block";  
    setTimeout(showSlides, 2000); // Change image every 2 seconds
}

btn.onclick = function() {
    modal.style.display = "block";
}

span.onclick = function() {
    modal.style.display = "none";
}

window.onclick = function(event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

showSlides();
