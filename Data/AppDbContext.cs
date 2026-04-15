using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> SupermarketCategoriesTbl { get; set; }

        public DbSet<Product> SupermarketProductsTbl { get; set; }
    }
}
