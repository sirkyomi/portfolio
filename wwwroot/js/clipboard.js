// Clipboard-Interop: kopiert Text (z. B. E-Mail-Adresse) in die Zwischenablage.
window.clipboardInterop = {
    copy: async function (text) {
        try {
            await navigator.clipboard.writeText(text);
            return true;
        } catch {
            // Fallback für Kontexte ohne Clipboard-API.
            const ta = document.createElement('textarea');
            ta.value = text;
            ta.style.position = 'fixed';
            ta.style.opacity = '0';
            document.body.appendChild(ta);
            ta.select();
            const ok = document.execCommand('copy');
            document.body.removeChild(ta);
            return ok;
        }
    },
};
