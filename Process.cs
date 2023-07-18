using System.Text.Json.Serialization;

namespace ASE.PodISMConsole
{
    public class Process
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } // Идентификатор процесса

        [JsonPropertyName("up_id")]
        public string Up_id { get; set; } // Идентификатор родительского процесса

        [JsonPropertyName("title")]
        public string Title { get; set; } // Заголовок процесса

        [JsonPropertyName("parentProcess")]
        public string OwnerGroupProcess { get; set; } // Владелец группы процессов

        [JsonPropertyName("devBy")]
        public string Methodologist { get; set; } // Методолог, отвечающий за процесс

        [JsonPropertyName("generalInfoName")]
        public string GeneralInfoName { get; set; } // Общая информация о процессе

        [JsonPropertyName("distributionArea")]
        public string DistributionArea { get; set; } // Область распространения процесса

        [JsonPropertyName("justificationOrder")]
        public string JustificationOrder { get; set; } // Порядок обоснования процесса

        [JsonPropertyName("processGroupMetrics")]
        public string ProcessGroupMetrics { get; set; } // Метрики группы процессов

        [JsonPropertyName("linkProcessMap")]
        public string LinkProcessMap { get; set; } // Ссылка на карту процесса

        [JsonPropertyName("link")]
        public string Link { get; set; } // Ссылка на процесс

        [JsonPropertyName("Chield")]
        public List<Process> Chield { get; set; } = new List<Process>(); // Дочерние процессы
    }
}