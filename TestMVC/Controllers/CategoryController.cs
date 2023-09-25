using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TestMVC.Models;
using TestMVC.Repository;

namespace TestMVC.Controllers
{
    public class CategoryController : Controller
    {
        IRepository<Category> categoryRepository;
        public CategoryController(IRepository<Category> categoryRepo, IConfiguration configuration)
        {
            categoryRepository = categoryRepo;
            string name = configuration["AppName"];
        }
        [Authorize(Roles = "Admin,User")]
        public IActionResult Index(string? search)
        {
            ViewBag.Search = search;
            if (string.IsNullOrEmpty(search) == true)
            {
                List<Category> CatListModel = categoryRepository.GetAll();
                return View("Index", CatListModel);
            }
            else
            {
                List<Category> CatListModel = categoryRepository.GetAll();
                return View("Index", CatListModel.Where(c => c.Name.Contains(search)).ToList());
            }
            
        }
        public IActionResult New()
        {
            return View("New");
        }
        public IActionResult SaveNew(Category cat)
        {
            if (cat.Name != null)
            {
                categoryRepository.Insert(cat);
                categoryRepository.Save();
                return RedirectToAction("Index");
            }
            return View("New", cat);
        }

        public IActionResult Delete(int id)
        {
            Category categoryModel = categoryRepository.Get(id);
            return View("Delete", categoryModel);
        }
        public IActionResult DeleteCurrent(int id)
        {
            categoryRepository.Delete(id);
            categoryRepository.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            Category categoryModel = categoryRepository.Get(id);
            if (categoryModel != null)
                return View("Details", categoryModel);
            return BadRequest("Invalid id");
        }

    }
}
