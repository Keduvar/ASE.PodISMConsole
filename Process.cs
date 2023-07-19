using System.Text.Json.Serialization;

namespace ASE.PodISMConsole
{
    public class Process
    {
        [JsonPropertyName("UID")]
        public string Id { get; set; }

        [JsonPropertyName("UpUID")]
        public string Up_id { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [JsonPropertyName("EmpParentProcess")]
        public string OwnerGroupProcess { get; set; }

        [JsonPropertyName("EmpDevBy")]
        public string Methodologist { get; set; }

        [JsonPropertyName("GeneralInfoName")]
        public string GeneralInfoName { get; set; }

        [JsonPropertyName("DistributionArea")]
        public string DistributionArea { get; set; }

        [JsonPropertyName("JustificationOrder")]
        public string JustificationOrder { get; set; }

        [JsonPropertyName("LinkProcessMap")]
        public string LinkProcessMap { get; set; }

        [JsonPropertyName("Link")]
        public string Link { get; set; }

        [JsonPropertyName("Chields")]
        public List<Process> Chields { get; set; } = new List<Process>();
    }

    public class ModelJson
    {
        [JsonPropertyName("processes")]
        public List<Process> Processes { get; set; } = new List<Process>();
    }
}