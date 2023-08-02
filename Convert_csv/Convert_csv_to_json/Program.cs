using System;
using System.Text;

namespace Convert_csv_to_json
{
    class Program
    {
        public const string PathData = @".\data";
        public const string JsonFilePath = @".\data\db.json";
        public const string CsvFilePathProcess = @".\data\pm.content.ver40-main.csv";
        public const string CsvFilePathEmployee = @".\data\pm.content.ver40-employee.csv";

        static void Main()
        {
            try
            {
                FileSystemHelper.EnsureDirectoryExists(PathData);

                if (FileSystemHelper.FileExists(CsvFilePathProcess) && FileSystemHelper.FileExists(CsvFilePathEmployee))
                {
                    Console.WriteLine("Найдены CSV-файлы");

                    if (FileSystemHelper.FileExists(JsonFilePath))
                    {
                        Console.WriteLine("Файл JSON уже существует");
                    }
                    else
                    {
                        Console.WriteLine("Путь верный");

                        var csvProcess = FileSystemHelper.ReadAllText(CsvFilePathProcess);
                        var csvEmployee = FileSystemHelper.ReadAllText(CsvFilePathEmployee);

                        if (CsvHelper.IsValidCsv(csvProcess) && CsvHelper.IsValidCsv(csvEmployee))
                        {
                            var employee = CsvHelper.ReadEmployeesFromCsv(csvEmployee);
                            var processes = CsvHelper.ReadProcessFromCsv(csvProcess , employee);

                            if (processes != null && employee != null)
                            {
                                var jsonObject = new { processes};
                                var jsonProcess = JsonHelper.SerializeToJson(jsonObject);

                                if (JsonHelper.IsValidJson(jsonProcess))
                                {
                                    File.WriteAllText(JsonFilePath, jsonProcess, Encoding.UTF8);
                                    Console.WriteLine("CSV-файл успешно переведен в JSON и сохранен в папку data");
                                }
                                else
                                {
                                    Console.WriteLine("Не удалось сериализовать JSON-файл");
                                }
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Файл не найден: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода/вывода: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
