using Telegram.Bot.Types;

namespace Telegram.Bot.Pipelined.Test.Utilities.Types;

public class UpdateData<TResult>
{
    public Update Update { get; set; }

    public TResult ExpectedResult { get; set; }

    public UpdateData(Update update, TResult expectedResult)
    {
        Update = update;
        ExpectedResult = expectedResult;
    }
}
