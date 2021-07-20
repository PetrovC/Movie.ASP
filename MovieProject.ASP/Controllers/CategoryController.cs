using Microsoft.AspNetCore.Mvc;
using MovieProject.ASP.Models;
using MovieProject.ASP.Tools;
using MovieProject.DAL.Entities;
using MovieProject.DAL.Services;
using System.Collections.Generic;
using System.Linq;

namespace MovieProject.ASP.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _catService;
        public CategoryController(ICategoryService c)
        {
            _catService = c;
        }
        public IActionResult Index()
        {
            List<Category> list = _catService.Get();
            return View(new CategoryModel
            {
                Categories = list.Select(m => m.ToCategoryFromDAL()).ToList()
            });
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryModel c)
        {
            _catService.Add(c.ToCategoryDAL());
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            CategoryModel c = _catService.GetById(id).ToCategoryFromDAL();
            return View(c);
        }
        [HttpPost]
        public IActionResult Edit(CategoryModel c)
        {
            _catService.Update(c.ToCategoryDAL());
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            CategoryModel c = _catService.GetById(id).ToCategoryFromDAL();
            return View(c);
        }
        [HttpPost]
        public IActionResult Delete(CategoryModel c)
        {
            _catService.Delete(c.Id);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            CategoryModel c = new CategoryModel();
            c = _catService.GetById(id).ToCategoryFromDAL();
            c.Movies = _catService.GetFilmByCategory(id).ToListModel();
            return View(c);
        }
    }
}
