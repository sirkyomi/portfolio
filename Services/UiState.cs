namespace Portfolio.Web.Services;

public sealed class UiState
{
    public bool SidebarOffen { get; private set; }

    public event Action? OnChange;

    public void ToggleSidebar()
    {
        SidebarOffen = !SidebarOffen;
        OnChange?.Invoke();
    }

    public void SchliesseSidebar()
    {
        if (!SidebarOffen) return;
        SidebarOffen = false;
        OnChange?.Invoke();
    }
}
