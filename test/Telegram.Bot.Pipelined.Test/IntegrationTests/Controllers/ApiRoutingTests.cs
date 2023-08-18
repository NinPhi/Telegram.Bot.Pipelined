using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
using Telegram.Bot.Pipelined.Test.Utilities.Fixtures;
using Telegram.Bot.Pipelined.Test.Utilities.Theory;
using Telegram.Bot.Pipelined.Test.Utilities.Types;

namespace Telegram.Bot.Pipelined.Test.IntegrationTests.Controllers;

public class ApiRoutingTests : IClassFixture<TestWebAppFixture>
{
    private readonly HttpClient _testClient;

    public ApiRoutingTests(TestWebAppFixture fixture)
    {
        _testClient = fixture.Client
            ?? throw new ArgumentException(nameof(fixture.Client));
    }

    [Theory]
    [ClassData(typeof(MatchingUpdateTheoryData))]
    public async Task BotEndpoints_MatchingUpdate_ReturnsExpectedResult(
        UpdateData<string> updateData)
    {
        // Arrange
        var updateJson = JsonConvert.SerializeObject(updateData.Update);
        var updateContent = new StringContent(updateJson, Encoding.UTF8, "application/json");

        // Act
        var response = await _testClient.PostAsync("/test", updateContent);
        var result = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(updateData.ExpectedResult, result);
    }
}
