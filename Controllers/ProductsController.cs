using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
            var products = await _db.SupermarketProductsTbl.Include(p => p.Category).ToListAsync();
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

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Action = "edit";
            var allInfo = await _db.SupermarketProductsTbl.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);

            var productViewModel = new ProductViewModel
            {
                Product = allInfo != null ? new Product() { ProductId = allInfo.ProductId, Name = allInfo.Name, Price = allInfo.Price, Quantity = allInfo.Quantity, CategoryId = allInfo.CategoryId } : new Product(),
                //Product = ProductRepository.GetProductById(id) ?? new Product(),
                Categories = await _db.SupermarketCategoriesTbl.ToListAsync()
            };
            
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var productToUpdate = await _db.SupermarketProductsTbl.FirstOrDefaultAsync(p => p.ProductId == productViewModel.Product.ProductId);
                if (productToUpdate != null)
                {
                    productToUpdate.Name = productViewModel.Product.Name;
                    productToUpdate.Price = productViewModel.Product.Price;
                    productToUpdate.Quantity = productViewModel.Product.Quantity;
                    productToUpdate.CategoryId = productViewModel.Product.CategoryId;
                    await _db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            ViewBag.Action = "edit";
            productViewModel.Categories = await _db.SupermarketCategoriesTbl.ToListAsync();
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int productId)
        {
            var productToDelete = _db.SupermarketProductsTbl.Find(productId);
            if (productToDelete != null) 
            {
                _db.SupermarketProductsTbl.Remove(productToDelete);
                _db.SaveChanges();
            }
            //ProductRepository.DeleteProduct(productId);
            return RedirectToAction(nameof(Index));
        } 
        
    }
}
