const placeholderImagesFW = [
    '1.png',
    '2.png',
    '3.png',
    '4.png',
];

// Função para obter uma imagem aleatória do array
function getRandomImage() {
    const randomIndex = Math.floor(Math.random() * placeholderImages.length);
    return `Images/FWGalleryPlaceHolders/${placeholderImages[randomIndex]}`;
}

// Função para criar um item da galeria
function createGalleryItem(index) {
    const item = document.createElement('div');
    item.classList.add('fw-gallery-item');

    // Criar container para cores e imagem
    const imageContainer = document.createElement('div');
    imageContainer.classList.add('fw-image-container');

    // Criar bolinhas das cores (ficam em cima)
    const colorContainer = document.createElement('div');
    colorContainer.classList.add('fw-color-container');

    const colors = ['#000000', '#343434', '#686868'];
    colors.forEach(color => {
        const colorCircle = document.createElement('div');
        colorCircle.classList.add('fw-color-circle');
        colorCircle.style.backgroundColor = color;
        colorContainer.appendChild(colorCircle);
    });

    imageContainer.appendChild(colorContainer);

    // Adicionar a imagem do item
    const img = document.createElement('img');
    img.src = getRandomImage();
    img.alt = `Item ${index}`;
    img.classList.add('fw-item-image');
    imageContainer.appendChild(img);

    item.appendChild(imageContainer);

    // Criar container de informações (abaixo da imagem)
    const infoContainer = document.createElement('div');
    infoContainer.classList.add('fw-item-info');

    const title = document.createElement('h3');
    title.classList.add('fw-item-title');
    title.textContent = `Sneakers ${index}`;
    infoContainer.appendChild(title);

    const subtitle = document.createElement('p');
    subtitle.classList.add('fw-item-subtitle');
    subtitle.textContent = `Shoe ${index} type`;
    infoContainer.appendChild(subtitle);

    // Adiciona o container de informações no final (abaixo da imagem)
    item.appendChild(infoContainer);

    return item;
}

// Adicionar itens a todas as galerias
const galleriesFW = document.querySelectorAll('.fw-gallery');
const numberOfItemsFW = 15;

galleries.forEach(gallery => {
    for (let i = 1; i <= numberOfItems; i++) {
        const galleryItem = createGalleryItem(i);
        gallery.appendChild(galleryItem);
    }
});
