using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UsersTasks.Models.Dto
{
    public class NewUser
    {
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
