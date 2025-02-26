using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieService _movieService;

    public MoviesController(MovieService movieService)
    {
        _movieService = movieService;
    }

    // Endpoint to get movie details by name
    [HttpGet("{movieName}")]
    public async Task<IActionResult> GetMovieDetails(string movieName)
    {
        var movie = await _movieService.GetMovieDetailsAsync(movieName);

        if (movie == null || movie.Title == null)
        {
            return NotFound("Movie not found.");
        }

        return Ok(movie);
    }
}
