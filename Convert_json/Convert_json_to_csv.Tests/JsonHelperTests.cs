using System.Text.Json;
using NUnit.Framework;

namespace Convert_json_to_csv.Tests
{
    public class JsonHelperTests
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
                    new Process { Id = "3", Title = "Child 2" }
                }}
            };

            var model = new ModelJson { Processes = processes };
            var jsonContent = JsonSerializer.Serialize(model);
            File.WriteAllText(TestJsonFilePath, jsonContent);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            // Удаление тестовых файлов
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
        public void TestIsValidJson_ValidJson_ReturnsTrue()
        {
            var json = "{\"Name\": \"Ira\", \"Age\": 30}";

            bool isValid = JsonHelper.IsValidJson(json);

            Assert.That(isValid, Is.True);
        }

        [Test]
        public void TestIsValidJson_InvalidJson_ReturnsFalse()
        {
            var invalidJson = "invalid json";

            bool isInValid = JsonHelper.IsValidJson(invalidJson);

            Assert.That(isInValid, Is.False); 
        }

        [Test]
        public void UpdateChildParents_NestedProcesses_ChildParentsUpdatedRecursively()
        {
            var child1 = new Process { Id = "2", Title = "Child 1" };
            var child2 = new Process { Id = "3", Title = "Child 2" };
            var parent = new Process { Id = "1", Title = "Parent", Chields = new List<Process> { child1, child2 } };

            JsonHelper.UpdateChildParents(new List<Process> { parent }, null);

            Assert.That(parent.Up_id, Is.Null);
            Assert.Multiple(() =>
            {
                Assert.That(child1.Up_id, Is.EqualTo(parent.Id));
                Assert.That(child2.Up_id, Is.EqualTo(parent.Id));
            });
        }

        [Test]
        public void TestConvertJsonToCsv_ValidJson_CorrectCsvContent()
        {
            var json = "{\"Processes\": [{\"Id\": 1, \"Up_id\": null, \"Title\": \"Process 1\", \"Chields\": [{\"Id\": 2, \"Up_id\": 1, \"Title\": \"Process 2\", \"Chields\": []}, {\"Id\": 3, \"Up_id\": 1, \"Title\": \"Process 3\", \"Chields\": []}] }]}";
            File.WriteAllText(TestJsonFilePath, json);
            var jsonFilePath = TestJsonFilePath;
            var csvFilePath = TestCsvFilePath;
            var expectedCsvContent = "Id;Up_id;Title;EmployeeParentProcess;EmployeeDevBy;GeneralInfoName;DistributionArea;JustificationOrder;LinkProcessMap;Link\r\n";

            JsonHelper.ConvertJsonToCsv(jsonFilePath, csvFilePath);
            var actualCsvContent = File.ReadAllText(csvFilePath);

            Assert.That(actualCsvContent, Is.EqualTo(expectedCsvContent));
        }

        [Test]
        public void UpdateChildParents_SingleProcess_ChildParentUpdated()
        {
            var process = new Process { Id = "1", Title = "Process 1" };

            JsonHelper.UpdateChildParents(new List<Process> { process }, null);

            Assert.That(process.Up_id, Is.Null);
        }

        [Test]
        public void TestConvertJson_ValidJson_CreateCsvFile()
        {
            var jsonFilePath = TestJsonFilePath;
            var csvFilePath = TestCsvFilePath;

            JsonHelper.ConvertJsonToCsv(jsonFilePath, csvFilePath);
            Assert.That(File.Exists(csvFilePath), Is.True);
        }

        [Test]
        public void TestConvertJsonToCsv_InvalidJson_ThrowsException()
        {
            var invalidJson = "invalid json";
            var jsonFilePath = TestJsonFilePath;
            var csvFilePath = TestCsvFilePath;

            JsonHelper.ConvertJsonToCsv(jsonFilePath, invalidJson);

            Assert.That(File.Exists(csvFilePath), Is.True);
        }
    }
}