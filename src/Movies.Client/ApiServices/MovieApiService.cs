using System.Text.Json;
using IdentityModel.Client;
using Movies.Client.Models;

namespace Movies.Client.ApiServices;

public class MovieApiService : IMovieApiService
{
    private readonly IHttpClientFactory httpClientFactory;

    public MovieApiService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
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
        var apiClientCredentials = new ClientCredentialsTokenRequest
        {
            Address = "https://localhost:5005/conect/token",
            ClientId = "movieClient",
            ClientSecret = "secret",
            Scope = "movieAPI"
        };

        var client = new HttpClient();

        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5005");
        if (disco.IsError)
        {
            return null;
        }

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiClientCredentials);
        if (tokenResponse.IsError)
        {
            return null;
        }

        var apiClient = new HttpClient();
        client.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.GetAsync("https://localhost:5001/api/movies");
        response.EnsureSuccessStatusCode();

        string content = await response.Content.ReadAsStringAsync();

        List<Movie> movieList = JsonSerializer.Deserialize<List<Movie>>(content);

        return movieList;
    }

    public async Task<Movie> UpdateMovie(Movie movie)
    {
        throw new NotImplementedException();
    }
}
