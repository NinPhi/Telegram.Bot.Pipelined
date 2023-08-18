namespace Telegram.Bot.Pipelined.Helpers;

internal readonly struct CommandMatch
{
    internal string Command { get; } = string.Empty;

    internal string Parameters { get; } = string.Empty;

    internal bool IsSuccess { get; } = false;

    public CommandMatch() { }

    public CommandMatch(string name, string parameters)
    {
        Command = name;
        Parameters = parameters;
        IsSuccess = true;
    }
}
