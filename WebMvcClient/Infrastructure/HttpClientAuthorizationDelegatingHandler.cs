﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using System.Net.Http.Headers;

namespace WebMvcClient.Infrastructure;

public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                 CancellationToken cancellationToken)
    {
        StringValues authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            request.Headers.Add("Authorization",
                                new List<string>
                                {
                                    authorizationHeader
                                });
        }

        string? token = await GetToken();

        if (token != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        StringValues authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            request.Headers.Add("Authorization",
                                new List<string>
                                {
                                    authorizationHeader
                                });
        }

        string? token = GetToken().Result;

        if (token != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return base.Send(request, cancellationToken);
    }

    private async Task<string> GetToken()
    {
        const string ACCESS_TOKEN = "access_token";

        return await _httpContextAccessor.HttpContext.GetTokenAsync(ACCESS_TOKEN);
    }
}