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
        public DbSet<UserTickets> UserTickets { get; set; }
        public DbSet<DailyStreak> DailyStreak { get; set; }


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
            modelBuilder.Entity<UserTickets>().HasKey(x => x.TicketId);

            modelBuilder.Entity<UserCurrency>().HasKey(x => x.UserId);
            modelBuilder.Entity<PastRecycledClassCount>().HasKey(x => x.UserId);
            modelBuilder.Entity<DailyStreak>().HasKey(x => x.UserId);

            // 1-1
            modelBuilder.Entity<UserCurrency>()
                .HasOne(uc => uc.UserProfile)
                .WithOne(up => up.UserCurrency)
                .HasForeignKey<UserProfile>(up => up.UserId);

            // 1-1
            modelBuilder.Entity<PastRecycledClassCount>()
                .HasOne(prcc => prcc.UserProfile)
                .WithOne(up => up.PastRecycledClassCount)
                .HasForeignKey<UserProfile>(up => up.UserId);

            // 1-1
            modelBuilder.Entity<DailyStreak>()
                .HasOne(prcc => prcc.UserProfile)
                .WithOne(up => up.DailyStreak)
                .HasForeignKey<UserProfile>(up => up.UserId);


            //modelBuilder.Entity<UserCurrency>()
            //    .HasOne(uc => uc.userProfile)
            //    .WithOne(up => up.userCurrency)
            //    .HasForeignKey<UserProfile>(up => up.UserId);

            //modelBuilder.Entity<PastRecycledClassCount>()
            //    .HasOne(prcc => prcc.userProfile)
            //    .WithOne(up => up.pastRecycledClassCount)
            //    .HasForeignKey<UserProfile>(up => up.UserId);




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
                new RecyclableMaterials { MaterialId = 22, Material = "paper and plastic", CategoryId = 1 },
                new RecyclableMaterials { MaterialId = 23, Material = "ldpe 4 low density polyethylene", CategoryId = 1},

                // Glass (2)
                new RecyclableMaterials { MaterialId = 8, Material = "clear glass", CategoryId = 2 },
                new RecyclableMaterials { MaterialId = 9, Material = "green glass", CategoryId = 2 },
                new RecyclableMaterials { MaterialId = 10, Material = "brown glass", CategoryId = 2 },
                new RecyclableMaterials { MaterialId = 11, Material = "glass", CategoryId = 2 },

                // Metal (3)
                new RecyclableMaterials { MaterialId = 12, Material = "aluminium", CategoryId = 3 },
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
                new UserCurrency { UserId = 1, Balance = 1000 },
                new UserCurrency { UserId = 2, Balance = 45 },
                new UserCurrency { UserId = 3, Balance = 24 },
                new UserCurrency { UserId = 4, Balance = 100 }
            );

            modelBuilder.Entity<UserProfile>().HasData(
                new UserProfile { UserId = 1 },
                new UserProfile { UserId = 2 },
                new UserProfile { UserId = 3 },
                new UserProfile { UserId = 4 }
            );

            modelBuilder.Entity<PastRecycledClassCount>().HasData(
                new PastRecycledClassCount { UserId = 1, Cat1 = 10, Cat2 = 0, Cat3 = 0, Cat4 = 0, Cat5 = 0 },
                new PastRecycledClassCount { UserId = 2, Cat1 = 0, Cat2 = 7, Cat3 = 6, Cat4 = 0, Cat5 = 0 },
                new PastRecycledClassCount { UserId = 3, Cat1 = 0, Cat2 = 0, Cat3 = 8, Cat4 = 0, Cat5 = 0 },
                new PastRecycledClassCount { UserId = 4, Cat1 = 1, Cat2 = 0, Cat3 = 2, Cat4 = 0, Cat5 = 6 }
            );

            modelBuilder.Entity<DailyStreak>().HasData(
                new DailyStreak { UserId = 1, TotalStreak = 5 },
                new DailyStreak { UserId = 2, TotalStreak = 3 },
                new DailyStreak { UserId = 3, TotalStreak = 6 },
                new DailyStreak { UserId = 4, TotalStreak = 2 }
            );

        }
    }
}
