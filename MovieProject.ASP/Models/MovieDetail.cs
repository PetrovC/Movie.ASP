using System;

namespace MovieProject.ASP.Models
{
    public class MovieDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal BoxOffice { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Poster { get; set; }
        public string Realisateur { get; set; }
        public string CategoryName { get; set; }
    }
}
