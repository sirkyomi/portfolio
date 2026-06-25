/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: 'class',
  content: [
    './**/*.razor',
    './**/*.cs',
    './wwwroot/index.html',
  ],
  theme: {
    extend: {
      colors: {
        bg:       'rgb(var(--c-bg) / <alpha-value>)',
        surface:  'rgb(var(--c-surface) / <alpha-value>)',
        elevated: 'rgb(var(--c-elevated) / <alpha-value>)',
        border:   'rgb(var(--c-border) / <alpha-value>)',
        text:     'rgb(var(--c-text) / <alpha-value>)',
        muted:    'rgb(var(--c-muted) / <alpha-value>)',
        wood: {
          DEFAULT: 'rgb(var(--c-wood) / <alpha-value>)',
          soft:    'rgb(var(--c-wood-soft) / <alpha-value>)',
        },
        ok:   'rgb(var(--c-ok) / <alpha-value>)',
        warn: 'rgb(var(--c-warn) / <alpha-value>)',
        info: 'rgb(var(--c-info) / <alpha-value>)',
      },
      fontFamily: {
        sans:  ['Inter', 'system-ui', 'sans-serif'],
        serif: ['Inter', 'system-ui', 'sans-serif'],
        mono:  ['"JetBrains Mono"', 'ui-monospace', 'monospace'],
      },
      borderRadius: { xl: '0.9rem', '2xl': '1.25rem' },
      boxShadow: {
        cosy: '0 8px 30px -12px rgb(0 0 0 / 0.45)',
        'cosy-light': '0 10px 30px -18px rgb(120 90 60 / 0.30)',
      },
    },
  },
  plugins: [],
};
