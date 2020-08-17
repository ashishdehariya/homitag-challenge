using System;
using System.Collections.Generic;

namespace HomitagChallenge.DataAccessLayer.Models
{
    public partial class MovieGenres : BaseModel
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }

        public virtual Genres Genre { get; set; }
        public virtual Movies Movie { get; set; }
    }
}
