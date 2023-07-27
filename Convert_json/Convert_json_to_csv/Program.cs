using System;
using System.IO;

namespace Convert_json_to_csv
{
    class Program
    {
        public const string PathData = @".\data";
        public const string JsonFilePath = @".\data\db.json";
        public const string CsvFilePath = @".\data\pm.content.ver37-main.csv";

        static void Main()
        {
            try
            {
                FileSystemHelper.EnsureDirectoryExists(PathData);

                if (FileSystemHelper.FileExists(JsonFilePath))
                {
                    Console.WriteLine("Найден JSON-файл");

                    if (FileSystemHelper.FileExists(CsvFilePath))
                    {
                        Console.WriteLine("Файл CSV уже существует");
                    }
                    else
                    {
                        Console.WriteLine("Путь верный");

                        JsonToCsvConverter.ConvertJsonToCsv(JsonFilePath, CsvFilePath);
                    }
                }
                else
                {
                    Console.WriteLine("JSON-файл не найден");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Файл не найден: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
