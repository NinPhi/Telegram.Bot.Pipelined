using Telegram.Bot.Pipelined.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var botToken = builder.Configuration["Telegram:BotToken"]
    ?? throw new ArgumentException("BotToken");
var webhookAddress = builder.Configuration["Telegram:WebhookAddress"]
    ?? throw new ArgumentException("WebhookAddress");

builder.Services.AddBotPipelined(opts =>
{
    opts.BotToken = botToken;
    opts.WebhookAddress = new Uri(webhookAddress);
});

var app = builder.Build();

app.UseAuthorization();

app.UseBotPipelined();

app.MapControllers();

await app.RunBotWebhookAsync();

app.Run();
