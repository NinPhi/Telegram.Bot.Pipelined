namespace Telegram.Bot.Pipelined.Configuration;

public class BotContext
{
    public Update Update { get; }
    public User BotUser { get; }
    public User? User { get; }
    public Chat? Chat { get; }
    public ChatMember? ChatMember { get; }
    public string? MessageText { get; }

    public BotContext(
        Update update,
        User botUser,
        User? user = null,
        Chat? chat = null,
        ChatMember? chatMember = null,
        string? messageText = null)
    {
        Update = update;
        BotUser = botUser;
        User = user;
        Chat = chat;
        ChatMember = chatMember;
        MessageText = messageText;
    }
}