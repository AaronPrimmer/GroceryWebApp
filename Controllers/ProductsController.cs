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

        public IActionResult Add()
        {
            ViewBag.Action = "add";
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

            ViewBag.Action = "add";
            productViewModel.Categories = CategoriesRepository.GetAllCategories();
            return View(productViewModel);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Action = "edit";
            var productViewModel = new ProductViewModel
            {
                Product = ProductRepository.GetProductById(id) ?? new Product(),
                Categories = CategoriesRepository.GetAllCategories()
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.UpdateProduct(productViewModel.Product.ProductId, productViewModel.Product);
                return RedirectToAction("Index");
            }

            ViewBag.Action = "edit";
            productViewModel.Categories = CategoriesRepository.GetAllCategories();
            return View(productViewModel);
        }

        public IActionResult Delete(int categoryId)
        {
            ProductRepository.DeleteProduct(categoryId);
            return RedirectToAction(nameof(Index));
        } 
        
    }
}
