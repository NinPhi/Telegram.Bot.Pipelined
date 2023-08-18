namespace Telegram.Bot.Pipelined.Routing.ActionConstraints.Abstractions;

public abstract class ConstraintBase : IActionConstraint
{
    public int Order { get; internal init; } = 0;

    public MatchingBehavior MatchingBehavior { get; internal init; } = MatchingBehavior.MatchAny;

    public abstract bool Accept(ActionConstraintContext context);
}
