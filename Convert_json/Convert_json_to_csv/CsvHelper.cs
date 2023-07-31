using System.Text;

namespace Convert_json_to_csv
{
    public class CsvHelper
    {
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
