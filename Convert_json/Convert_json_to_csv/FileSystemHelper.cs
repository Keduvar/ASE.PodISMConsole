using System.IO;

namespace Convert_json_to_csv
{
    public class FileSystemHelper
    {
        public static void EnsureDirectoryExists(string directoryPath)
        {
            DirectoryInfo dirInfo = new(directoryPath);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static string ReadAllText(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
