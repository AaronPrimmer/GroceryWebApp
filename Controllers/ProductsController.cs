using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var products = ProductRepository.GetProducts(true);
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var productViewModel = new ProductViewModel
            {
                Categories = CategoriesRepository.GetAllCategories()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.AddProduct(productViewModel.Product);
                return RedirectToAction("Index");
            }

            productViewModel.Categories = CategoriesRepository.GetAllCategories();
            return View(productViewModel);
        }
    }
}
