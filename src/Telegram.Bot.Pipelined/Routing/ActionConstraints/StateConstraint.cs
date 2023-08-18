namespace Telegram.Bot.Pipelined.Routing.ActionConstraints;

public class StateConstraint : ConstraintBase
{
    private readonly string[] _states;

    public StateConstraint(params string[] states)
    {
        _states = states;
    }

    public override bool Accept(ActionConstraintContext context)
    {
        return true;
    }
}
