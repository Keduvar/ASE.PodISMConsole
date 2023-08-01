using System.Diagnostics;
using System.Collections.Generic;
using System.Net;
using System.IO;
using NUnit.Framework;
using System.Text.Json;

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
        public void ConvertToCsv_Should_Return_Correct_Content_After_Conversion()
        {
            List<Process> processes;
            using (var fileStream = new StreamReader(TestJsonFilePath))
            {
                var jsonContent = fileStream.ReadToEnd();
                var model = JsonSerializer.Deserialize<ModelJson>(jsonContent);
                processes = model.Processes();
            }
            CsvHelperTests.ConvertToCsv(processes, TestCsvFilePath);

            Assert.IsTrue(File.Exists(TestCsvFilePath));
            string[] csvLines = File.ReadAllLines(TestCsvFilePath);
            Assert.AreEqual(5, csvLines.Length);

        }

        [Test]
        public void ConvertToCsv_Should_Handle_List_With_Empty_Values_Correctly()
        {
           List<Process> processs = new List<Process>
           {
            new Process {Id = "1", Title = "Process 1"},
            new Process {Id = "2", Title = "Process 2"},
            new Process {Id = "3", Title = "Process 3"},
           };
           CsvHelperTests.ConvertToCsv(processs, TestCsvFilePath);

           Assert.IsTrue(File.Exists(TestCsvFilePath));
           string[] csvLines = File.ReadAllLines(TestCsvFilePath);
           Assert.AreEqual(4, csvLinescsvLines.Length);
        }

        [Test]
        public void ConvertToCsv_Should_Handle_List_Without_Chields()
        {
           List<Process> processes = new List<Process>|
           {
            new Process {Id = "1", Title = "Process 1", Chields = null},
            new Process {Id = "2", Title = "Process 2", Chields = null},
           };
           CsvHelperTests.ConvertToCsv(processes, TestCsvFilePath);

           Assert.IsTrue(File.Exists(TestCsvFilePath));
           string[] csvLines = File.ReadAllLines(TestCsvFilePath);
           Assert.AreEqual(3, csvLines.Length);
        }

        [Test]
        public void ConvertToCsv_Should_Handle_Empty_List()
        {
           List<Process> processes = new List<Process>();

           CsvHelperTests.ConvertToCsv(processes, TestCsvFilePath);

           Assert.IsTrue(File.Exists(TestCsvFilePath));
           string[] csvLines = File.ReadAllLines(TestCsvFilePath);
           Assert.AreEqual(1, csvLines.Length);
        }

        [Test]
        public void ConvertToCsv_Should_Handle_List_With_Chields_Correctly()
        {
            List<Process> processes = new List<Process>
            {
                Id = "1",
                Title = "Process 1",
                Chields = new List<Process>
                {
                    new Process {Id = "2", Title = "Process 1"},
                    new Process {Id = "3", Title = "Process 2", Chields = null},
                }
            }
        };
        CsvHelperTests.ConvertToCsv(processes, TestCsvFilePath);

        Assert.IsTrue(File.Exists(TestsCsvFilePath));
        string[] csvLines = File.ReadAllLines(TestCsvFilePath);
        Assert.AreEqual(4, csvLines.Length);

    }
}
