using System.Text.Json.Serialization;

namespace ASE.PodISMConsole
{
    public class Process
    {
        [JsonPropertyName("UID")]
        public string UID { get; set; }

        [JsonPropertyName("UpUID")]
        public string UpUID { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [JsonPropertyName("EmpParentProcess")]
        public string EmpParentProcess { get; set; }

        [JsonPropertyName("EmpDevBy")]
        public string EmpDevBy { get; set; }

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
        public List<Process> Chields { get; set; }
    }
}