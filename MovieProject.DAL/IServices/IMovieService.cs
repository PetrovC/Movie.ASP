using MovieProject.DAL.Entities;
using System.Collections.Generic;

namespace MovieProject.DAL.Services
{
    public interface IMovieService
    {
        int Add(Movie m);
        void Delete(int id);
        Movie GetOneById(int id);
        List<Movie> GetWithCategory();
        void Update(Movie m);
    }
}