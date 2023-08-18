namespace Telegram.Bot.Pipelined.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseBotPipelined(
        this IApplicationBuilder app)
    {
        app.UseMiddleware<BotPipelinedMiddleware>();
        app.UseRouting();

        return app;
    }
}
