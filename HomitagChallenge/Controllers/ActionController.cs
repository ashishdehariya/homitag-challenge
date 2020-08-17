using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomitagChallenge.Services.Abstract;
using HomitagChallenge.Services.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomitagChallenge.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly IActionManager _actionManager;
        public ActionController(IActionManager actionManager)
        {
            _actionManager = actionManager;
        }

        [HttpPost]
        public ActionResult<GenreResult> AddGenre(GenreResult genre)
        {
            var genreResult = _actionManager.AddGenre(genre);
            return Created("api/Action/AddGenre", genreResult);
        }

        [HttpPost]
        public ActionResult<MovieResult> AddMovie(MovieResult movie)
        {
            var movieResult = _actionManager.AddMovie(movie);
            return Created("api/Action/AddMovie", movieResult);
        }

        [HttpDelete]
        public ActionResult<GenreResult> DeleteGenre(int genreId)
        {
            var genreResult = _actionManager.DeleteGenre(genreId);
            return Ok(genreResult);
        }

        [HttpDelete]
        public ActionResult<MovieResult> DeleteMovie(int movieId)
        {
            var deletedResult = _actionManager.DeleteMovie(movieId);
            return Ok(deletedResult);
        }

        [HttpGet]
        public ActionResult<List<GenreResult>> GetAllGenres()
        {
            var genres = _actionManager.GetAllGenres();
            return Ok(genres);
        }

        [HttpGet]
        public ActionResult<List<MovieResult>> GetMoviesByGenre([FromQuery]List<int> genreIds)
        {
            var movies = _actionManager.GetMoviesByGenre(genreIds);
            return Ok(movies);
        }

        [HttpGet]
        public ActionResult<List<MovieResult>> GetMoviesByKeyword(string keyword)
        {
            var movies = _actionManager.GetMoviesByKeyword(keyword);
            return Ok(movies);
        }

        [HttpPut]
        public ActionResult<GenreResult> UpdateGenre(GenreResult genreToUpdate)
        {
            var genre = _actionManager.UpdateGenre(genreToUpdate);
            return Ok(genre);
        }

        [HttpPut]
        public ActionResult<MovieResult> UpdateMovie(MovieResult movieToUpdate)
        {
            var movie = _actionManager.UpdateMovie(movieToUpdate);
            return Ok(movie);
        }
    }
}
