using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table("supermarket_products_tbl")]
    public class Product
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Required]
        [Column("category_id")]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [Required]
        [Column("product_name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("product_quantity")]
        public int? Quantity { get; set; }

        [Required]
        [Column("product_price")]
        [Range(0, int.MaxValue)]
        public double Price { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
