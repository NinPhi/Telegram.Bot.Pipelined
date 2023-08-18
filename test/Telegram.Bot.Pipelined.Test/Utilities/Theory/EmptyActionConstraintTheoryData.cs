using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Telegram.Bot.Pipelined.Routing.ActionConstraints;
using Telegram.Bot.Pipelined.Test.Utilities.Types;

namespace Telegram.Bot.Pipelined.Test.Utilities.Theory;

public class EmptyActionConstraintTheoryData : TheoryData<IActionConstraint>
{
    public EmptyActionConstraintTheoryData()
    {
        Add(new CommandConstraint());   
        Add(new GenericCommandConstraint<CommandType>());   
        Add(new RoleConstraint());   
        Add(new UpdateConstraint());   
    }
}
