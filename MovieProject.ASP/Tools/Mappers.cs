using MovieProject.ASP.Models;
using MovieProject.DAL.Entities;
using System.Collections.Generic;

namespace MovieProject.ASP.Tools
{
    public static class Mappers
    {
        public static MovieModel ToMovieModel(this Movie m)
        {
            return new MovieModel
            {
                Id = m.Id,
                Title = m.Title,
                Poster = m.Poster,
                CategoryName = m.Category?.Name,
            };
        }
        public static Movie ToDalMovie(this CreateMovieModel form)
        {
            return new Movie
            {
                Id = form.Id,
                Title = form.Title,
                Poster = form.Poster,
                BoxOffice = form.BoxOffice,
                CategoryId = form.CategoryId,
                Director = form.Realisateur,
                Duration = form.Duration
            };
        }
        public static MovieDetail ToDetails(this Movie m)
        {
            return new MovieDetail
            {
                Id = m.Id,
                Title = m.Title,
                Poster = m.Poster,
                BoxOffice = m.BoxOffice,
                CategoryName = m.Category?.Name,
                Duration = m.Duration,
                Realisateur = m.Director
            };
        }
        public static CreateMovieModel ToCreate(this Movie m)
        {
            return new CreateMovieModel
            {
                Id = m.Id,
                Title = m.Title,
                Poster = m.Poster,
                BoxOffice = m.BoxOffice,
                CategoryId = m.CategoryId,
                Duration = m.Duration,
                Realisateur = m.Director
            };
        }
        public static CategoryModel ToCategoryFromDAL(this Category c)
        {
            return new CategoryModel
            {
                Id = c.Id,
                Name = c.Name,
            };
        }
        public static Category ToCategoryDAL(this CategoryModel c)
        {
            return new Category
            {
                Id = c.Id,
                Name = c.Name
            };
        }
        public static List<MovieModel> ToListModel(this List<Movie> l)
        {
            List<MovieModel> m = new List<MovieModel>();
            if (l !=null)
            {
                for (int i = 0; i < l.Count; i++)
                {
                    m.Add(new MovieModel
                    {
                        Id = l[i].Id,
                        Title = l[i].Title,
                        Poster = l[i].Poster,
                        CategoryName = l[i].Category?.Name,
                    });
                }
            }
            return m;
        }
    }
}
