using MovieProject.DAL.Entities;
using System.Collections.Generic;

namespace MovieProject.DAL.Services
{
    public interface ICategoryService
    {
        int Add(Category c);
        void Delete(int id);
        List<Category> Get();
        Category GetById(int id);
        void Update(Category c);
    }
}