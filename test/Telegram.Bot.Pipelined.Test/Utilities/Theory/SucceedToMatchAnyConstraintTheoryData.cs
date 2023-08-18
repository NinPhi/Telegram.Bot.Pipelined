using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using Telegram.Bot.Pipelined.Configuration;
using Telegram.Bot.Pipelined.Routing.ActionConstraints;
using Telegram.Bot.Pipelined.Test.Utilities.Types;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Pipelined.Test.Utilities.Theory;

public class SucceedToMatchAnyConstraintTheoryData : TheoryData<ActionConstraintWithContext>
{
    public SucceedToMatchAnyConstraintTheoryData()
    {
        var constraintContext = new ActionConstraintContext()
        {
            RouteContext = new RouteContext(new DefaultHttpContext())
        };

        var text = "/start param1";
        var update = new Update() { Message = new Message() { Text = text } };
        var chatMember = new ChatMemberOwner();

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
