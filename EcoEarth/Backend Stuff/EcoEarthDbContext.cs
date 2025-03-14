using EcoEarthPOC.Backend_Stuff.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoEarthPOC.Backend_Stuff
{
    public class EcoEarthDbContext : DbContext
    {
        public string DbPath { get; private set; }

        // Tables
        public DbSet<EcoEarthPOC.Backend_Stuff.Tables.UserCurrency> UserCurrency { get; set; }

        // Like this for testing purposes
        public EcoEarthDbContext()
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "EcoEarth.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source= {DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Relationships will go here v

            //
        }
    }
}
