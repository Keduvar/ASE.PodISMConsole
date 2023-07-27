using NUnit.Framework;
using System.Collections.Generic;

namespace Convert_csv_to_json.Tests
{
    [TestFixture]
    public class CsvHelperTest
    {
        [Test]
        public void IsValidCsv_ValidCsv_ReturnsTrue()
        {
            // Arrange
            string validCsv = "header1;header2\nvalue1;value2";

            // Act
            bool result = CsvHelper.IsValidCsv(validCsv);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsValidCsv_InvalidCsv_ReturnsFalse()
        {
            // Arrange
            string invalidCsv = "header1,header2\nvalue1;value2";

            // Act
            bool result = CsvHelper.IsValidCsv(invalidCsv);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
