using IdentityModel.Client;

namespace Movies.Client.HttpHandlers;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ClientCredentialsTokenRequest tokenRequest;

    public AuthenticationDelegatingHandler(IHttpClientFactory httpClientFactory, ClientCredentialsTokenRequest tokenRequest)
    {
        this.httpClientFactory = httpClientFactory;
        this.tokenRequest = tokenRequest;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpClient = httpClientFactory.CreateClient("IDPClient");

        var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(tokenRequest);
        if (tokenResponse.IsError)
        {
            throw new HttpRequestException("Something went wrong while requesting the access token.");
        }

        request.SetBearerToken(tokenResponse.AccessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}
