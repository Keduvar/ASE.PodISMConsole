using System.Text.Json.Serialization;

namespace ASE.PodISMConsole
{
    public class Process 
    {
        public string UID { get; set; }

        public string UpUID { get; set; }

        public string Title { get; set; }

        public string EmpParentProcess { get; set; }

        public string EmpDevBy { get; set; }

        public string GeneralInfoName { get; set; }

        public string DistributionArea { get; set; }

        public string JustificationOrder { get; set; }

        public string LinkProcessMap { get; set; }

        public string Link { get; set; }

        public List<Process> Chields { get; set; }
    }
}