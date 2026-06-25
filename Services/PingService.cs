namespace Portfolio.Web.Services;

public sealed class PingService : IDisposable
{
    private readonly System.Timers.Timer _timer;
    private readonly Random _rng = new();

    public string Branch => "main";
    public string Pipeline => "erfolgreich";
    public int LatenzMs { get; private set; } = 23;

    public event Action? OnTick;

    public PingService()
    {
        _timer = new System.Timers.Timer(3000) { AutoReset = true };
        _timer.Elapsed += (_, _) =>
        {
            LatenzMs = _rng.Next(12, 41);
            OnTick?.Invoke();
        };
        _timer.Start();
    }

    public void Dispose() => _timer.Dispose();
}
