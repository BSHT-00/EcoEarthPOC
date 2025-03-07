using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LoginAPI.Data
{
    public class Users : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? RefreshToken { get; set; }
    }
}
