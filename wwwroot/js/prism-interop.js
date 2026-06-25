// Prism-Interop: highlightet Quelltext ZEILENWEISE.
// Rückgabe = Array aus HTML-Strings (eine Zeile pro Eintrag), damit Blazor
// pro Zeile eine Hover-Zone für Tooltips rendern kann.
window.prismInterop = {
    highlightLines: function (code, sprache) {
        const grammar = (window.Prism && Prism.languages[sprache])
            || (window.Prism && Prism.languages.clike);

        return code.split('\n').map(function (zeile) {
            if (zeile.length === 0) return '&nbsp;';
            if (!grammar) return escapeHtml(zeile);
            return Prism.highlight(zeile, grammar, sprache);
        });
    },
};

function escapeHtml(s) {
    return s.replace(/[&<>"']/g, function (c) {
        return ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' })[c];
    });
}
