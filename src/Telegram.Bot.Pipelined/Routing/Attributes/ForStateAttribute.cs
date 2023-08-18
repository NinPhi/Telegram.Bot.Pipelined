namespace Telegram.Bot.Pipelined.Routing.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class ForStateAttribute : ForAttribute
{
    private readonly string[] _states;

    public ForStateAttribute(params string[] states)
    {
        _states = states;
    }

    public override IActionConstraint CreateInstance(IServiceProvider services)
    {
        var constraint = new StateConstraint(_states)
        {
            MatchingBehavior = MatchingBehavior
        };

        return constraint;
    }
}
