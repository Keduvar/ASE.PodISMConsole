using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Convert_json_to_csv.Tests
{
    [TestFixture]
    public class FileSystemHelperTests
    {
        private static readonly string TestDirectory;
        private static readonly string TestFile;

        static FileSystemHelperTests()
        {
            // Получаем текущий каталог исполняемой сборки
            var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            TestDirectory = Path.Combine(currentDirectory, "testDir");
            TestFile = "testFile.json";
        }

        [OneTimeSetUp]
        public void Setup()
        {
            // Создание тестовой директории
            Directory.CreateDirectory(TestDirectory);

            // Создание тестового JSON-файла внутри тестовой директории
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
            File.WriteAllText(Path.Combine(TestDirectory, TestFile), jsonContent);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            // Удаление тестового файла
            var filePath = Path.Combine(TestDirectory, TestFile);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Удаление тестовой директории
            if (Directory.Exists(TestDirectory))
            {
                Directory.Delete(TestDirectory);
            }
        }

        [Test]
        public void EnsureDirectoryExists_DirectoryExists_NothingChanges()
        {
            // Директория "testDir" была создана в фазе настройки (Setup)
            // Проверяем, что директория все еще существует после вызова метода
            FileSystemHelper.EnsureDirectoryExists(TestDirectory);
            Assert.That(Directory.Exists(TestDirectory), Is.True);
        }

        [Test]
        public void EnsureDirectoryExists_DirectoryDoesNotExist_DirectoryCreated()
        {
            const string newDirectory = "newDir";

            // Убеждаемся, что тестовая директория не существует перед вызовом метода EnsureDirectoryExists
            Assert.That(Directory.Exists(Path.Combine(TestDirectory, newDirectory)), Is.False);

            // Вызываем метод EnsureDirectoryExists для создания новой директории
            FileSystemHelper.EnsureDirectoryExists(Path.Combine(TestDirectory, newDirectory));

            // Проверяем, что новая директория была успешно создана
            Assert.That(Directory.Exists(Path.Combine(TestDirectory, newDirectory)), Is.True);

            // Удаление только что созданной директории во избежание загромождения файловой системы
            Directory.Delete(Path.Combine(TestDirectory, newDirectory));
        }

        [Test]
        public void FileExists_FileExists_ReturnsTrue()
        {
            // Тестовый файл "testFile.json" был создан в фазе настройки (Setup)
            var filePath = Path.Combine(TestDirectory, TestFile);

            // Проверяем существование тестового файла
            var result = FileSystemHelper.FileExists(filePath);

            // Утверждаем, что результат true, так как файл существует
            Assert.That(result, Is.True);
        }

        [Test]
        public void FileExists_FileDoesNotExist_ReturnsFalse()
        {
            // Убеждаемся, что тестовый файл не существует перед вызовом метода FileExists
            var filePath = Path.Combine(TestDirectory, "nonExistentFile.json");

            // Проверяем существование несуществующего файла
            var result = FileSystemHelper.FileExists(filePath);

            // Утверждаем, что результат false, так как файл не существует
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReadAllText_FileExists_ReturnsFileContent()
        {
            // Тестовый файл "testFile.json" был создан в фазе настройки (Setup)
            var filePath = Path.Combine(TestDirectory, TestFile);

            // Считываем содержимое тестового файла
            var content = FileSystemHelper.ReadAllText(filePath);

            // Утверждаем, что содержимое соответствует ожидаемому содержимому
            var expectedJsonContent = File.ReadAllText(filePath);
            Assert.That(content, Is.EqualTo(expectedJsonContent));
        }
    }
}
