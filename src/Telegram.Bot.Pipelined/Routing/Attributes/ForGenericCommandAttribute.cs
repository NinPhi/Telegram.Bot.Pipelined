namespace Telegram.Bot.Pipelined.Routing.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class ForCommandAttribute<TCommand> : ForAttribute where TCommand : struct, Enum
{
    private readonly TCommand[] _commands;

    public ForCommandAttribute(params TCommand[] commands)
    {
        _commands = commands;
    }

    public override IActionConstraint CreateInstance(IServiceProvider services)
    {
        var constraint = new GenericCommandConstraint<TCommand>(_commands)
        {
            MatchingBehavior = MatchingBehavior
        };

        return constraint;
    }
}
