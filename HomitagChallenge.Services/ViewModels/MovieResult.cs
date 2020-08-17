using HomitagChallenge.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomitagChallenge.Services.ViewModels
{
    public class MovieResult :  BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<GenreResult> Genre { get; set; }
        public long DurationInSeconds { get; set; }
        public int Rating { get; set; }
    }
}
