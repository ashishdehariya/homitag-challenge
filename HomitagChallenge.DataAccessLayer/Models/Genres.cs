using System;
using System.Collections.Generic;

namespace HomitagChallenge.DataAccessLayer.Models
{
    public partial class Genres : BaseModel
    {
        public Genres()
        {
            MovieGenres = new HashSet<MovieGenres>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<MovieGenres> MovieGenres { get; set; }
    }
}
