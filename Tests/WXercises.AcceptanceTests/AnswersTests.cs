using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using WXercises.Models;
using Xunit;

namespace WXercises.AcceptanceTests
{
    public class AnswersTests  : TestBase
    {
        [Fact]
        public async Task retrieve_user()
        {
            using (var client = Server.CreateClient())
            {
                var response = await client.GetAsync("https://localhost/api/answers/user");
                var result = await response.Content.ReadAsAsync<User>();

                result.Should().NotBeNull();
                result.Name.Should().BeEquivalentTo("Johan Sugiarto");
            }
        }
    }
}
