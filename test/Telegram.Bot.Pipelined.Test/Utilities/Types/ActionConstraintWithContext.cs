using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Telegram.Bot.Pipelined.Test.Utilities.Types;

public record ActionConstraintWithContext(
    IActionConstraint ActionConstraint, ActionConstraintContext ActionContext);
