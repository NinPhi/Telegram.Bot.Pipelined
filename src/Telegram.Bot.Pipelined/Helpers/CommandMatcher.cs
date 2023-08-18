using System.Text.RegularExpressions;

namespace Telegram.Bot.Pipelined.Helpers;

internal partial class CommandMatcher
{
    internal static CommandMatch Match(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return new CommandMatch();

        var match = CommandRegex().Match(text);
        if (match.Success)
        {
            string command = match.Groups["command"].Value;
            string parameters = match.Groups["parameters"].Value;
            return new CommandMatch(command, parameters);
        }

        return new CommandMatch();
    }

    [GeneratedRegex(
        "^\\/(?<command>\\w*)(?:$|\\s(?<parameters>.*))",
        RegexOptions.IgnoreCase | RegexOptions.Compiled)]
    private static partial Regex CommandRegex();
}
