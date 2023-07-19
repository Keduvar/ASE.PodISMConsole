using System.Text;
using System.Text.Json;

namespace ASE.PodISMConsole
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
                DirectoryInfo dirInfo = new(PathData);

                if(!dirInfo.Exists)
                {
                    dirInfo.Create();
                }


                if (File.Exists(JsonFilePath))
                {
                    Console.WriteLine("Найден JSON-файл");

                    if(File.Exists(CsvFilePath))
                    {
                        Console.WriteLine("Файл CSV уже существует");
                    }
                    else
                    {
                        Console.WriteLine("Путь верный");

                        var json = File.ReadAllText(JsonFilePath);

                        if (IsValidJson(json))
                        {
                            var model = JsonSerializer.Deserialize<ModelJson>(json);

                            if (model != null)
                            {
                                Console.WriteLine("JSON-файл успешно десериализован.");

                                UpdateChildParents(model.Processes, null);

                                ConvertToCsv(model);
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

        static bool IsValidJson(string json)
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

        static void UpdateChildParents(List<Process> processes, Process parent)
        {
            foreach (var process in processes)
            {
                process.Up_id = parent?.Id;

                if (process.Chield != null)
                {
                    UpdateChildParents(process.Chield, process);
                }
            }
        }

        static void ConvertToCsv(ModelJson modelJson)
        {
            var csvContent = new StringBuilder();

            var headers = typeof(Process).GetProperties();
            csvContent.AppendLine(string.Join("\t", headers.Select(p => p.Name)));

            foreach (var process in modelJson.Processes)
            {
                ConvertProcessToCsv(process, csvContent);
            }

            File.WriteAllText(CsvFilePath, csvContent.ToString(), Encoding.UTF8);
            Console.WriteLine("Конвертация JSON в CSV завершена");
        }

        private static void ConvertProcessToCsv(Process process, StringBuilder csvContent)
        {
            var properties = typeof(Process).GetProperties();
            var values = new List<string>();
            
            foreach (var property in properties)
            {
                var value = property.GetValue(process)?.ToString() ?? "";
                values.Add(value);
            }
        
            csvContent.AppendLine(string.Join("\t", values));

            if (process.Chield != null)
            {
                foreach (var child in process.Chield)
                {
                    ConvertProcessToCsv(child, csvContent);
                }
            }
        }
    }
}
