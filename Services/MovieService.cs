using Newtonsoft.Json;

public class MovieService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public MovieService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<Movie> GetMovieDetailsAsync(string movieName)
    {
        // Get the API key from environment variable
        var apiKey = Environment.GetEnvironmentVariable("OMDB_API_KEY");
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("API key is not set.");
        }

        var baseUrl = _configuration["OMDbApiSettings:BaseUrl"];

        var url = $"{baseUrl}?t={movieName}&apikey={apiKey}";

        var response = await _httpClient.GetStringAsync(url);

        var movie = JsonConvert.DeserializeObject<Movie>(response);

        return movie;
    }
}
