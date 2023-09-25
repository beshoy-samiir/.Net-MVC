using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMVC.Models;
using TestMVC.Repository;
using TestMVC.ViewModel;

namespace TestMVC.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository productRepository;
        IRepository<Category> categoryRepository;
        public ProductController(IProductRepository ProductRepo,IRepository<Category> categoryRepo)
        {
            productRepository = ProductRepo;
            categoryRepository = categoryRepo;
        }
        public IActionResult Details(int id)
        {
            Product ProductModel = productRepository.Get(id);
            if (ProductModel != null)
                return View("Details", ProductModel);
            return BadRequest("Invalid id");
        }
        public IActionResult ShowAll(string? search)
        {
            ViewBag.Search = search;
            if (string.IsNullOrEmpty(search) == true)
            {
                List<Product> ProductsList = productRepository.GetAll();
                return View("ShowAllProducts", ProductsList);
            }
            else
            {
                List<Product> ProductsList = productRepository.GetAll();
                return View("ShowAllProducts", ProductsList.Where(c => c.Name.Contains(search)).ToList());
            }
        }
    
        public IActionResult Edit(int id)
        {
            Product productModel = productRepository.Get(id);
            ProductWithCategoryListViewModel productVM = new ProductWithCategoryListViewModel()
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                CatId = productModel.CatId,
                Price = productModel.Price
            };
            productVM.Categories = categoryRepository.GetAll();
            return View("Edit", productVM);
        }
        [HttpPost]
        public IActionResult SaveEdit(Product newProduct)
        {
            if (ModelState.IsValid == true)
            {
                productRepository.Update(newProduct);
                productRepository.Save();
                return RedirectToAction("Details", "Product", new { id = newProduct.Id, name = newProduct.Name });
            }
            ProductWithCategoryListViewModel productVM = new ProductWithCategoryListViewModel();
            productVM.Id = newProduct.Id;
            productVM.Name = newProduct.Name;
            productVM.Price = newProduct.Price;
            productVM.Image = newProduct.Image;
            productVM.Description = newProduct.Description;
            productVM.CatId = newProduct.CatId;
            productVM.Categories = categoryRepository.GetAll();

            return View("Edit", productVM);
        }

        public IActionResult Delete(int id)
        {
            Product productModel = productRepository.Get(id);
            return View("Delete", productModel);
        }
        public IActionResult DeleteCurrent(int id) 
        {
            productRepository.Delete(id);
            productRepository.Save();
            return RedirectToAction("ShowAll");
        }
        public IActionResult New()
        {
            return View("New");
        }
        public IActionResult SaveNew(Product product)
        {
            if (product.Name != null)
            {
                productRepository.Insert(product);
                productRepository.Save();
                return RedirectToAction("ShowAll");
            }
            return View("New", product);
        }
    }
}
