using Telegram.Bot.Pipelined.Test.Utilities.Types;
using Telegram.Bot.Pipelined.TestApi.Controllers;
using Telegram.Bot.Types;

namespace Telegram.Bot.Pipelined.Test.Utilities.Theory;

public class MatchingUpdateTheoryData : TheoryData<UpdateData<string>>
{
    public MatchingUpdateTheoryData()
    {
        Add(new UpdateData<string>(new Update()
        {
            Message = new Message()
            {
                Text = "/start param1 param2",
                Date = DateTime.Now,
                Chat = new Chat(),
            }
        }, nameof(TestController.Start)));

        Add(new UpdateData<string>(new Update()
        {
            Message = new Message()
            {
                Text = "/cancel",
                Date = DateTime.Now,
                Chat = new Chat(),
            }
        }, nameof(TestController.Cancel)));

        Add(new UpdateData<string>(new Update()
        {
            InlineQuery = new InlineQuery()
            {
                Id = "0",
                From = new User()
                {
                    FirstName = "test",
                    Username = "test_user",
                },
                Query = "test query",
                Offset = string.Empty,
            }
        }, nameof(TestController.Inline)));

        Add(new UpdateData<string>(new Update()
        {
            Message = new Message()
            {
                Text = "test message",
                Date = DateTime.Now,
                Chat = new Chat(),
            }
        }, nameof(TestController.Default)));
    }
}
