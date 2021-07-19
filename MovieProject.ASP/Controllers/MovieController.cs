using Microsoft.AspNetCore.Mvc;
using MovieProject.ASP.Models;
using MovieProject.ASP.Tools;
using MovieProject.DAL.Entities;
using MovieProject.DAL.Services;
using System.Collections.Generic;
using System.Linq;

namespace MovieProject.ASP.Controllers
{
    public class MovieController : Controller
    {
        private readonly ICategoryService _catService;
        private readonly IMovieService _movieService;
        public MovieController(IMovieService m, ICategoryService c)
        {
            _catService = c;
            _movieService = m;
        }
        public IActionResult Index()
        {
            List<Movie> list = _movieService.GetWithCategory();
            return View(new MovieIndex
            {
                Movies = list.Select(m => m.ToMovieModel()).ToList()
            });
        }
        public IActionResult Create()
        {
            return View(new CreateMovieModel
            {
                Categories = _catService.Get().Select(m => new CategoryModel
                {
                    Id = m.Id,
                    Name = m.Name,
                }).ToList()
            }); 
        }

        [HttpPost]
        public IActionResult Create(CreateMovieModel form)
        {
            if (ModelState.IsValid)
            {
                _movieService.Add(form.ToDalMovie());
                return RedirectToAction("Index");

            }
            return View(form);
        }
        public IActionResult Detail(int id)
        {
            return View(_movieService.GetOneById(id).ToDetails());
        }
        public IActionResult Delete(int id)
        {
            return View(_movieService.GetOneById(id).ToDetails());
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            _movieService.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            CreateMovieModel cmm = _movieService.GetOneById(id).ToCreate();
            cmm.Categories = _catService.Get().Select(m => new CategoryModel
            {
                Id = m.Id,
                Name = m.Name,
            }).ToList();
            return View(cmm);
        }
        [HttpPost]
        public IActionResult Update(CreateMovieModel m)
        {
            _movieService.Update(m.ToDalMovie());
            return RedirectToAction("Index");
        }
    }
}
