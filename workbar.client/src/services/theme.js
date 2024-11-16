export function applyTheme(themeName) {
  const themePath = `/themes/${themeName}/theme.css`;
  let linkElement = document.getElementById('themeStylesheet');

  if (linkElement) {
    linkElement.href = themePath;
  } else {
    linkElement = document.createElement('link');
    linkElement.id = 'themeStylesheet';
    linkElement.rel = 'stylesheet';
    linkElement.href = themePath;
    document.head.appendChild(linkElement);
  }

  localStorage.setItem('selectedTheme', themeName);
}

export function loadSavedTheme() {
  const savedTheme = localStorage.getItem('selectedTheme');
  if (savedTheme) {
    applyTheme(savedTheme);
  }
}
