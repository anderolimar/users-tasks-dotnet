using System.Text.Json.Serialization;

namespace UsersTasks.Models.Dto
{
    public class NewUser
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
