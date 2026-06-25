namespace Portfolio.Web.Models.Werdegang;

public sealed record WerdegangEintrag(
    string Von,
    string? Bis,
    string Typ,
    string Titel,
    string Institution,
    string? Ort = null,
    string? Highlight = null,
    string? Beschreibung = null,
    bool Aktuell = false,
    Abschluss? Abschluss = null);

public sealed record Abschluss(
    string? Note = null,
    int? IhkPunkte = null,
    string? Berufsschule = null);
