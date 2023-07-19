using System.Text.Json.Serialization;

namespace ASE.PodISMConsole
{
    public class Process
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("up_id")]
        public string Up_id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("parentProcess")]
        public string OwnerGroupProcess { get; set; }

        [JsonPropertyName("devBy")]
        public string Methodologist { get; set; }

        [JsonPropertyName("generalInfoName")]
        public string GeneralInfoName { get; set; }

        [JsonPropertyName("distributionArea")]
        public string DistributionArea { get; set; }

        [JsonPropertyName("justificationOrder")]
        public string JustificationOrder { get; set; }

        [JsonPropertyName("processGroupMetrics")]
        public string ProcessGroupMetrics { get; set; }

        [JsonPropertyName("linkProcessMap")]
        public string LinkProcessMap { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("Chield")]
        public List<Process> Chield { get; set; } = new List<Process>();
    }

    public class ModelJson
    {
        [JsonPropertyName("processes")]
        public List<Process> Processes { get; set; } = new List<Process>();
    }
}