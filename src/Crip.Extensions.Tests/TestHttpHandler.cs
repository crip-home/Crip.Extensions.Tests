using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Crip.Extensions.Tests;

/// <summary>
/// DelegatingHandler test implementation.
/// </summary>
public class TestHttpHandler : DelegatingHandler
{
    private readonly HttpResponseMessage _returnMessage;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestHttpHandler"/> class.
    /// </summary>
    /// <param name="returnMessage">The response message to return on execution.</param>
    public TestHttpHandler(HttpResponseMessage returnMessage)
    {
        _returnMessage = returnMessage;
    }

    /// <inheritdoc />
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken ct)
    {
        return Task.Factory.StartNew(() => _returnMessage, ct);
    }
}