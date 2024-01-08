namespace AbstractionDemo
{
    // Abstract class AbstractUser
    public abstract class AbstractUser
    {
        public abstract void UserInfo();

        // Displays about of user
        public void AboutUser()
        {
            Console.WriteLine("This is about user.");
        }
    }

    // Interface IUSer
    public interface IUser
    {
        void UserInfo();
        void Greetings();

    }

    // Class User implements abstract class AbstractUser and interface IUser
    public class User : AbstractUser, IUser
    {
        // Override UserInfo() method and Displays information of User
        public override void UserInfo()
        {
            Console.WriteLine("This is first user.");
        }

        // Implements Greetings() method and Displays greeting message
        public void Greetings()
        {
            Console.WriteLine("Hello there !");
        }

    }


    public class ProgramAbstraction
    {
        public static void Main(string[] args)
        {
            User userOne = new User();
            userOne.UserInfo();
            userOne.AboutUser();
            userOne.Greetings();
        }
    }
}


