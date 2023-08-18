namespace Telegram.Bot.Pipelined.Routing.ActionConstraints;

public class RoleConstraint : ConstraintBase
{
    private readonly ChatMemberStatus[] _roles;

    public RoleConstraint(params ChatMemberStatus[] roles)
    {
        _roles = roles;
    }

    public override bool Accept(ActionConstraintContext context)
    {
        var botContext = context.RouteContext.HttpContext.Features.Get<BotContext>()
            ?? throw new ArgumentException(nameof(BotContext));
        var chatMember = botContext.ChatMember
            ?? throw new ArgumentException(nameof(botContext.ChatMember));

        return MatchingBehavior switch
        {
            MatchingBehavior.MatchAny => _roles.Contains(chatMember.Status),
            MatchingBehavior.MatchNone => !_roles.Contains(chatMember.Status),
            _ => false,
        };
    }
}
