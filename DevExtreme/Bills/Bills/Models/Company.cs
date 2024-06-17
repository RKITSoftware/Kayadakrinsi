namespace Bills.Models
{
    public class Company
    {
        public int CompanyId { get; set; }

        public string Name { get; set; }

        public string GSTNumber { get; set; }  // New GST number field

        public Company(int companyId, string name, string gstNumber)
        {
            CompanyId = companyId;
            Name = name;
            GSTNumber = gstNumber;  // Initialize the new field in the constructor
        }
    }
}
