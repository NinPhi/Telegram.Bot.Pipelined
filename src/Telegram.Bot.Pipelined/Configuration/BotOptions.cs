namespace Telegram.Bot.Pipelined.Configuration;

public class BotOptions
{
    public string BotToken { get; set; } = string.Empty;
    public string SecretToken { get; set; } = string.Empty;
    public Uri? WebhookAddress { get; set; }
    public BotCommand[] Commands { get; set; } = Array.Empty<BotCommand>();
}
