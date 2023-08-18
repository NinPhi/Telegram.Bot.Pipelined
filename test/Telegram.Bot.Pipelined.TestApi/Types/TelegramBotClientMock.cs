using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;

namespace Telegram.Bot.Pipelined.TestApi.Types;

public class TelegramBotClientMock : ITelegramBotClient
{
    public bool LocalBotServer => false;

    public long? BotId => 0;

    public TimeSpan Timeout { get; set; }
    public IExceptionParser ExceptionsParser { get; set; }

    public event AsyncEventHandler<ApiRequestEventArgs>? OnMakingApiRequest;
    public event AsyncEventHandler<ApiResponseEventArgs>? OnApiResponseReceived;

    public Task DownloadFileAsync(string filePath, Stream destination, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task<TResponse> MakeRequestAsync<TResponse>(
        IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        if (request is GetMeRequest)
        {
            return Task.FromResult((TResponse)(object)new User() { IsBot = true, Username = "test_bot" });
        }

        return Task.FromResult<TResponse>(default!);
    }

    public Task<bool> TestApiAsync(
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }
}
