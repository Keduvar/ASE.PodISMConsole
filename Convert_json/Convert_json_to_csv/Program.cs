using System.Text.Json;

namespace Convert_json_to_csv
{
    class Program
    {
        public const string PathData = @".\data";
        public const string JsonFilePath = @".\data\db.json";
        public const string CsvFilePath = @".\data\pm.content.ver36.csv";

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

                        var json = FileSystemHelper.ReadAllText(JsonFilePath);

                        if (JsonToCsvConverter.IsValidJson(json))
                        {
                            var model = JsonSerializer.Deserialize<ModelJson>(json);

                            if (model != null)
                            {
                                Console.WriteLine("JSON-файл успешно десериализован.");

                                JsonToCsvConverter.UpdateChildParents(model.Processes, null);

                                JsonToCsvConverter.ConvertToCsv(model.Processes, CsvFilePath);
                            }
                            else
                            {
                                Console.WriteLine("Не удалось десериализовать JSON-файл.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("JSON-файл имеет некорректный формат.");
                        }
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
            catch (JsonException ex)
            {
                Console.WriteLine($"Ошибка десериализации JSON: {ex.Message}");
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
