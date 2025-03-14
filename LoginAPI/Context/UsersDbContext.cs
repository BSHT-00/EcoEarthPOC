using LoginAPI.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Context
{
    public partial class UserDBContext : IdentityDbContext<Users>
    {
        public new DbSet<Users> Users { get; set; }
        //public UserDBContext(DbContextOptions<UserDBContext> options)
        //    : base(options)
        //{
        //}

        public string DbPath { get; set; } = string.Empty;

        public UserDBContext()
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "AppUsers.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
