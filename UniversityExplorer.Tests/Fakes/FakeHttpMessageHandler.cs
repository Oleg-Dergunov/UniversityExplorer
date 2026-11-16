using System.Net;
using System.Text;

namespace UniversityExplorer.Tests.Fakes;

public class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly string _responseJson;

    public FakeHttpMessageHandler(string responseJson)
    {
        _responseJson = responseJson;
    }

    public List<Uri> RequestedUris { get; } = new();

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        RequestedUris.Add(request.RequestUri!);

        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(_responseJson, Encoding.UTF8, "application/json")
        });
    }
}