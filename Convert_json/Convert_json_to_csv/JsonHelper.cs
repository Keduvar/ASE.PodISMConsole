using System.Text.Json;

namespace Convert_json_to_csv
{
    public class JsonHelper
    {
        public static void ConvertJsonToCsv(string jsonFilePath, string csvFilePath)
        {
            try
            {
                var json = FileSystemHelper.ReadAllText(jsonFilePath);

                if (IsValidJson(json))
                {
                    var model = JsonSerializer.Deserialize<ModelJson>(json);

                    if (model != null)
                    {
                        UpdateChildParents(model.Processes, null);
                        CsvHelper.ConvertToCsv(model.Processes, csvFilePath);
                        Console.WriteLine("Конвертация JSON в CSV завершена");
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
            catch (JsonException ex)
            {
                Console.WriteLine($"Ошибка при конвертации JSON в CSV: {ex.Message}");
            }
        }

        public static bool IsValidJson(string json)
        {
            try
            {
                using (JsonDocument.Parse(json))
                {
                    return true;
                }
            }
            catch (JsonException)
            {
                return false;
            }
        }

        public static void UpdateChildParents(List<Process> processes, Process parent)
        {
            foreach (var process in processes)
            {
                process.Up_id = parent?.Id;
                if (process.Chields != null)
                {
                    UpdateChildParents(process.Chields, process);
                }
            }
        }
    }
}
