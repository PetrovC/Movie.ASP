using System.Collections.Generic;

namespace MovieProject.ASP.Models
{
    public class CategoryModel
    {
        public List<CategoryModel> Categories { get; set; }
        public List<MovieModel> Movies { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}