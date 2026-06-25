using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Portfolio.Web;
using Portfolio.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<ThemeService>();
builder.Services.AddSingleton<VirtualFileSystem>();
builder.Services.AddSingleton<EditorState>();
builder.Services.AddSingleton<PingService>();

await builder.Build().RunAsync();
