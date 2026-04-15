using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata.Ecma335;
using WebApp.Data;
using WebApp.Models;
using WebApp.Settings;

namespace WebApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _db;

        public CategoriesController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _db.SupermarketCategoriesTbl.ToListAsync();
            return View(categories);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Action = "edit";
            var category = await _db.SupermarketCategoriesTbl.FirstOrDefaultAsync(c => c.CategoryId == (id.HasValue ? id.Value : 0));
            //var category = CategoriesRepository.GetCategoryById(id.HasValue ? id.Value : 0);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var categoryUpdate = await _db.SupermarketCategoriesTbl.SingleAsync(c => c.CategoryId == category.CategoryId);
                categoryUpdate.Name = category.Name;
                categoryUpdate.Description = category.Description;
                await _db.SaveChangesAsync();
                //CategoriesRepository.UpdateCategory(category.CategoryId, category);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "edit";
            return View(category);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            
            if (ModelState.IsValid) 
            {
                _db.SupermarketCategoriesTbl.Add(category);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "add";
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var categoryToDelete = await _db.SupermarketCategoriesTbl.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            if (categoryToDelete != null) 
            {
                _db.SupermarketCategoriesTbl.Remove(categoryToDelete);
                await _db.SaveChangesAsync();
            }
            //CategoriesRepository.DeleteCategory(categoryId);
            return RedirectToAction(nameof(Index));
        }
    }
}
