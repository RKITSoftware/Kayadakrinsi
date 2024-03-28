namespace LinQFile
{
    public class Program
    {
        #region Public Members 

        /// <summary>
        /// Path of folder
        /// </summary>
        public static string startFolder = @"F:\Krinsi - 379\New folder\Advance C#\2. Advance C#\LinQ\LinQFile\LinQFile\Files";

        /// <summary>
        /// Takes a snapshot of the file system.
        /// </summary>        
        public static DirectoryInfo dir = new DirectoryInfo(startFolder);

        /// <summary>
        /// This method assumes that the application has discovery for all folders under the specified path. 
        /// </summary>        
        public static IEnumerable<FileInfo> fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);

        #endregion

        #region Public Methods

        /// <summary>
        /// It will produce full path for given extentions files under the specified folder including subfolders  
        /// It orders the list according to the file name. 
        /// </summary>
        public void FindFileByExtension()
        {
            
            IEnumerable<FileInfo> fileQuery =
                from file in fileList
                where file.Extension == ".txt"
                orderby file.Name
                select file;

            Console.WriteLine("Files with given extention");

            //Execute the query. This might write out a lot of files!  
            foreach (FileInfo fi in fileQuery)
            {
                Console.WriteLine(fi.FullName);
            }

            // Create and execute a new query by using the previous query as a starting point. 
            // fileQuery is not executed again until the call to Last()  
            var newestFile =
                (from file in fileQuery
                 orderby file.CreationTime
                 select new { file.FullName, file.CreationTime })
                .Last();

            Console.WriteLine("\r\nThe newest .txt file is {0}.\nCreation time: {1}\n",
                newestFile.FullName, newestFile.CreationTime);
        }

        /// <summary>
        /// Calculates file length in bytes
        /// </summary>
        /// <param name="filename">Filename of file whoose length will be claculated</param>
        /// <returns>length of file in bytes or zero if file not exist</returns>
        public long GetFileLength(string filename)
        {
            long lenFile;

            try
            {
                FileInfo fi = new FileInfo(filename);
                lenFile = fi.Length;
            }
            catch (FileNotFoundException)
            {
                lenFile = 0;
            }

            return lenFile;
        }

        /// <summary>
        /// Calculates total bytes in folder with file count and maximum length of bytes 
        /// under the specified folder including subfolders
        /// </summary>
        public void QuerySize()
        {
            var fileQuery = from file in fileList
                            select GetFileLength(file.FullName);

            // Cache the results to avoid multiple trips to the file system.  
            long[] fileLengths = fileQuery.ToArray();

            // Return the size of the largest file  
            long largestFile = fileLengths.Max();

            // Return the total number of bytes in all the files under the specified folder.  
            long totalBytes = fileLengths.Sum();

            Console.WriteLine("There are {0} bytes in {1} files under {2}",
                totalBytes, fileList.Count(), startFolder);

            Console.WriteLine("The largest files is {0} bytes.", largestFile);
        }

        #endregion

        public static void Main()
        {
            var objProgram = new Program();
            objProgram.FindFileByExtension();
            objProgram.QuerySize();
        }
        
    }
}
