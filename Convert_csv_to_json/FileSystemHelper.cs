using System.IO;

namespace Convert_csv_to_json
{
    public class FileSystemHelper
    {
        public static void EnsureDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
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
