using GymTrackerBot.Domain.Interfaces;
using GymTrackerBot.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GymTrackerBot.PWA;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add our services
// builder.Services.AddScoped<VersionMigrationService>();
builder.Services.AddScoped<IGymServiceStandalone, LocalStorageGymSessionService>();


await builder.Build().RunAsync();