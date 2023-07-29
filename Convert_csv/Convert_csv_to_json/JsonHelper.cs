using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;

namespace Convert_csv_to_json
{
    public class JsonHelper
    {
        public static bool IsValidJson(string json)
        {
            try
            {
                using (JsonDocument.Parse(json))
                {
                    return true;
                }
            }
            catch (JsonException)
            {
                return false;
            }
        }

        public static string SerializeToJson(object obj)
        {
            JsonSerializerOptions jsonOptions = new()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            return JsonSerializer.Serialize(obj, jsonOptions);
        }
    }
}
