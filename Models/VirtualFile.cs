namespace Portfolio.Web.Models;

public sealed class VirtualFile
{
    public required string Pfad { get; init; }
    public required string Name { get; init; }
    public required string Titel { get; init; }
    public string? Kontext { get; init; }
    public required string Sprache { get; init; }
    public EditorAnsicht Ansicht { get; init; } = EditorAnsicht.Code;
    public required string Inhalt { get; init; }
    public IReadOnlyList<CodeAnnotation> Annotationen { get; init; } = [];

    public int ZeilenAnzahl => Inhalt.Split('\n').Length;
}
