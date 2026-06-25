// Annotation-Tooltip, der dem Cursor folgt.
// Ein einziges Tooltip-Element wird per Event-Delegation gesteuert, damit es
// auch nach Blazor-Re-Renders funktioniert. Hover-Zeilen tragen data-anno.
(function () {
    let tip = null;

    function ensure() {
        if (tip) return tip;
        tip = document.createElement('div');
        tip.id = 'anno-tip';
        tip.innerHTML =
            '<div class="anno-titel"></div><div class="anno-text"></div>';
        document.body.appendChild(tip);
        return tip;
    }

    function position(x, y) {
        const t = ensure();
        const pad = 16;
        const w = t.offsetWidth;
        const h = t.offsetHeight;
        let left = x + pad;
        let top = y + pad;
        if (left + w > window.innerWidth - 8) left = x - w - pad;   // links statt rechts
        if (top + h > window.innerHeight - 8) top = y - h - pad;    // oben statt unten
        t.style.left = left + 'px';
        t.style.top = top + 'px';
    }

    document.addEventListener('mouseover', function (e) {
        const row = e.target.closest('[data-anno]');
        if (!row) return;
        const t = ensure();
        t.querySelector('.anno-titel').textContent = row.dataset.annoTitel || '';
        t.querySelector('.anno-text').textContent = row.dataset.annoText || '';
        t.classList.add('sichtbar');
        position(e.clientX, e.clientY);
    });

    document.addEventListener('mousemove', function (e) {
        if (!tip || !tip.classList.contains('sichtbar')) return;
        if (!e.target.closest('[data-anno]')) return;
        position(e.clientX, e.clientY);
    });

    document.addEventListener('mouseout', function (e) {
        const row = e.target.closest('[data-anno]');
        if (row && tip) tip.classList.remove('sichtbar');
    });
})();
