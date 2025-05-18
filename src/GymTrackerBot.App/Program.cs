using Telegram.Bot;
using GymTrackerBot.Telegram;
using GymTrackerBot.Domain.Interfaces;
using GymTrackerBot.Infrastructure.Repositories;
using GymTrackerBot.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Load configuration files (base and environment-specific)
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Load user secrets in development (Optional)
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Get the Telegram Bot API key from configuration
var botApiKey = builder.Configuration["Telegram:Token"];
if (string.IsNullOrEmpty(botApiKey))
{
    throw new InvalidOperationException(
        "Telegram Bot API key is missing in configuration. Please add it to 'appsettings.json' or UserSecrets.");
}

// Register services to Dependency Injection container
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(botApiKey));
builder.Services.AddSingleton<TelegramBotHandler>();

// Register application-specific services
builder.Services.AddSingleton<IGymRepository, GymRepositoryLocalTest>();
builder.Services.AddSingleton<IGymService, GymService>();

// Build the application
var app = builder.Build();

// Configure and run the Telegram Bot
var botHandler = app.Services.GetRequiredService<TelegramBotHandler>();
botHandler.StartReceiving();

Console.WriteLine("Telegram bot is running. Press Ctrl+C to stop.");

// Run the application
await app.RunAsync();