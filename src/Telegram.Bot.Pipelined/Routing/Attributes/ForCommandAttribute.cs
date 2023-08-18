namespace Telegram.Bot.Pipelined.Routing.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class ForCommandAttribute : ForAttribute
{
    private readonly string[] _commands;

    public ForCommandAttribute(params string[] commands)
    {
        _commands = commands;
    }

    public override IActionConstraint CreateInstance(IServiceProvider services)
    {
        var constraint = new CommandConstraint(_commands)
        {
            MatchingBehavior = MatchingBehavior
        };

        return constraint;
    }
}
