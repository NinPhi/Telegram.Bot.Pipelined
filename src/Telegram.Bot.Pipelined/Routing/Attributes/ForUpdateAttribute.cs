namespace Telegram.Bot.Pipelined.Routing.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class ForUpdateAttribute : ForAttribute
{
    private readonly UpdateType[] _updateTypes;

    public ForUpdateAttribute(params UpdateType[] updateTypes)
    {
        _updateTypes = updateTypes;
    }

    public override IActionConstraint CreateInstance(IServiceProvider services)
    {
        var constraint = new UpdateConstraint(_updateTypes)
        {
            MatchingBehavior = MatchingBehavior
        };

        return constraint;
    }
}
