using System.Text.Json;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies.Client.Models;

namespace Movies.Client.ApiServices;

public class MovieApiService : IMovieApiService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly IHttpContextAccessor httpContextAccessor;

    public MovieApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        this.httpClientFactory = httpClientFactory;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<Movie> CreateMovie(Movie movie)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteMovie(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Movie> GetMovie(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Movie>> GetMovies()
    {
        var httpClient = httpClientFactory.CreateClient("MovieAPIClient");

        var request = new HttpRequestMessage(HttpMethod.Get, "/movies");
        var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        List<Movie> movieList = JsonSerializer.Deserialize<List<Movie>>(content);

        return movieList;
    }

    public async Task<UserInfoViewModel> GetUserInfo()
    {
        var idpClient = httpClientFactory.CreateClient("IDPClient");
        var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();

        if (metaDataResponse.IsError)
        {
            throw new HttpRequestException("Something went wrong while requesting the access token.");
        }

        var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        var userInfoResponse = await idpClient.GetUserInfoAsync(
            new UserInfoRequest
            {
                Address = metaDataResponse.UserInfoEndpoint,
                Token = accessToken
            }
        );

        if (userInfoResponse.IsError)
        {
            throw new HttpRequestException("Something went wrong while getting user info.");
        }

        var userInfoDictionary = new Dictionary<string, string>();

        foreach (var claim in userInfoResponse.Claims)
        {
            userInfoDictionary.Add(claim.Type, claim.Value);
        }

        return new UserInfoViewModel(userInfoDictionary);
    }

    public async Task<Movie> UpdateMovie(Movie movie)
    {
        throw new NotImplementedException();
    }
}
