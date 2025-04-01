using System.ComponentModel.DataAnnotations;

namespace UsersTasks.Models.Auth
{
    public class Token
    {
        [Required]
        public string AccessToken { get; set; } = string.Empty;

        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
