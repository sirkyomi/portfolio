using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public sealed class EditorState
{
    private readonly List<VirtualFile> _offen = [];

    public IReadOnlyList<VirtualFile> Offen => _offen;
    public VirtualFile? Aktiv { get; private set; }

    public event Action? OnChange;

    public void Oeffnen(VirtualFile datei)
    {
        if (!_offen.Contains(datei)) _offen.Add(datei);
        Aktiv = datei;
        OnChange?.Invoke();
    }

    public void Aktivieren(VirtualFile datei)
    {
        if (!_offen.Contains(datei)) return;
        Aktiv = datei;
        OnChange?.Invoke();
    }

    public void Schliessen(VirtualFile datei)
    {
        var index = _offen.IndexOf(datei);
        if (index < 0) return;

        _offen.RemoveAt(index);

        if (Aktiv == datei)
            Aktiv = _offen.Count == 0 ? null : _offen[Math.Min(index, _offen.Count - 1)];

        OnChange?.Invoke();
    }
}
