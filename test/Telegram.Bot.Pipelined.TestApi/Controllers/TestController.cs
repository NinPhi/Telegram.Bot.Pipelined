using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Pipelined.Abstractions;
using Telegram.Bot.Pipelined.Routing.Attributes;
using Telegram.Bot.Pipelined.Test.Utilities.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Pipelined.TestApi.Controllers;

[Route("test")]
[ApiController]
public class TestController : BotControllerBase
{
    [ForCommand<CommandType>(CommandType.Start)]
    public IActionResult Start() => Ok(nameof(Start));

    [ForCommand<CommandType>(CommandType.Cancel)]
    public IActionResult Cancel() => Ok(nameof(Cancel));

    [ForUpdate(UpdateType.InlineQuery)]
    public IActionResult Inline() => Ok(nameof(Inline));

    public IActionResult Default() => Ok(nameof(Default));
}