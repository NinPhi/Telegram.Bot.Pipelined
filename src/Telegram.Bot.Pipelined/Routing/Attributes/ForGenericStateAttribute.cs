namespace Telegram.Bot.Pipelined.Routing.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class ForStateAttribute<TState> : ForAttribute where TState : struct, Enum
{
    private readonly TState[] _states;

    public ForStateAttribute(params TState[] states)
    {
        _states = states;
    }

    public override IActionConstraint CreateInstance(IServiceProvider services)
    {
        var constraint = new GenericStateConstraint<TState>(_states)
        {
            MatchingBehavior = MatchingBehavior
        };

        return constraint;
    }
}
