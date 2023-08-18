using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace Telegram.Bot.Pipelined.Middleware;

public class BotPipelinedMiddleware : IMiddleware
{
    private readonly Uri _webhookAddress;

    public BotPipelinedMiddleware(BotOptions botOptions)
    {
        ArgumentNullException.ThrowIfNull(botOptions, nameof(botOptions));
        ArgumentException.ThrowIfNullOrEmpty(botOptions.WebhookAddress?.ToString(), nameof(botOptions.WebhookAddress));

        _webhookAddress = botOptions.WebhookAddress;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        bool isBotWebhookRequest = context.Request.Path.StartsWithSegments(
            _webhookAddress.LocalPath, StringComparison.OrdinalIgnoreCase);

        if (!isBotWebhookRequest)
        {
            await next(context);
            return;
        }

        bool isHttpPostRequest = context.Request.Method == HttpMethod.Post.Method;
        if (!isHttpPostRequest)
        {
            context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
            return;
        }

        bool hasJsonContentType = context.Request.HasJsonContentType();
        if (!hasJsonContentType)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }

        var update = await ExtractUpdateAsync(context);
        if (update == null)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }

        await SetBotContextFeatureAsync(context, update);

        //pass control along the pipeline
        await next(context);

        if (context.Response.StatusCode == StatusCodes.Status404NotFound)
        {
            context.Response.StatusCode = StatusCodes.Status204NoContent;
        }
    }

    private static async Task<Update?> ExtractUpdateAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        var binaryContent = await context.Request.BodyReader.ReadAsync();
        context.Request.Body.Seek(0, SeekOrigin.Begin);

        var jsonContent = Encoding.UTF8.GetString(binaryContent.Buffer);
        var update = JsonConvert.DeserializeObject<Update>(jsonContent);

        return update;
    }

    private static async Task SetBotContextFeatureAsync(HttpContext context, Update update)
    {
        var botClient = context.RequestServices.GetRequiredService<ITelegramBotClient>();
        var botUser = await botClient.GetMeAsync();

        var chatId = GetChatId(update);
        var user = GetUser(update);

        Chat? chat = null;
        ChatMember? chatMember = null;

        if (chatId != 0)
        {
            chat = await botClient.GetChatAsync(new(chatId));

            if (user != null)
            {
                chatMember = await botClient.GetChatMemberAsync(new(chatId), user.Id);
            }
        }

        var messageText = GetMessageText(update);

        var feature = new BotContext(
            update,
            botUser,
            user,
            chat,
            chatMember,
            messageText);

        context.Features.Set(feature);
    }

    private static User? GetUser(Update update)
    {
        User? user = null;

        switch (update.Type)
        {
            case UpdateType.Message: user = update.Message?.From; break;
            case UpdateType.EditedMessage: user = update.EditedMessage?.From; break;
            case UpdateType.InlineQuery: user = update.InlineQuery?.From; break;
            case UpdateType.ChosenInlineResult: user = update.ChosenInlineResult?.From; break;
            case UpdateType.CallbackQuery: user = update.CallbackQuery?.From; break;
            case UpdateType.MyChatMember: user = update.MyChatMember?.From; break;
            case UpdateType.ChatMember: user = update.ChatMember?.From; break;
            case UpdateType.ChatJoinRequest: user = update.ChatJoinRequest?.From; break;
            case UpdateType.ShippingQuery: user = update.ShippingQuery?.From; break;
            case UpdateType.PreCheckoutQuery: user = update.PreCheckoutQuery?.From; break;
            case UpdateType.PollAnswer: user = update.PollAnswer?.User; break;

            case UpdateType.ChannelPost:
            case UpdateType.EditedChannelPost:
            case UpdateType.Poll:
            case UpdateType.Unknown:
            default: break;
        }

        return user;
    }

    private static long GetChatId(Update update)
    {
        long chatId = 0;

        switch (update.Type)
        {
            case UpdateType.Message: chatId = update.Message?.Chat.Id ?? 0; break;
            case UpdateType.EditedMessage: chatId = update.EditedMessage?.Chat.Id ?? 0; break;
            case UpdateType.CallbackQuery: long.TryParse(update.CallbackQuery?.ChatInstance, out chatId); break;
            case UpdateType.MyChatMember: chatId = update.MyChatMember?.Chat.Id ?? 0; break;
            case UpdateType.ChatMember: chatId = update.ChatMember?.Chat.Id ?? 0; break;
            case UpdateType.ChatJoinRequest: chatId = update.ChatJoinRequest?.Chat.Id ?? 0; break;

            case UpdateType.InlineQuery:
            case UpdateType.ChosenInlineResult:
            case UpdateType.ShippingQuery:
            case UpdateType.PreCheckoutQuery:
            case UpdateType.ChannelPost:
            case UpdateType.EditedChannelPost:
            case UpdateType.Poll:
            case UpdateType.PollAnswer:
            case UpdateType.Unknown:
            default: break;
        }

        return chatId;
    }

    private static string? GetMessageText(Update update)
    {
        string? messageText = null;

        switch (update.Type)
        {
            case UpdateType.Message: messageText = update.Message?.Text; break;
            case UpdateType.EditedMessage: messageText = update.EditedMessage?.Text; break;
            case UpdateType.CallbackQuery: messageText = update.CallbackQuery?.Message?.Text; break;
            case UpdateType.InlineQuery: messageText = update.InlineQuery?.Query; break;
            case UpdateType.ChosenInlineResult: messageText = update.ChosenInlineResult?.Query; break;
            case UpdateType.ChannelPost: messageText = update.ChannelPost?.Text; break;
            case UpdateType.EditedChannelPost: messageText = update.EditedChannelPost?.Text; break;

            case UpdateType.MyChatMember:
            case UpdateType.ChatMember:
            case UpdateType.ChatJoinRequest:
            case UpdateType.ShippingQuery:
            case UpdateType.PreCheckoutQuery:
            case UpdateType.Poll:
            case UpdateType.PollAnswer:
            case UpdateType.Unknown:
            default: break;
        }

        return messageText;
    }
}
