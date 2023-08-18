using Telegram.Bot.Pipelined.Helpers;

namespace Telegram.Bot.Pipelined.Test.UnitTests.Helpers;

public class CommandMatcherTests
{
    [Fact]
    public void Match_CommandWithParameters_CommandMatches()
    {
        // Arrange
        var text = "/start param1 param2";

        // Act
        var match = CommandMatcher.Match(text);

        // Assert
        Assert.True(match.IsSuccess);
        Assert.NotEmpty(match.Command);
        Assert.NotEmpty(match.Parameters);
        Assert.Equal("start", match.Command);
        Assert.Equal("param1 param2", match.Parameters);
    }

    [Fact]
    public void Match_CommandWithoutParameters_CommandMatches()
    {
        // Arrange
        var text = "/start";

        // Act
        var match = CommandMatcher.Match(text);

        // Assert
        Assert.True(match.IsSuccess);
        Assert.NotEmpty(match.Command);
        Assert.Equal("start", match.Command);
        Assert.Equal(string.Empty, match.Parameters);
    }
}
