using Bills.Models;

namespace Bills.Data
{
    public class Data
    {
        public static List<Products> productList = new List<Products>
        {
            new Products(1, "Laptop", 999.99),
            new Products(2, "Smartphone", 499.99),
            new Products(3, "Tablet", 299.99),
            new Products(4, "Headphones", 89.99),
            new Products(5, "Smartwatch", 199.99),
            new Products(6, "Keyboard", 49.99),
            new Products(7, "Mouse", 29.99),
            new Products(8, "Monitor", 149.99),
            new Products(9, "Printer", 89.99),
            new Products(10, "External Hard Drive", 79.99)
        };

        public static List<Company> companyList = new List<Company>
        {
            new Company(1, "Tech Solutions Inc.", "27AAAPL1234C1Z1"),
            new Company(2, "Global Enterprises Ltd.", "27AAAPL1234C2Z2"),
            new Company(3, "Innovative Designs Co.", "27AAAPL1234C3Z3"),
            new Company(4, "Precision Systems Ltd.", "27AAAPL1234C4Z4"),
            new Company(5, "Apex Technologies", "27AAAPL1234C5Z5"),
            new Company(6, "Elite Software Solutions", "27AAAPL1234C6Z6"),
            new Company(7, "Prime Innovations Inc.", "27AAAPL1234C7Z7"),
            new Company(8, "Alpha Industries", "27AAAPL1234C8Z8"),
            new Company(9, "Omega Networks Ltd.", "27AAAPL1234C9Z9"),
            new Company(10, "Pinnacle Solutions", "27AAAPL1234C0Z1"),
            new Company(11, "Vanguard Systems", "27AAAPL1234C1Z2"),
            new Company(12, "Liberty Software Inc.", "27AAAPL1234C2Z3"),
            new Company(13, "Summit Technologies", "27AAAPL1234C3Z4"),
            new Company(14, "Spectrum Innovations", "27AAAPL1234C4Z5"),
            new Company(15, "Dynamic Designs Ltd.", "27AAAPL1234C5Z6"),
            new Company(16, "Advanced Solutions Inc.", "27AAAPL1234C6Z7"),
            new Company(17, "Infinite Networks", "27AAAPL1234C7Z8"),
            new Company(18, "Milestone Systems", "27AAAPL1234C8Z9"),
            new Company(19, "Evolutionary Tech Co.", "27AAAPL1234C9Z0"),
            new Company(20, "Eagle Eye Software", "27AAAPL1234C0Z2")
        };

        private static Random random = new Random();

        public static List<Bill> billList = GenerateSampleBill();
        
        public static List<Bill> GenerateSampleBill()
        {
            List<Bill> BillList = new List<Bill>();

            for (int i = 1; i <= 20; i++)
            {
                Company seller = companyList[random.Next(companyList.Count)];
                Company purchaser = companyList[random.Next(companyList.Count)];

                int numberOfItems = random.Next(1, 5); // Random number of products per bill (1 to 4)
                List<ProductItem> products = new List<ProductItem>();

                for (int j = 0; j < numberOfItems; j++)
                {
                    Products product = productList[random.Next(productList.Count)];
                    int quantity = random.Next(1, 5); // Random quantity between 1 to 4
                    products.Add(new ProductItem(product.ProductId, quantity));
                }

                double totalAmount = CalculateTotalAmount(products);

                Bill bill = new Bill(i, DateTime.Now.Date.AddDays(-i), seller.CompanyId, purchaser.CompanyId, products, totalAmount);

                BillList.Add(bill);

            }

            return BillList;
        }

        private static double CalculateTotalAmount(List<ProductItem> products)
        {
            double total = 0;

            foreach (var item in products)
            {
                // Retrieve the product from productList
                Products product = productList.Find(p => p.ProductId == item.Id);

                // Ensure both Price and Quantity are not null
                if (product.Price != null && item.Quantity != null)
                {
                    total += (double)product.Price * item.Quantity;
                }
            }

            return total;
        }

    }
}
