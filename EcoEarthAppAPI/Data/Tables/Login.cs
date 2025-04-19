using System.Security.Policy;

namespace EcoEarthAppAPI.Data.Tables
{
    // In the login table, the PK is a hash. 
    // This table is used to take that hash and give the user their progress using a numeric userId (which is used throughout this API)
    // More simply, LoginID (string hash) <-> UserId (int)
    public class Login
    {
        public required string LoginAPIId { get; set; }
        public required int UserId { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
