using System.Security.Policy;

namespace EcoEarthAppAPI.Data.Tables
{
    public class Login
    {
        public int LoginAPIId { get; set; }
        public int UserId { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
