using System;
using System.Collections.Generic;

namespace HomitagChallenge.DataAccessLayer.Models
{
    public partial class Movies : BaseModel
    {
        public Movies()
        {
            MovieGenres = new HashSet<MovieGenres>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        //public int GenreId { get; set; }
        public long DurationInSeconds { get; set; }
        public int? Rating { get; set; }

        //public virtual Genres Genre { get; set; }
        public virtual ICollection<MovieGenres> MovieGenres { get; set; }
    }
}
