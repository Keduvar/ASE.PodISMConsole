using System.Text;
using System.Text.Json;

namespace Convert_json_to_csv
{
    public class JsonToCsvConverter
    {
        public static void ConvertJsonToCsv(string jsonFilePath, string csvFilePath)
        {
            var json = FileSystemHelper.ReadAllText(jsonFilePath);

            if (IsValidJson(json))
            {
                var model = JsonSerializer.Deserialize<ModelJson>(json);

                if (model != null)
                {
                    UpdateChildParents(model.Processes, null);

                    ConvertToCsv(model.Processes, csvFilePath);
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

        public static void ConvertToCsv(List<Process> processes, string csvFilePath)
        {
            var csvContent = new StringBuilder();

            var headers = typeof(Process).GetProperties().Where(p => p.Name != "Chields");
            csvContent.AppendLine(string.Join(";", headers.Select(p => p.Name)));

            foreach (var process in processes)
            {
                ConvertProcessToCsv(process, csvContent);
            }

            File.WriteAllText(csvFilePath, csvContent.ToString(), Encoding.UTF8);
            Console.WriteLine("Конвертация JSON в CSV завершена");
        }

        private static void ConvertProcessToCsv(Process process, StringBuilder csvContent)
        {
            var properties = typeof(Process).GetProperties().Where(p => p.Name != "Chields");
            var values = new List<string>();

            foreach (var property in properties)
            {
                var value = property.GetValue(process)?.ToString() ?? "";
                values.Add(value);
            }

            csvContent.AppendLine(string.Join(";", values));

            if (process.Chields != null)
            {
                foreach (var child in process.Chields)
                {
                    ConvertProcessToCsv(child, csvContent);
                }
            }
        }
    }
}
