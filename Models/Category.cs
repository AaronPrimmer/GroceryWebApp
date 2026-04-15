using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table("supermarket_categories_tbl")]
    public class Category
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Required]
        [Column("category_name")]
        public string Name { get; set; } = string.Empty;

        [Column("category_description")]
        public string? Description { get; set; } = string.Empty;
    }
}
