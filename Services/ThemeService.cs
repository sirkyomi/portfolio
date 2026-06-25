using Microsoft.JSInterop;

namespace Portfolio.Web.Services;

public sealed class ThemeService(IJSRuntime js)
{
    public bool IstDunkel { get; private set; }
    public bool Initialisiert { get; private set; }

    public event Action? OnChange;

    public async Task InitAsync()
    {
        if (Initialisiert) return;

        var gespeichert = await js.InvokeAsync<string?>("themeInterop.get");
        IstDunkel = gespeichert is "dark"
            || (gespeichert is null && await js.InvokeAsync<bool>("themeInterop.prefersDark"));

        await js.InvokeVoidAsync("themeInterop.apply", IstDunkel ? "dark" : "light");
        Initialisiert = true;
        OnChange?.Invoke();
    }

    public async Task UmschaltenAsync()
    {
        IstDunkel = !IstDunkel;
        await js.InvokeVoidAsync("themeInterop.set", IstDunkel ? "dark" : "light");
        OnChange?.Invoke();
    }
}
