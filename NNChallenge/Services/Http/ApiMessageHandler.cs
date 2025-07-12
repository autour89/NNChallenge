using System.Net;

namespace NNChallenge.Services.Http;

public class ApiMessageHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw response.StatusCode switch
            {
                HttpStatusCode.Unauthorized => new UnauthorizedAccessException(
                    "API key is invalid"
                ),
                HttpStatusCode.NotFound => new ArgumentException("Location not found"),
                HttpStatusCode.TooManyRequests => new InvalidOperationException(
                    "Rate limit exceeded"
                ),
                _ => new HttpRequestException(
                    $"API request failed with status {response.StatusCode}"
                ),
            };
        }

        return response;
    }
}
