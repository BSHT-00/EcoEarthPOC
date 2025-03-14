using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Microsoft.Data.Sqlite;
using EcoEarthAppAPI.Data.Tables;
using Microsoft.Extensions.DependencyModel.Resolution;

namespace EcoEarthAppAPI.Data
{
    public class EcoEarthAppAPIDbContext : DbContext
    {
        public string DbPath { get; private set; } = string.Empty;

        // Data Classes
        public DbSet<RecyclableMaterials> RecyclableMaterials { get; set; }
        public DbSet<UserCurrency> UserCurrency { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<PastRecycledClassCount> PastRecycledClassCount { get; set; }


        public EcoEarthAppAPIDbContext()
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "EcoEarthApp.db");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source= {DbPath}"); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Not related with the rest of the tables
            modelBuilder.Entity<RecyclableMaterials>().HasKey(x => x.MaterialId);


            // Relationships

            // CompositeKey relationship between UserProfile and UserCurrency
            modelBuilder.Entity<UserCurrency>()
            .HasKey(abc => new { abc.UserId, abc.Currency });

            // UserProfile (1) <-> PastRecycledClassCount (1)
            modelBuilder.Entity<PastRecycledClassCount>()
                .HasOne(e => e.userProfile)
                .WithOne(e => e.pastRecycledClassCount)
                .HasForeignKey<PastRecycledClassCount>(e => e.UserId);

            // UserProfile (1) <-> UserCurrency (1)
            modelBuilder.Entity<UserCurrency>()
                .HasOne(e => e.userProfile)
                .WithOne(e => e.userCurrency)
                .HasForeignKey<UserCurrency>(e => e.UserId);

            // Seed Data
            modelBuilder.Entity<RecyclableMaterials>().HasData(
                // Plastic (1)
                new RecyclableMaterials { MaterialId = 1, Material = "pet", CategoryId = 1 },
                new RecyclableMaterials { MaterialId = 2, Material = "hdpe", CategoryId = 1 },
                new RecyclableMaterials { MaterialId = 3, Material = "pvc", CategoryId = 1 },
                new RecyclableMaterials { MaterialId = 4, Material = "ldpe", CategoryId = 1 },
                new RecyclableMaterials { MaterialId = 5, Material = "pp", CategoryId = 1 },
                new RecyclableMaterials { MaterialId = 6, Material = "ps", CategoryId = 1 },
                new RecyclableMaterials { MaterialId = 7, Material = "plastic", CategoryId = 1 },

                // Glass (2)
                new RecyclableMaterials { MaterialId = 8, Material = "clear glass", CategoryId = 2 },
                new RecyclableMaterials { MaterialId = 9, Material = "green glass", CategoryId = 2 },
                new RecyclableMaterials { MaterialId = 10, Material = "brown glass", CategoryId = 2 },
                new RecyclableMaterials { MaterialId = 11, Material = "glass", CategoryId = 2 },

                // Metal (3)
                new RecyclableMaterials { MaterialId = 12, Material = "aluminum", CategoryId = 3 },
                new RecyclableMaterials { MaterialId = 13, Material = "steel", CategoryId = 3 },
                new RecyclableMaterials { MaterialId = 14, Material = "metal", CategoryId = 3 },

                // Paper (4)
                new RecyclableMaterials { MaterialId = 15, Material = "office paper", CategoryId = 4 },
                new RecyclableMaterials { MaterialId = 16, Material = "newspapers", CategoryId = 4 },
                new RecyclableMaterials { MaterialId = 17, Material = "magazines", CategoryId = 4 },
                new RecyclableMaterials { MaterialId = 18, Material = "paper", CategoryId = 4 },

                // Cardboard (5)
                new RecyclableMaterials { MaterialId = 19, Material = "corrugated cardboard", CategoryId = 5 },
                new RecyclableMaterials { MaterialId = 20, Material = "paperboard", CategoryId = 5 },
                new RecyclableMaterials { MaterialId = 21, Material = "cardboard", CategoryId = 5 }
            );

            modelBuilder.Entity<UserCurrency>().HasData(
                new UserCurrency { UserId = 1, Currency = 0 },
                new UserCurrency { UserId = 2, Currency = 45 },
                new UserCurrency { UserId = 3, Currency = 24 },
                new UserCurrency { UserId = 4, Currency = 100 }
            );

            modelBuilder.Entity<UserProfile>().HasData(
                new UserProfile { UserId = 1 },
                new UserProfile { UserId = 2 },
                new UserProfile { UserId = 3 },
                new UserProfile { UserId = 4 }
            );

            modelBuilder.Entity<PastRecycledClassCount>().HasData(
                new PastRecycledClassCount { UserId = 1, Cat1 = 0, Cat2 = 0, Cat3 = 0, Cat4 = 0, Cat5 = 0 },
                new PastRecycledClassCount { UserId = 2, Cat1 = 0, Cat2 = 0, Cat3 = 0, Cat4 = 0, Cat5 = 0 },
                new PastRecycledClassCount { UserId = 3, Cat1 = 0, Cat2 = 0, Cat3 = 0, Cat4 = 0, Cat5 = 0 },
                new PastRecycledClassCount { UserId = 4, Cat1 = 0, Cat2 = 0, Cat3 = 0, Cat4 = 0, Cat5 = 0 }
            );


        }
    }
}
