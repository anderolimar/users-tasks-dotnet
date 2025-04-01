using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace UsersTasks.Models
{
    public class UserTask
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName ("name")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserTaskStatus Status { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }

    public enum UserTaskStatus {
        [JsonStringEnumMemberName("pending")]
        PENDING,
        [JsonStringEnumMemberName("in_progress")]
        IN_PROGRESS,
        [JsonStringEnumMemberName("done")]
        DONE
    }
}

