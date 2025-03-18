using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

public class CookieHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<CookieHandler> _logger;

    public CookieHandler(IHttpContextAccessor httpContextAccessor, ILogger<CookieHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            // Copy the cookies from the current request to the outgoing request
            var cookies = httpContext.Request.Cookies;
            if (cookies.Any())
            {
                // Combine all cookies into one header value
                var cookieHeader = string.Join("; ", cookies.Select(c => $"{c.Key}={c.Value}"));
                request.Headers.Add("Cookie", cookieHeader);

                _logger.LogDebug($"Forwarded {cookies.Count} cookies to API request");
            }
        }

        // Send the request and get the response
        var response = await base.SendAsync(request, cancellationToken);

        // Log the response status for debugging
        _logger.LogDebug($"API response status: {response.StatusCode}");

        return response;
    }
}