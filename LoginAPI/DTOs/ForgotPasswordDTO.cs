using System.ComponentModel.DataAnnotations;

namespace LoginAPI.DTOs
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
