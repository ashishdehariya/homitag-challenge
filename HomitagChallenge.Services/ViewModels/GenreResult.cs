using HomitagChallenge.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomitagChallenge.Services.ViewModels
{
    public class GenreResult: BaseModel
    {
        public string Name { get; set; }
        public string Decription { get; set; }
    }
}
