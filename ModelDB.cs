using Microsoft.EntityFrameworkCore;

namespace Lab3
{
    public class ModelDB:DbContext
    {
        public ModelDB(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<PriceList>? PriceLists { get; set; }
        public DbSet<Registration>? Registrations { get; set; }
        public DbSet<User>? Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceList>().HasData(
                new PriceList { Id = 1, CodeBroadcast = "01", NameBroadcast = "Weather", PricePerMinute = "1000"},
                new PriceList { Id = 2, CodeBroadcast = "02", NameBroadcast = "Animal", PricePerMinute = "900" },
                new PriceList { Id = 3, CodeBroadcast = "03", NameBroadcast = "Films", PricePerMinute = "1200" },
                new PriceList { Id = 4, CodeBroadcast = "04", NameBroadcast = "Stand Up", PricePerMinute = "800" },
                new PriceList { Id = 5, CodeBroadcast = "05", NameBroadcast = "Reality Show", PricePerMinute = "500" },
                new PriceList { Id = 6, CodeBroadcast = "06", NameBroadcast = "WWN", PricePerMinute = "700" },
                new PriceList { Id = 7, CodeBroadcast = "07", NameBroadcast = "C#", PricePerMinute = "400" }
                );
            modelBuilder.Entity<Registration>().HasData(
                new Registration { Id = 1, DateBroadcast = new DateTime(2023, 12, 12), CodeBroadcast = "01", NameBroadcast = "Weather", Regularity = "2 times", TimeOnBroadcast = "15 minute", CostBroadcast = "30000", PriceListId = 1},
                new Registration { Id = 2, DateBroadcast = new DateTime(2023, 12, 13), CodeBroadcast = "01", NameBroadcast = "Weather", Regularity = "1 times", TimeOnBroadcast = "15 minute", CostBroadcast = "15000", PriceListId = 1 },
                new Registration { Id = 3, DateBroadcast = new DateTime(2023, 12, 14), CodeBroadcast = "01", NameBroadcast = "Weather", Regularity = "3 times", TimeOnBroadcast = "10 minute", CostBroadcast = "30000", PriceListId = 1 },
                new Registration { Id = 4, DateBroadcast = new DateTime(2023, 12, 15), CodeBroadcast = "01", NameBroadcast = "Weather", Regularity = "1 times", TimeOnBroadcast = "10 minute", CostBroadcast = "10000", PriceListId = 1 },
                new Registration { Id = 5, DateBroadcast = new DateTime(2023, 12, 16), CodeBroadcast = "02", NameBroadcast = "Animal", Regularity = "2 times", TimeOnBroadcast = "15 minute", CostBroadcast = "14000", PriceListId = 2 },
                new Registration { Id = 6, DateBroadcast = new DateTime(2023, 12, 17), CodeBroadcast = "03", NameBroadcast = "Films", Regularity = "1 times", TimeOnBroadcast = "15 minute", CostBroadcast = "15000", PriceListId = 3 },
                new Registration { Id = 7, DateBroadcast = new DateTime(2023, 12, 17), CodeBroadcast = "04", NameBroadcast = "Stand Up", Regularity = "3 times", TimeOnBroadcast = "10 minute", CostBroadcast = "18000", PriceListId = 4 },
                new Registration { Id = 8, DateBroadcast = new DateTime(2023, 12, 18), CodeBroadcast = "05", NameBroadcast = "Reality Show", Regularity = "1 times", TimeOnBroadcast = "10 minute", CostBroadcast = "23000", PriceListId = 5 },
                new Registration { Id = 9, DateBroadcast = new DateTime(2023, 12, 19), CodeBroadcast = "06", NameBroadcast = "WWN", Regularity = "3 times", TimeOnBroadcast = "10 minute", CostBroadcast = "22000", PriceListId = 6 },
                new Registration { Id = 10, DateBroadcast = new DateTime(2023, 12, 20), CodeBroadcast = "07", NameBroadcast = "C#", Regularity = "1 times", TimeOnBroadcast = "10 minute", CostBroadcast = "10000", PriceListId = 7 }
                );
            modelBuilder.Entity<User>().HasData(
                new User { id = 1, EMail = "1", Password = "1" }
                );
        }
    }
}
