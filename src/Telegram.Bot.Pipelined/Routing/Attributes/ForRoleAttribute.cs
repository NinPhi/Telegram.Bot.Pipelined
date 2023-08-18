namespace Telegram.Bot.Pipelined.Routing.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class ForRoleAttribute : ForAttribute
{
    private readonly ChatMemberStatus[] _roles;

    public ForRoleAttribute(params ChatMemberStatus[] roles)
    {
        _roles = roles;
    }

    public override IActionConstraint CreateInstance(IServiceProvider services)
    {
        var constraint = new RoleConstraint(_roles)
        {
            MatchingBehavior = MatchingBehavior
        };

        return constraint;
    }
}
