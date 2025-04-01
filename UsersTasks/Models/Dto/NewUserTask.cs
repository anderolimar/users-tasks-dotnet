using System.Text.Json.Serialization;
using UsersTasks.Models.Business;

namespace UsersTasks.Models.Dto
{
    public class NewUserTask
    {
        [JsonPropertyName("name")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserTaskStatus Status { get; set; }
    }
}
