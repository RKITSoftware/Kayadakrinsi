namespace Bills.Models
{
    public class ProductItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        // Adjusted constructor to handle null values gracefully
        public ProductItem(int id = default, int quantity = default)
        {
            Id = id != default ? id : throw new ArgumentNullException(nameof(id));
            Quantity = quantity != default ? quantity : throw new ArgumentNullException(nameof(quantity));
        }

    }

    public class Bill
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int SellerId { get; set; }

        public int PurchaserId { get; set; }

        public List<ProductItem>? Products { get; set; }

        public double TotalAmount { get; set; }

        public Bill(int id, DateTime date, int sellerId, int purchaserId, List<ProductItem> products, double totalAmount)
        {
            Id = id;
            Date = date;
            SellerId = sellerId;
            PurchaserId = purchaserId;
            Products = products;
            TotalAmount = totalAmount;
        }
    }
}
