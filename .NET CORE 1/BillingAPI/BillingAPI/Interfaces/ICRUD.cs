using BillingAPI.Models;

namespace BillingAPI.Interfaces
{
    public interface ICRUD
    {
        public RES01 CreateTable();
        public RES01 Add();
        public RES01 Update();
        public RES01 Delete(int id);
        public RES01 Select();

    }
}
