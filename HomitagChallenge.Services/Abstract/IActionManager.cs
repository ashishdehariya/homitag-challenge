using HomitagChallenge.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomitagChallenge.Services.Abstract
{
    public interface IActionManager
    {
        public List<GenreResult> GetAllGenres();
        public GenreResult AddGenre(GenreResult genre);
        public GenreResult UpdateGenre(GenreResult genre);
        public GenreResult DeleteGenre(int genreId);
        public List<MovieResult> GetMoviesByGenre(List<int> genreIds);
        public List<MovieResult> GetMoviesByKeyword(string keyword);
        public MovieResult AddMovie(MovieResult movie);
        public MovieResult UpdateMovie(MovieResult movie);
        public MovieResult DeleteMovie(int movieId);
    }
}
