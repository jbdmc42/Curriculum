const placeholderImages = [
    '1.png',
    '2.png',
    '3.png',
];

// Função para obter uma imagem aleatória do array
function getRandomImage() {
    const randomIndex = Math.floor(Math.random() * placeholderImages.length);
    return `Images/GalleryPlaceHolders/${placeholderImages[randomIndex]}`;
}

// Função para criar um item da galeria
function createGalleryItem(index) {
    const item = document.createElement('div');
    item.classList.add('gallery-item');

    // Criar container para cores e imagem
    const imageContainer = document.createElement('div');
    imageContainer.classList.add('image-container');

    // Criar bolinhas das cores (ficam em cima)
    const colorContainer = document.createElement('div');
    colorContainer.classList.add('color-container');

    const colors = ['#000000', '#343434', '#686868'];
    colors.forEach(color => {
        const colorCircle = document.createElement('div');
        colorCircle.classList.add('color-circle');
        colorCircle.style.backgroundColor = color;
        colorContainer.appendChild(colorCircle);
    });

    imageContainer.appendChild(colorContainer);

    // Adicionar a imagem do item
    const img = document.createElement('img');
    img.src = getRandomImage();
    img.alt = `Item ${index}`;
    img.classList.add('item-image');
    imageContainer.appendChild(img);

    item.appendChild(imageContainer);

    // Criar container de informações (abaixo da imagem)
    const infoContainer = document.createElement('div');
    infoContainer.classList.add('item-info');

    const title = document.createElement('h3');
    title.classList.add('item-title');
    title.textContent = `PRODUCT ${index}`;
    infoContainer.appendChild(title);

    const subtitle = document.createElement('p');
    subtitle.classList.add('item-subtitle');
    subtitle.textContent = `Product ${index} type`;
    infoContainer.appendChild(subtitle);

    // Adiciona o container de informações no final (abaixo da imagem)
    item.appendChild(infoContainer);

    return item;
}

// Adicionar itens a todas as galerias
const galleries = document.querySelectorAll('.gallery');
const numberOfItems = 15;

galleries.forEach(gallery => {
    for (let i = 1; i <= numberOfItems; i++) {
        const galleryItem = createGalleryItem(i);
        gallery.appendChild(galleryItem);
    }
});
