using GestaoEncomendasExercicio.Client;
using GestaoEncomendasExercicio.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5147/") });

builder.Services.AddScoped<OrderService>(); // Register Service

await builder.Build().RunAsync(); 