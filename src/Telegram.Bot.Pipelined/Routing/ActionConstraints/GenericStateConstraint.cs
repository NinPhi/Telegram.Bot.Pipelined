namespace Telegram.Bot.Pipelined.Routing.ActionConstraints;

public class GenericStateConstraint<TState> : ConstraintBase where TState : struct, Enum
{
    private readonly TState[] _states;

    public GenericStateConstraint(params TState[] states)
    {
        _states = states;
    }

    public override bool Accept(ActionConstraintContext context)
    {
        return true;
    }
}
