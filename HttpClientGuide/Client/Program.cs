using System.Text.Json;
using System.Text.Json.Serialization;
using HttpClientGuide.Client.IServices;
using HttpClientGuide.Client.Services;
using HttpClientGuide.Client.Shared.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Default", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddMudServices();
builder.Services.AddScoped<IUserService, HttpUserService>();
builder.Services.AddOptions<JsonSerializerOptions>()
    .Configure(options =>
    {
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.PropertyNameCaseInsensitive = false;
        options.ReferenceHandler = ReferenceHandler.Preserve;
        options.MaxDepth = 32;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Default"));
await builder.Build().RunAsync();
