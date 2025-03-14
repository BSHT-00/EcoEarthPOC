namespace LoginAPI.DTOs
{
    public class AuthenticateUser
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
