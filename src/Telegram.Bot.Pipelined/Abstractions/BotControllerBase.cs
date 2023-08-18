namespace Telegram.Bot.Pipelined.Abstractions;

public abstract class BotControllerBase : ControllerBase
{
    private BotContext? _botContext;

    private BotContext BotContext
    {
        get
        {
            _botContext ??= HttpContext.Features.Get<BotContext>() ??
                throw new ArgumentException(nameof(BotContext));
            return _botContext!;
        }
    }

    public Update Update => BotContext.Update;
    public User BotUser => BotContext.BotUser;
    public Chat? Chat => BotContext.Chat;
    public User? From => BotContext.User;
}
