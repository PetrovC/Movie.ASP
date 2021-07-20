using Microsoft.AspNetCore.Mvc;
using MovieProject.ASP.Models;
using MovieProject.DAL.Services;
using System.Linq;

namespace MovieProject.ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiMovieController : ControllerBase
    {
        IMovieService movieService;
        public ApiMovieController(IMovieService monService)
        {
            movieService = monService;
        }
        [HttpGet]
        public MovieIndex Get()
        {
            return new MovieIndex
            {
                Movies = movieService.GetWithCategory().Select(m => new MovieModel
                {
                    Title = m.Title,
                    Poster = m.Poster,
                    CategoryName = m.Category?.Name,
                }).ToList()
            };
        }
    }
}
