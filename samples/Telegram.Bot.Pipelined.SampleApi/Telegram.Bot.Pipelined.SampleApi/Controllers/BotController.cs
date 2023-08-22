using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Pipelined.Abstractions;
using Telegram.Bot.Pipelined.Routing.Attributes;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Pipelined.SampleApi.Controllers;

[Route("bot")]
[ApiController]
public class BotController : BotControllerBase
{
    private readonly ITelegramBotClient _botClient;

    public BotController(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    [ForUpdate(UpdateType.Message, UpdateType.EditedMessage)]
    public async Task Default()
    {
        await _botClient.SendTextMessageAsync(
            chatId: Chat!.Id,
            text: "Default handler triggered!");
    }
}
