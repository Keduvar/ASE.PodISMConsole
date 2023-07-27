using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Convert_csv_to_json.Tests
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
            TestDirectory = Path.Combine(currentDirectory , "testDir");
            TestFile = "testFile.json";
        }

        [SetUp]
        public void Setup()
        {
            // Удаляем тестовую директорию перед каждым тестом (если она существует)
            if (Directory.Exists(TestDirectory))
            {
                Directory.Delete(TestDirectory, true);
            }

            // Создаем тестовую директорию заново перед каждым тестом
            Directory.CreateDirectory(TestDirectory);
        }

        [TearDown]
        public void Cleanup()
        {
            // Удаление тестового файла (если существует)
            var filePath = Path.Combine(TestDirectory, TestFile);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Удаление тестовой директории (если существует)
            if (Directory.Exists(TestDirectory))
            {
                Directory.Delete(TestDirectory, true);
            }
        }

        [Test]
        public void EnsureDirectoryExists_DirectoryExists_NothingChanges()
        {
            // Act
            FileSystemHelper.EnsureDirectoryExists(TestDirectory);

            // Assert
            Assert.That(Directory.Exists(TestDirectory), Is.True);
        }

        [Test]
        public void EnsureDirectoryExists_DirectoryDoesNotExist_CreatesDirectory()
        {
            // Arrange
            var newTestDirectory = Path.Combine(TestDirectory, "newTestDir");

            // Act
            FileSystemHelper.EnsureDirectoryExists(newTestDirectory);

            // Assert
            Assert.That(Directory.Exists(newTestDirectory), Is.True);
        }

        [Test]
        public void FileExists_ValidFilePath_ReturnsTrue()
        {
            // Arrange
            var filePath = Path.Combine(TestDirectory, TestFile);
            File.WriteAllText(filePath, "test content");

            // Act
            bool fileExists = FileSystemHelper.FileExists(filePath);

            // Assert
            Assert.That(fileExists, Is.True);
        }

        [Test]
        public void FileExists_FileDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var nonExistentFilePath = Path.Combine(TestDirectory, "nonExistentFile.txt");

            // Act
            bool fileExists = FileSystemHelper.FileExists(nonExistentFilePath);

            // Assert
            Assert.That(fileExists, Is.False);
        }

        [Test]
        public void ReadAllText_ValidFilePath_ReturnsFileContent()
        {
            // Arrange
            var filePath = Path.Combine(TestDirectory, TestFile);
            string expectedContent = "test content";
            File.WriteAllText(filePath, expectedContent);

            // Act
            string fileContent = FileSystemHelper.ReadAllText(filePath);

            // Assert
            Assert.That(fileContent, Is.EqualTo(expectedContent));
        }
    }
}
