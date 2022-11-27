using System.Text.Json;
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
        var httpClient = httpClientFactory.CreateClient("MovieAPIClient");

        var request = new HttpRequestMessage(HttpMethod.Get, "/api/movies/");
        var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        List<Movie> movieList = JsonSerializer.Deserialize<List<Movie>>(content);

        return movieList;
    }

    public async Task<Movie> UpdateMovie(Movie movie)
    {
        throw new NotImplementedException();
    }
}
