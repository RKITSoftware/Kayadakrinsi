using System.ComponentModel.DataAnnotations;

namespace Bills.Models
{
    public class Products
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public Products(int productId, string name, double price)
        {
            ProductId = productId;
            Name = name;
            Price = price;
        }
    }

}
