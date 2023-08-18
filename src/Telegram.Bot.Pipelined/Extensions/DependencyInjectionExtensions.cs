namespace Telegram.Bot.Pipelined.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBotPipelined(
        this IServiceCollection services,
        Action<BotOptions> configureOptions)
    {
        services.AddBotOptions(configureOptions);
        services.AddScoped<BotPipelinedMiddleware>();
        services.AddBotClient();

        return services;
    }

    private static IServiceCollection AddBotOptions(
        this IServiceCollection services,
        Action<BotOptions> configureOptions)
    {
        services.AddSingleton(services =>
        {
            var options = new BotOptions();
            configureOptions(options);
            return options;
        });

        return services;
    }

    private static IServiceCollection AddBotClient(
        this IServiceCollection services)
    {
        services.AddTransient<ITelegramBotClient>(services =>
        {
            var options = services.GetRequiredService<BotOptions>();
            var botClient = new TelegramBotClient(options.BotToken);
            return botClient;
        });

        return services;
    }
}
