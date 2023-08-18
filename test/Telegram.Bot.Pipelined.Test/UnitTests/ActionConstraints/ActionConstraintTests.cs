using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using Telegram.Bot.Pipelined.Test.Utilities.Theory;
using Telegram.Bot.Pipelined.Test.Utilities.Types;

namespace Telegram.Bot.Pipelined.Test.UnitTests.ActionConstraints;

public class ActionConstraintTests
{
    [Theory]
    [ClassData(typeof(EmptyActionConstraintTheoryData))]
    public void Accept_MissingBotContextFeature_ThrowsArgumentException(
        IActionConstraint sut)
    {
        // Arrange
        var stub = new ActionConstraintContext()
        {
            RouteContext = new RouteContext(new DefaultHttpContext())
        };

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => sut.Accept(stub));
    }

    [Theory]
    [ClassData(typeof(SucceedToMatchAnyConstraintTheoryData))]
    public void Accept_MatchAny_ShouldSucceed(
        ActionConstraintWithContext constraint)
    {
        // Arrange
        IActionConstraint sut = constraint.ActionConstraint;
        ActionConstraintContext stub = constraint.ActionContext;

        // Act
        var result = sut.Accept(stub);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [ClassData(typeof(FailToMatchAnyConstraintTheoryData))]
    public void Accept_MatchAny_ShouldFail(
        ActionConstraintWithContext constraint)
    {
        // Arrange
        IActionConstraint sut = constraint.ActionConstraint;
        ActionConstraintContext stub = constraint.ActionContext;

        // Act
        var result = sut.Accept(stub);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [ClassData(typeof(SucceedToMatchNoneConstraintTheoryData))]
    public void Accept_MatchNone_ShouldSucceed(
        ActionConstraintWithContext constraint)
    {
        // Arrange
        IActionConstraint sut = constraint.ActionConstraint;
        ActionConstraintContext stub = constraint.ActionContext;

        // Act
        var result = sut.Accept(stub);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [ClassData(typeof(FailToMatchNoneConstraintTheoryData))]
    public void Accept_MatchNone_ShouldFail(
        ActionConstraintWithContext constraint)
    {
        // Arrange
        IActionConstraint sut = constraint.ActionConstraint;
        ActionConstraintContext stub = constraint.ActionContext;

        // Act
        var result = sut.Accept(stub);

        // Assert
        Assert.False(result);
    }
}
