using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using Telegram.Bot.Pipelined.Configuration;
using Telegram.Bot.Pipelined.Routing.ActionConstraints;
using Telegram.Bot.Pipelined.Test.Utilities.Types;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Pipelined.Test.Utilities.Theory;

public class FailToMatchAnyConstraintTheoryData : TheoryData<ActionConstraintWithContext>
{
    public FailToMatchAnyConstraintTheoryData()
    {
        var constraintContext = new ActionConstraintContext()
        {
            RouteContext = new RouteContext(new DefaultHttpContext())
        };

        var text = "/cancel param1";
        var update = new Update() { InlineQuery = new InlineQuery() { Query = text } };
        var chatMember = new ChatMemberMember();

        var botContext = new BotContext(
            update: update,
            botUser: new User(),
            chatMember: chatMember,
            messageText: text);

        constraintContext.RouteContext.HttpContext.Features.Set(botContext);

        Add(new(new CommandConstraint("start"), constraintContext));
        Add(new(new GenericCommandConstraint<CommandType>(CommandType.Start), constraintContext));
        Add(new(new RoleConstraint(ChatMemberStatus.Creator), constraintContext));
        Add(new(new UpdateConstraint(UpdateType.Message), constraintContext));
    }
}
