using Telegram.Bot.Pipelined.Helpers;

namespace Telegram.Bot.Pipelined.Routing.ActionConstraints;

public class CommandConstraint : ConstraintBase
{
    private readonly string[] _commands;

    public CommandConstraint(params string[] commands)
    {
        _commands = commands;
    }
    
    public override bool Accept(ActionConstraintContext context)
    {
        var botContext = context.RouteContext.HttpContext.Features.Get<BotContext>()
            ?? throw new ArgumentException(nameof(BotContext));

        var match = CommandMatcher.Match(botContext.MessageText);

        if (match.IsSuccess)
        {
            return MatchingBehavior switch
            {
                MatchingBehavior.MatchAny => _commands.Contains(match.Command),
                MatchingBehavior.MatchNone => !_commands.Contains(match.Command),
                _ => false,
            };
        }

        return false;
    }
}
