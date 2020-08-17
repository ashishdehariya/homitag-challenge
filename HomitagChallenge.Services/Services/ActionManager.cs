using HomitagChallenge.DataAccessLayer.Abstract;
using HomitagChallenge.DataAccessLayer.Models;
using HomitagChallenge.Services.Abstract;
using HomitagChallenge.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace HomitagChallenge.Services.Services
{
    public class ActionManager : IActionManager
    {
        private readonly IRepository<Genres> _genreRepository;
        private readonly IRepository<Movies> _movieRepository;
        private readonly IRepository<MovieGenres> _movieGenreRepository;
        public ActionManager(IRepository<Genres> genreRepository, IRepository<Movies> movieRepository, IRepository<MovieGenres> movieGenreRepository)
        {
            _genreRepository = genreRepository;
            _movieRepository = movieRepository;
            _movieGenreRepository = movieGenreRepository;
        }
        public GenreResult AddGenre(GenreResult genre)
        {
            Genres dbGenre = new Genres()
            {
                Name = genre.Name,
                Description = genre.Decription
            };
            var createdGenre = _genreRepository.Add(dbGenre);
            _genreRepository.Save();
            genre.Id = createdGenre.Id;

            return genre;
        }

        public MovieResult AddMovie(MovieResult movie)
        {
            Movies dbMovie = new Movies();
            dbMovie.Name = movie.Name;
            dbMovie.Description = movie.Description;
            dbMovie.DurationInSeconds = movie.DurationInSeconds;

            dbMovie.Rating = movie.Rating;
            dbMovie.ReleaseDate = movie.ReleaseDate;

            var createdMovie = _movieRepository.Add(dbMovie);
            _movieRepository.Save();
            movie.Id = createdMovie.Id;

            //since one movie can have multiple genres
            movie.Genre.ForEach(g =>
            {
                var movieGenre = _movieGenreRepository.Add(new MovieGenres { MovieId = createdMovie.Id, GenreId = createdMovie.Id });
            });
            _movieGenreRepository.Save();

            return movie;
        }

        public GenreResult DeleteGenre(int genreId)
        {
            //Remove from dependent movies
            _movieGenreRepository.GetAll().Where(mg => mg.GenreId == genreId).ToList().ForEach(mg =>
            {
                _movieGenreRepository.Delete(mg);
            });
            _genreRepository.Save();

            var dbGenre = _genreRepository.Get(genreId);
            var deletedGenre = _genreRepository.Delete(dbGenre);
            _genreRepository.Save();

            GenreResult genre = new GenreResult { Id = dbGenre.Id, Name = dbGenre.Name, Decription = dbGenre.Description };
            return genre;
        }

        public MovieResult DeleteMovie(int movieId)
        {
            var dbMovie = _movieRepository.Get(movieId);
            
            //Remove from dependent movies
            _movieGenreRepository.GetAll().Where(mg => mg.MovieId == movieId).ToList().ForEach(mg =>
            {
                _movieGenreRepository.Delete(mg);
            });
            _genreRepository.Save();

            //Delete movie
            var deletedMovie = _movieRepository.Delete(dbMovie);
            _movieRepository.Save();

            MovieResult movieResult = new MovieResult()
            {
                Id = dbMovie.Id,
                Name = dbMovie.Name,
                Description = dbMovie.Description,
                DurationInSeconds = dbMovie.DurationInSeconds,
                ReleaseDate = dbMovie.ReleaseDate,
                Rating = dbMovie.Rating ?? 0,
                Genre = _genreRepository.GetAll().Where(g => dbMovie.MovieGenres.Select(mg => mg.GenreId).Contains(g.Id))
                        .Select(g => new GenreResult { Id = g.Id, Name = g.Name, Decription = g.Description }).ToList()
            };

            return movieResult;
        }


        public List<GenreResult> GetAllGenres()
        {
            var genres = _genreRepository.GetAll().Select(g => new GenreResult { Id = g.Id, Name = g.Name, Decription = g.Description }).ToList();
            return genres;
        }

        public List<MovieResult> GetMoviesByGenre(List<int> genreIds)
        {
            var movies = _movieRepository.GetAll().Where(m => _movieGenreRepository.GetAll().Where(mg => genreIds
                .Contains(mg.GenreId)).Select(mg => mg.MovieId).Contains(m.Id)).Select(dbm => new MovieResult
                {
                    Id = dbm.Id,
                    Name = dbm.Name,
                    Description = dbm.Description,
                    DurationInSeconds = dbm.DurationInSeconds,
                    ReleaseDate = dbm.ReleaseDate,
                    Rating = dbm.Rating ?? 0,
                    Genre = _genreRepository.GetAll().Where(g => dbm.MovieGenres.Select(mg => mg.GenreId).Contains(g.Id)).Select(g => new GenreResult
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Decription = g.Description
                    }).ToList()
                }).ToList();

            return movies;
        }

        public List<MovieResult> GetMoviesByKeyword(string keyword)
        {
            var moviesWithNameMatch = _movieRepository.GetAll().Where(m => m.Name.Contains(keyword));
            var moviesWithDescriptionMatch = _movieRepository.GetAll().Where(m => m.Description.Contains(keyword));

            List<MovieResult> matchedMovies = moviesWithNameMatch.Concat(moviesWithDescriptionMatch).Distinct().Select(m => new MovieResult
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                DurationInSeconds = m.DurationInSeconds,
                ReleaseDate = m.ReleaseDate,
                Rating = m.Rating ?? 0,
                Genre = _genreRepository.GetAll().Where(g => m.MovieGenres.Select(mg => mg.GenreId).Contains(g.Id)).Select(g => new GenreResult
                {
                    Id = g.Id,
                    Name = g.Name,
                    Decription = g.Description
                }).ToList()
            }).ToList();

            return matchedMovies;
        }

        public GenreResult UpdateGenre(GenreResult genreToUpdate)
        {
            var dbGenre = _genreRepository.Get(genreToUpdate.Id);
            dbGenre.Name = genreToUpdate.Name;
            dbGenre.Description = genreToUpdate.Decription;

            dbGenre = _genreRepository.Update(dbGenre);
            _genreRepository.Save();

            return genreToUpdate;
        }

        public MovieResult UpdateMovie(MovieResult movieToUpdate)
        {
            var dbMovie = _movieRepository.Get(movieToUpdate.Id);
            dbMovie.Name = movieToUpdate.Name;
            dbMovie.Description = movieToUpdate.Description;
            dbMovie.DurationInSeconds = movieToUpdate.DurationInSeconds;
            dbMovie.Rating = movieToUpdate.Rating;
            dbMovie.ReleaseDate = movieToUpdate.ReleaseDate;

            dbMovie = _movieRepository.Update(dbMovie);
            _movieRepository.Save();
            
            //update genres
            IEnumerable<int> commonGenresOfOldAndNew = _movieGenreRepository.GetAll().Where(mg => mg.MovieId == dbMovie.Id).Select(mg => mg.GenreId).ToList()
                .Intersect(movieToUpdate.Genre.Select(g => g.Id)).ToList();

            var movieGenreToDelete = _movieGenreRepository.GetAll().Where(mg => mg.MovieId == dbMovie.Id && !commonGenresOfOldAndNew.Contains(mg.GenreId));
            var movieGenreToAdd = movieToUpdate.Genre.Where(mg => !commonGenresOfOldAndNew.Contains(mg.Id)).Select(g => new MovieGenres
            {
                MovieId = movieToUpdate.Id,
                GenreId = g.Id
            });

            movieGenreToDelete.ToList().ForEach(mg =>
            {
                _movieGenreRepository.Delete(mg);
            });

            var newGenres = new List<GenreResult>();
            movieGenreToAdd.ToList().ForEach(mg =>
            {
                _movieGenreRepository.Add(mg);
            });
            _movieGenreRepository.Save();

            //set new genres in response
            movieToUpdate.Genre = _genreRepository.GetAll().Where(g => commonGenresOfOldAndNew.Concat(movieGenreToAdd.Select(mg => mg.GenreId)).Contains(g.Id))
                .Select(g => new GenreResult 
                { 
                    Id = g.Id,
                    Name = g.Name,
                    Decription = g.Description
                }).ToList();

            return movieToUpdate;
        }
    }
}
