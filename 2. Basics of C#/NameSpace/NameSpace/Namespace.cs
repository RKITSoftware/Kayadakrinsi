namespace OuterNamespace
{
    // namespace can contain class , interface , feild , records , another namespace etc.

    #region Private Members

    // namespace contains namespace
    namespace Accounts
    {
        #region Public Members

        // Class Account implements interface Printing Methods
        public class Account : IPrintingMethods
        {
            #region Private Members

            private long _accountNumber;

            #endregion


            #region Public Members

            public int id = 0;
            public string name;

            #endregion


            #region Constructors

            public Account() { }

            public Account(string name, long accountNumber)
            {
                id = ++id;
                this.name = name;
                _accountNumber = accountNumber;
            }

            #endregion


            #region Public Methods

            // Displays account details
            public void DisplayAccountDetails()
            {
                Console.WriteLine("Id : " + id);
                Console.WriteLine("Name : " + name);
                Console.WriteLine("Account number : It is not accessible due to security reasons right now.");
            }

            // Returns account number 
            public long GetAccountNumber()
            {
                return _accountNumber;
            }

            // Displays account number
            public void DisplayAccountNumber()
            {
                Console.WriteLine("Account number : " + GetAccountNumber());
            }

            #endregion

        }

        #endregion
    }

    #endregion

    #region Public Members

    // namespace contains class
    public class Namespace
    {
        #region Public Methods
        public static void Main(string[] args) 
        {
            Accounts.Account account = new Accounts.Account("Krinsi Kayada",1234567890L);
            account.DisplayAccountDetails();
            account.DisplayAccountNumber();
        }

        #endregion
    }

    // namespace contains interface
    public interface IPrintingMethods
    {
        void DisplayAccountDetails() { }

        void DisplayAccountNumber() { }
    }

    #endregion

}