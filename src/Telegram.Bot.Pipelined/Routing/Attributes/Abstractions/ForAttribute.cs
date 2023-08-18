namespace Telegram.Bot.Pipelined.Routing.Attributes.Abstractions;

public abstract class ForAttribute : Attribute, IActionConstraintFactory
{
    public MatchingBehavior MatchingBehavior { get; init; } = MatchingBehavior.MatchAny;

    public bool IsReusable => true;

    public abstract IActionConstraint CreateInstance(IServiceProvider services);
}
