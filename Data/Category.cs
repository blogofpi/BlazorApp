using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int CategoryId { get; set; }
    }

    public class Order
    {
        [Required(ErrorMessage = "Category is required")]
        public string CategoryId { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Shipping City is required")]
        public string ShipCity { get; set; }
    }
}
