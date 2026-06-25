# Portfolio – Elias-Constantin Regber

Mein digitales Portfolio, gebaut als kleine IDE im Browser. Statt einer klassischen
Lebenslauf-Seite klickt man sich hier durch „Dateien" und sieht mein Profil so, wie
ich tatsächlich arbeite. Eine geführte Tour nimmt einen dabei an die Hand.

## Was drinsteckt

- **Blazor WebAssembly (.NET 10)** – komplett statisch, kein Backend
- **Tailwind CSS** mit eigenem „Cosy Wood"-Theme (Light & Dark)
- **Font Awesome Free**, lokal eingebunden – kein CDN nötig
- **Prism** fürs Syntax-Highlighting der gezeigten Dateien
- Geführte Tour: Hallo → Werdegang → Teamleitung → Ausbildung → Tech-Stack → DevOps

## Lokal starten

```bash
dotnet run
```

Danach die angezeigte URL im Browser öffnen.

## Deployment

Jeder Push auf `main` baut und veröffentlicht die Seite automatisch über GitHub
Actions auf GitHub Pages – siehe `.github/workflows/deploy.yml`.

Die Seite läuft unter einer eigenen Domain, daher bleibt `<base href="/">`.
Einmalig im Repository:

1. **Settings → Pages → Source: GitHub Actions**
2. **Settings → Pages → Custom domain**: die Domain eintragen
3. Beim DNS-Anbieter den passenden Eintrag setzen:
   - Apex-Domain (`example.de`): vier `A`-Records auf die GitHub-Pages-IPs
     (`185.199.108.153`, `.109.153`, `.110.153`, `.111.153`)
   - oder Subdomain (`www.example.de`): `CNAME` auf `<user>.github.io`

## Profilbild

Eine quadratische `wwwroot/img/profil.jpg` ablegen (mind. 256×256). Fehlt sie, zeigt
der Start automatisch meine Initialen.

## Kontakt

- GitHub: <https://github.com/sirkyomi>
- LinkedIn: <https://www.linkedin.com/in/elias-constantin-regber-aa7513261/>
- E-Mail: mail@eliasregber.de
