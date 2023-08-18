using Microsoft.AspNetCore.Mvc.Testing;
using Telegram.Bot.Pipelined.Test.Utilities.Types;

namespace Telegram.Bot.Pipelined.Test.Utilities.Fixtures;

public class TestWebAppFixture : IDisposable
{
    internal WebApplicationFactory<Program>? Factory { get; private set; }
    public HttpClient? Client { get; private set; }

    public TestWebAppFixture()
    {
        Factory = new TestWebApplicationFactory();
        Client = Factory.Server.CreateClient();
    }

    public void Dispose()
    {
        Client?.Dispose();
        Factory?.Dispose();

        Client = null;
        Factory = null;

        GC.SuppressFinalize(this);
    }
}
