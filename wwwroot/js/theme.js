// Theme-Interop: liest/schreibt Light-Dark-Status, setzt .dark auf <html>.
window.themeInterop = {
    get: () => localStorage.getItem('theme'),

    prefersDark: () => window.matchMedia('(prefers-color-scheme: dark)').matches,

    apply: (theme) =>
        document.documentElement.classList.toggle('dark', theme === 'dark'),

    set: (theme) => {
        localStorage.setItem('theme', theme);
        document.documentElement.classList.toggle('dark', theme === 'dark');
    },
};
