using Telegram.Bot.Pipelined.Helpers;

namespace Telegram.Bot.Pipelined.Routing.ActionConstraints;

public class GenericCommandConstraint<TCommand> : ConstraintBase where TCommand : struct, Enum
{
    private readonly TCommand[] _commands;

    public GenericCommandConstraint(params TCommand[] commands)
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
            var result = Enum.TryParse<TCommand>(match.Command, true, out var command);

            return MatchingBehavior switch
            {
                MatchingBehavior.MatchAny => result && _commands.Contains(command),
                MatchingBehavior.MatchNone => !result || !_commands.Contains(command),
                _ => false,
            };
        }

        return false;
    }
}
