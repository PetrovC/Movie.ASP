using System;

namespace MovieProject.DAL.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Director { get; set; }
        public decimal BoxOffice { get; set; }
        public string Poster { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
