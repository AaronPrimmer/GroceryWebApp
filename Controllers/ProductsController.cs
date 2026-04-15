using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _db;

        public ProductsController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _db.SupermarketProductsTbl.ToListAsync();
            //var products = ProductRepository.GetProducts(true);
            return View(products);
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.Action = "add";
            var productViewModel = new ProductViewModel
            {
                Categories = await _db.SupermarketCategoriesTbl.ToListAsync()
                //Categories = CategoriesRepository.GetAllCategories()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var productAdd = await _db.SupermarketProductsTbl.AddAsync(productViewModel.Product);
                await _db.SaveChangesAsync();
                //ProductRepository.AddProduct(productViewModel.Product);
                return RedirectToAction("Index");
            }

            ViewBag.Action = "add";
            productViewModel.Categories = await _db.SupermarketCategoriesTbl.ToListAsync();
            //productViewModel.Categories = CategoriesRepository.GetAllCategories();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int productId)
        {
            ProductRepository.DeleteProduct(productId);
            return RedirectToAction(nameof(Index));
        } 
        
    }
}
