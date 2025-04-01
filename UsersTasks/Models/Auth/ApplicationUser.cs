using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace UsersTasks.Models.Auth
{
    [Keyless]
    public class ApplicationUser : IdentityUser
    {
       public string Name { get; set; } 
    }
}
