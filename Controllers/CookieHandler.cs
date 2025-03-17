using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

public class CookieHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext != null)
        {
            // Copy the cookies from the current request to the outgoing request
            var cookies = httpContext.Request.Cookies;
            foreach (var cookie in cookies)
            {
                request.Headers.Add("Cookie", $"{cookie.Key}={cookie.Value}");
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}