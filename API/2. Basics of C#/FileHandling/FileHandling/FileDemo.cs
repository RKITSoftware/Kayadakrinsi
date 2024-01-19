/// <summary>
/// FileHandlingDemo class is used to demonstrate how to impelement open, create, append, delete, etc file operations.
/// </summary>
public class FileHandlingDemo
{
    #region Public Methods

    public static void Main()
    {
        string path = @"C:\Users\KRINSHI\OneDrive\Documents\RKIT\C#\FileHandling\FileHandling\text\file1.txt";

        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("Hello ! ");
                sw.WriteLine("And...");
                sw.WriteLine("Welcome👋");
            }

            //File.WriteAllText(path, "This file created successfully.\nHello\nWelcome");
        }
        else
        {
            File.AppendAllText(path, "Appended Line One.\nAppended Line Two.");
        }

        using (StreamReader sr = File.OpenText(path))
        {
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                Console.WriteLine(s);
            }
        }

    }

    #endregion

}