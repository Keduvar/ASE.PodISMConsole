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
                string[] lines = csv.Split('\n');
                if (lines.Length > 1)
                {
                    string[] headers = lines[0].Split(';');
                    if (headers.Length > 1)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<Process> ReadProcessFromCsv(string csv)
        {
            List<Process> processes = new();
            List<Process> rootProcesses = new();

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

                    Process process = CreateProcessFromCsv(values);
                    processes.Add(process);

                    if (string.IsNullOrEmpty(process.UpUID))
                    {
                        rootProcesses.Add(process);
                    }

                    lineNumber++;
                }

                foreach (var rootProcess in rootProcesses)
                {
                    BuildProcessHierarchy(rootProcess, processes);
                }
            }

            return rootProcesses;
        }

        private static Process CreateProcessFromCsv(string[] values)
        {
            string uid = values[0];
            string upUid = string.IsNullOrEmpty(values[1]) ? null : values[1];

            Process process = new()
            {
                UID = uid,
                UpUID = upUid,
                Chields = new List<Process>()
            };

            for (int i = 2; i < values.Length; i++)
            {
                if (values[i] != "")
                {
                    SetProcessFieldValue(process, i, values[i]);
                }
            }

            return process;
        }

        private static void SetProcessFieldValue(Process process, int index, string value)
        {
            switch (index)
            {
                case 2: process.Title = value; break;
                case 3: process.EmployeeParentProcess = value; break;
                case 4: process.EmployeeDevBy = value; break;
                case 5: process.GeneralInfoName = value; break;
                case 6: process.DistributionArea = value; break;
                case 7: process.JustificationOrder = value; break;
                case 8: process.LinkProcessMap = value; break;
                case 9: process.Link = value; break;
            }
        }

        private static void BuildProcessHierarchy(Process parentProcess, List<Process> allProcesses)
        {
            foreach (var process in allProcesses)
            {
                if (process.UpUID == parentProcess.UID)
                {
                    parentProcess.Chields.Add(process);
                    BuildProcessHierarchy(process, allProcesses);
                }
            }
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