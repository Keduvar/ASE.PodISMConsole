using NUnit.Framework;

namespace Convert_csv_to_json.Tests
{
    [TestFixture]
    public class JsonHelperTests
    {
        [Test]
        public void IsValidJson_ValidJson_ReturnsTrue()
        {
            // Arrange
            string validJson = "{\"Name\":\"John\",\"Age\":30}";

            // Act
            bool result = JsonHelper.IsValidJson(validJson);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsValidJson_InvalidJson_ReturnsFalse()
        {
            // Arrange
            string invalidJson = "{\"Name\":\"John\",\"Age\":30";

            // Act
            bool result = JsonHelper.IsValidJson(invalidJson);

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void SerializeToJson_NullObject_ReturnsEmptyJsonString()
        {
            // Arrange
            object obj = null;

            // Act
            var jsonString = JsonHelper.SerializeToJson(obj);

            // Assert
            Assert.That(jsonString, Is.EqualTo("null"));
        }
    }
}
