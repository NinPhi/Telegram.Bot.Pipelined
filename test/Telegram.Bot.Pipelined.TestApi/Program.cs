using Microsoft.Extensions.DependencyInjection.Extensions;
using Telegram.Bot.Pipelined.TestApi.Types;
using Telegram.Bot.Pipelined.Extensions;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddBotPipelined(options =>
{
    options.BotToken = "test_token";
    options.WebhookAddress = new Uri("https://example.com/test");
});

var descriptor = new ServiceDescriptor(
    typeof(ITelegramBotClient), new TelegramBotClientMock());
builder.Services.Replace(descriptor);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseBotPipelined();

app.MapControllers();

await app.RunBotWebhookAsync();

app.Run();
