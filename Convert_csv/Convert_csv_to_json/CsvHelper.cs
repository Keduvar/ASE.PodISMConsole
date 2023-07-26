using System;
using System.Collections.Generic;
using System.IO;

namespace Convert_csv_to_json
{
    public class CsvHelper
    {
        public static bool IsValidCsv(string csv)
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

        public static List<Process> ReadProcessFromCsv(string csv)
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

        public static List<Employee> ReadEmployeesFromCsv(string csv)
        {
            List<Employee> employees = new();
            Dictionary<string, Employee> processMap = new();

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

                    Employee employee = new()
                    {
                        Id = values[0],
                        FullName = values[1],
                        Name = values[2],
                        Surname = values[3],
                        Patronymic = values[4],
                        ServiceNumber = values[5],
                        Email = values[6],
                        Position = values[7],
                        Subdivision = values[8],
                        Organization = values[9]
                    };
 
                    employees.Add(employee);

                    processMap[employee.Id] = employee;

                    lineNumber++;
                } 
            }

            return employees;
        }
    }
}
