using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WXercises.UnitTests.Proxies
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        public HttpRequestMessage Request { get; private set; }
        public HttpResponseMessage Response { get; set; }
        public string RequestContent { get; private set; }
        public HttpStatusCode MockStatusCode { get; set; } = HttpStatusCode.OK;
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Request = request;

            if (request.Content != null)
            {
                RequestContent = request.Content.ReadAsStringAsync().Result;
            }

            return await Task.Run(() => Response ?? new HttpResponseMessage(MockStatusCode)
                {
                    Content = new StringContent("[{}]", new ASCIIEncoding(), "application/json")
                },
                cancellationToken);
        }
    }
}
