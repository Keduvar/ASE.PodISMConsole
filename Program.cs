using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using ASE.PodISMConsole;

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
                if (File.Exists(JsonFilePath))
                {
                    Console.WriteLine("Найден JSON-файл");

                    var json = File.ReadAllText(JsonFilePath);

                    if (IsValidJson(json))
                    {
                        var process = JsonSerializer.Deserialize<Process>(json);

                        if (process != null)
                        {
                            Console.WriteLine("JSON-файл успешно десериализован.");

                            UpdateChildParents(process.Chield, null);

                            // Продолжение кода для дальнейшей обработки данных
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
                process.OwnerGroupProcess = parent?.Id;

                if (process.Chield != null)
                {
                    UpdateChildParents(process.Chield, process);
                }
            }
        }
    }
}
