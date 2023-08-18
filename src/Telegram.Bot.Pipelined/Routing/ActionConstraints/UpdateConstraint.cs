namespace Telegram.Bot.Pipelined.Routing.ActionConstraints;

public class UpdateConstraint : ConstraintBase
{
    private readonly UpdateType[] _types;

    public UpdateConstraint(params UpdateType[] types)
    {
        _types = types;
    }

    public override bool Accept(ActionConstraintContext context)
    {
        var botContext = context.RouteContext.HttpContext.Features.Get<BotContext>()
            ?? throw new ArgumentException(nameof(BotContext));

        return MatchingBehavior switch
        {
            MatchingBehavior.MatchAny => _types.Contains(botContext.Update.Type),
            MatchingBehavior.MatchNone => !_types.Contains(botContext.Update.Type),
            _ => false,
        };
    }
}
