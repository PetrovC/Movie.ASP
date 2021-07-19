﻿using System;
using System.Collections.Generic;

namespace MovieProject.ASP.Models
{
    public class CreateMovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal BoxOffice { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Poster { get; set; }
        public string Realisateur { get; set; }
        public int? CategoryId { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
}
