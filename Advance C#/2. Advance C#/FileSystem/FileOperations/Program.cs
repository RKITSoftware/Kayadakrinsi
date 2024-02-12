class Program
{
    /// <summary>
    /// Performs file operations
    /// </summary>
    /// <param name="args">arguments if any</param>
    static void Main(string[] args)
    {
        string path = "F:\\Krinsi - 379\\Training\\Advance C#\\2. Advance C#\\FileSystem\\FileOperations\\Files\\";
        // File operations
        string filePath =  path + "testfile.txt";
        string newFilePath = path + "newtestfile.txt";

        // Create a file
        File.WriteAllText(filePath, "This is a test file.");

        // Copy a file
        File.Copy(filePath, newFilePath);

        // Read the contents of a file
        string fileContents = File.ReadAllText(newFilePath);
        Console.WriteLine("Contents of the new file:");
        Console.WriteLine(fileContents);

        // Delete a file
        File.Delete(newFilePath);

        // Directory operations
        string directoryPath = path + "TestDirectory";
        string newDirectoryPath = path +  "NewTestDirectory";

        // Create a directory
        Directory.CreateDirectory(directoryPath);

        // Copy a directory
        DirectoryCopy(directoryPath, newDirectoryPath, true);

        // Delete a directory and its contents
        // Directory.Delete(newDirectoryPath, true);

        Console.WriteLine("Operations completed successfully.");
    }

    /// <summary>
    /// Method to recursively copy a directory and its contents
    /// </summary>
    /// <param name="sourceDirName">Name of directory which being copied</param>
    /// <param name="destDirName">Name of directory where copy will be saved</param>
    /// <param name="copySubDirs">weather to copy sub directories or not</param>
    /// <exception cref="DirectoryNotFoundException">Throw if source directory not found</exception>
    static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    {
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);
        DirectoryInfo[] dirs = dir.GetDirectories();

        // If the source directory does not exist, throw an exception.
        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
        }

        // If the destination directory does not exist, create it.
        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string tempPath = Path.Combine(destDirName, file.Name);
            file.CopyTo(tempPath, false);
        }

        // If copying subdirectories, copy them and their contents to new location.
        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                string tempPath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
            }
        }
    }
}
