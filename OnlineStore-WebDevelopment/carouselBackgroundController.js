// carouselController.js
let currentSlide = 0;
const slides = document.querySelectorAll('.carousel-slide');
const indicators = document.querySelectorAll('.indicator');
let autoSlideInterval;

// Função para exibir um slide específico
function showSlide(index) {
    // Esconde todos os slides e remove a classe "active" dos indicadores
    slides.forEach((slide, i) => {
        slide.classList.remove('active');
        indicators[i].classList.remove('active');
    });

    // Exibe o slide atual e adiciona a classe "active" ao indicador
    slides[index].classList.add('active');
    indicators[index].classList.add('active');
    currentSlide = index;
}

// Função para ir para o próximo slide
function nextSlide() {
    let nextIndex = (currentSlide + 1) % slides.length;
    showSlide(nextIndex);
}

// Função para iniciar o carrossel automático
function startAutoSlide() {
    autoSlideInterval = setInterval(nextSlide, 8000);
}

// Função para pausar o carrossel automático
function stopAutoSlide() {
    clearInterval(autoSlideInterval);
}

// Event listeners para os indicadores
indicators.forEach((indicator, index) => {
    indicator.addEventListener('click', () => {
        stopAutoSlide();
        showSlide(index);
        startAutoSlide();
    });
});

// Inicializar o carrossel com o primeiro slide e iniciar o auto-slide
showSlide(0);
startAutoSlide();
