using System.ComponentModel.DataAnnotations;

namespace ProductsApiDemo.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string ProducedBy { get; set; }
    }
}
