using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace WXercises.AcceptanceTests
{
    public abstract class TestBase
    {
        private TestServer _server;

        protected TestServer Server => _server ?? (_server = CreateServer());

        protected virtual TestServer CreateServer()
        {
            var builder = Program.CreateWebHostBuilder(new string[] { })
                .UseEnvironment("AcceptanceTest")
                .UseStartup<Startup>();

            return new TestServer(builder);
        }
    }
}
