using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ASE.PodISMConsole
{
    class Program
    {
        public const string PathData = @".\data";
        public const string JsonFilePath = @".\data\db.json";
        public const string CsvFilePathProcess = @".\data\pm.content.ver37-main.csv";
        
        static void Main()
        {
            try
            {
                DirectoryInfo dirInfo = new(PathData);

                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                if (File.Exists(CsvFilePathProcess)) //
                {
                    Console.WriteLine("Найден CSV-файл");

                    if (File.Exists(JsonFilePath))
                    {
                        Console.WriteLine("Файл JSON уже существует");
                    }
                    else
                    {
                        Console.WriteLine("Путь верный");

                        var csvProcess = File.ReadAllText(CsvFilePathProcess, Encoding.UTF8); //

                        if (IsValidCsv(csvProcess))
                        {
                            var processes = ReadEmployeesFromCsv(csvProcess); //

                            if (processes != null)
                            {
                                var json = SerializeToJson(processes);
                                
                                if(IsValidJson(json))
                                {
                                    File.WriteAllText(JsonFilePath, json, Encoding.UTF8);
                                    Console.WriteLine("CSV-файл успешно переведен в JSON и сохранен в папку data");
                                }

                            }
                            else
                            {
                                Console.WriteLine("Не удалось сериализовать JSON-файл");
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
        

        static bool IsValidCsv(string csv)
        {
            try
            {
                return csv.Contains(';');
            }
            catch (Exception)
            {
                return false;
            }
        }


        static List<Process> ReadEmployeesFromCsv(string csv)
        {
            List<Process> processes = new();
            Dictionary<string, Process> processMap = new();

            using (var reader = new StringReader(csv))
            {

                reader.ReadLine();

                int lineNumber = 2;
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    if (values.Length < 2)
                    {
                        Console.WriteLine($"Ошибка: Недостаточно столбцов в строке {lineNumber}. Пропускаем эту строку.");
                        lineNumber++;
                        continue;
                    }

                    Process process = new()
                    {
                        UID = values[0],
                        Chields = new List<Process>()
                    };

                    if(values[1] == "") process.UpUID = values[1] = "null";
                    else
                    {    
                        if (values.Length >= 2 && values[1] != null)
                            process.UpUID = values[1];
                    }

                    if(values[2] == ""){}
                    else
                    {
                        if (values.Length >= 3 && values[2] != null)
                            process.Title = values[2];
                    }

                    if(values[3] == ""){}
                    else
                    {
                        if (values.Length >= 4 && values[3] != null)
                            process.EmpParentProcess = values[3];
                    }
                    
                    if(values[4] == ""){}
                    else
                    {
                        if (values.Length >= 5 && values[4] != null)
                            process.EmpDevBy = values[4];
                    }

                    if(values[5] == ""){}
                    else
                    {
                        if (values.Length >= 6 && values[5] != null)
                            process.GeneralInfoName = values[5];
                    }

                    if(values[6] == ""){}
                    else
                    {
                        if (values.Length >= 7 && values[6] != null)
                            process.DistributionArea = values[6];
                    }

                    if(values[7] == ""){}
                    else
                    {
                        if (values.Length >= 8 && values[7] != null)
                            process.JustificationOrder = values[7];
                    }

                    if(values[8] == ""){}
                    else
                    {
                        if (values.Length >= 9 && values[8] != null)
                            process.LinkProcessMap = values[8];
                    }

                    if(values[9] == ""){}
                    else
                    {
                        if (values.Length >= 10 && values[9] != null)
                            process.Link = values[9];
                    }
                    
                    processes.Add(process);

                    processMap[process.UID] = process;

                    lineNumber++;
                }

                foreach (var process in processes)
                {
                    if (!string.IsNullOrEmpty(process.UpUID) && processMap.ContainsKey(process.UpUID))
                    {
                        var parentProcess = processMap[process.UpUID];
                        parentProcess.Chields.Add(process);
                    }
                }
            }
            
            return processes;
        }


        static string SerializeToJson(List<Process> processes)
        {
            JsonSerializerOptions jsonOptions = new()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var jsonObject = new { processes };
            
            return JsonSerializer.Serialize(jsonObject, jsonOptions);
        }
    }
}