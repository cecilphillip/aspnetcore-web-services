using System.ComponentModel.DataAnnotations;

namespace ProductsODataApiDemo.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProducedBy { get; set; }
    }
}
