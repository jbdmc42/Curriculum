document.querySelector('.menu-toggle').addEventListener('click', (event) => {
  const menu = document.querySelector('.dropdown-menu');
  menu.classList.toggle('show');

  // Impede o clique no botão de fechar o menu imediatamente
  event.stopPropagation();
});

document.addEventListener('click', (event) => {
  const menu = document.querySelector('.dropdown-menu');
  const toggleButton = document.querySelector('.menu-toggle');

  // Verifica se o clique foi fora do menu e do botão de toggle
  if (!menu.contains(event.target) && !toggleButton.contains(event.target)) {
    menu.classList.remove('show'); // Fecha o menu
  }
});
