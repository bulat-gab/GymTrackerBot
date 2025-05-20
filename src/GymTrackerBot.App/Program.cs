using Telegram.Bot;
using GymTrackerBot.Telegram;
using GymTrackerBot.Domain.Interfaces;
using GymTrackerBot.Infrastructure.Repositories;
using GymTrackerBot.Infrastructure.Services;
using GymTrackerBot.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Load configuration files
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Load user secrets in development
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Get the Telegram Bot API key from configuration
var botApiKey = builder.Configuration["Telegram:Token"];
if (string.IsNullOrEmpty(botApiKey))
{
    throw new InvalidOperationException("Telegram Bot API key is missing in configuration.");
}

// Get connection string from config
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("PostgreSQL connection string is missing in configuration.");
}

// Register EF Core PostgreSQL DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register services
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(botApiKey));
builder.Services.AddScoped<TelegramBotHandler>();
builder.Services.AddScoped<IGymRepository, GymRepository>();
builder.Services.AddScoped<IGymService, GymService>();

var app = builder.Build();

// Run Telegram bot
using (var scope = app.Services.CreateScope())
{
    var handler = scope.ServiceProvider.GetRequiredService<TelegramBotHandler>();
    handler.StartReceiving();
}

Console.WriteLine("Telegram bot is running. Press Ctrl+C to stop.");

// Run the app
await app.RunAsync();