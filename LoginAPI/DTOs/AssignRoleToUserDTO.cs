namespace LoginAPI.DTOs
{
    public class AssignRoleToUserDTO
    {
        public required string Email { get; set; }
        public required string RoleName { get; set; }
    }
}
