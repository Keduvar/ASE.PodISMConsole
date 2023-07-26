using System.Text.Json.Serialization;

namespace Convert_csv_to_json
{
    public class Employee 
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("FullName")]
        public string FullName { get; set; }
 
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Surname")]
        public string Surname { get; set; }

        [JsonPropertyName("Patronymic")]
        public string Patronymic { get; set; }

        [JsonPropertyName("ServiceNumber")]
        public string ServiceNumber {get; set; }
 
        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [JsonPropertyName("Position")]
        public string Position { get; set; }
 
        [JsonPropertyName("Subdivision")]
        public string Subdivision { get; set; }
     
        [JsonPropertyName("Organization")]
        public string Organization { get; set; }
    }
}