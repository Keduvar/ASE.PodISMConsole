using NUnit.Framework;
using System.Text.Json;
using Convert_json_to_csv;

namespace Convert_json_to_csv.Tests
{
    [TestFixture]
    public class CsvHelperTests
    {
        private const string TestJsonFilePath = "test.json";
        private const string TestCsvFilePath = "test.csv";

        [OneTimeSetUp]
        public void Setup()
        {
            var processes = new List<Process>
            {
                new Process { Id = "1", Title = "Process 1", Chields = new List<Process>
                {
                    new Process { Id = "2", Title = "Child 1" },
                    new Process { Id = "3", Title = "Child 2", Chields = new List<Process>
                        {
                            new Process { Id = "4", Title = "Nested Child 1" },
                            new Process { Id = "5", Title = "Nested Child 2" }
                        }
                    }
                }}
            };

            var model = new ModelJson { Processes = processes };
            var jsonContent = JsonSerializer.Serialize(model);
            File.WriteAllText(TestJsonFilePath, jsonContent);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            if (File.Exists(TestJsonFilePath))
            {
                File.Delete(TestJsonFilePath);
            }

            if (File.Exists(TestCsvFilePath))
            {
                File.Delete(TestCsvFilePath);
            }
        }

        [Test]
        public void ConvertToCsv_Should_Handle_List_With_Empty_Values_Correctly()
        {
           List<Process> processs = new()
           {
            new Process {Id = "1", Title = "Process 1"},
            new Process {Id = "2", Title = "Process 2"},
            new Process {Id = "3", Title = "Process 3"},
           };
           CsvHelper.ConvertToCsv(processs, TestCsvFilePath);

           Assert.That(File.Exists(TestCsvFilePath), Is.True);
           string[] csvLines = File.ReadAllLines(TestCsvFilePath);
           Assert.That(csvLines, Has.Length.EqualTo(4));
        }

        [Test]
        public void ConvertToCsv_Should_Handle_Empty_List()
        {
           List<Process> processes = new();

           CsvHelper.ConvertToCsv(processes, TestCsvFilePath);

           Assert.That(File.Exists(TestCsvFilePath), Is.True);
           string[] csvLines = File.ReadAllLines(TestCsvFilePath);
           Assert.That(csvLines, Has.Length.EqualTo(1));
        }
    }
}
