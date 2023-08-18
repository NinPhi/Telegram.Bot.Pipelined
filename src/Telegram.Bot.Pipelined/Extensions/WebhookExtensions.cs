namespace Telegram.Bot.Pipelined.Extensions;

public static class WebhookExtensions
{
    public static async Task RunBotWebhookAsync(this IApplicationBuilder app)
    {
        var options = app.ApplicationServices.GetRequiredService<BotOptions>();

        ArgumentException.ThrowIfNullOrEmpty(options.WebhookAddress?.ToString(), nameof(options.WebhookAddress));

        var botClient = app.ApplicationServices.GetRequiredService<ITelegramBotClient>();

        //await botClient.DeleteMyCommandsAsync();
        //await botClient.SetMyCommandsAsync(options.Commands);

        //await botClient.DeleteWebhookAsync();
        //await botClient.SetWebhookAsync(
        //    url: options.WebhookAddress.ToString(),
        //    secretToken: options.SecretToken,
        //    dropPendingUpdates: true);
    }
}
