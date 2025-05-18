using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using GymTrackerBot.Domain.Interfaces;
using GymTrackerBot.Domain.ResultPattern;
using Microsoft.Extensions.Logging;

namespace GymTrackerBot.Telegram;

public class TelegramBotHandler(
    ITelegramBotClient botClient,
    IGymService gymService,
    ILogger<TelegramBotHandler> logger)
{
    public void StartReceiving()
    {
        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync
        );
    }

    private async Task HandleUpdateAsync(ITelegramBotClient bot, Update update,
        CancellationToken cancellationToken)
    {
        if (update is { Type: UpdateType.Message, Message.Text: not null })
        {
            var message = update.Message;
            var responseText = ProcessUserCommand(message);

            await bot.SendMessage(
                chatId: message.Chat.Id,
                text: responseText,
                cancellationToken: cancellationToken
            );
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError($"Error occurred: {exception.Message}");
        return Task.CompletedTask;
    }

    private string ProcessUserCommand(Message message)
    {
        string text = message.Text.Trim();

        if (text.StartsWith("/start", StringComparison.OrdinalIgnoreCase))
        {
            var parts = text.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            string? note = parts.Length > 1 ? parts[1] : null;

            var result = gymService.StartSession(message.Chat.Id, note);

            if (result.Success)
            {
                return $"✅ Gym session started at {result.Data.StartTime:G}." +
                       (string.IsNullOrWhiteSpace(result.Data.Notes) ? "" : $"\n📝 Note: {result.Data.Notes}");
            }

            return $"❌ {result.Error}";
        }

        if (text.StartsWith("/end", StringComparison.OrdinalIgnoreCase))
        {
            var result = gymService.EndSession(message.Chat.Id);
            if (result.Success)
            {
                var session = result.Data!;
                var durationMinutes = Math.Round((session.EndTime.Value - session.StartTime).TotalMinutes, 2);

                return $"✅ Gym session ended at {session.EndTime:dd-MMM-yy HH:mm:ss}.\n" +
                       $"⏱ Duration: {durationMinutes} minutes.";
            }

            return $"❌ {result.Error}";
        }


        if (text.StartsWith("/isactive", StringComparison.OrdinalIgnoreCase))
        {
            var result = gymService.IsSessionActive(message.Chat.Id);
            return result.Data
                ? "✅ A gym session is currently active."
                : "❌ No active gym session found.";
        }

        return "❓ Unknown command. Use /start, /end, or /isactive.";
    }
}