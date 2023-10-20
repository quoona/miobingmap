using Microsoft.EntityFrameworkCore;
using MioMap.Models;

namespace MioMap
{
    public class MioMapDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "server=localhost;port=3306;database=miomap;user=root;password=123456";
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new System.Version(8, 0, 22)));
        }

        public DbSet<WaterClock> WaterClocks { get; set; }

        public DbSet<MioMap.Models.WaterPipline> WaterPiplines { get; set; } = default!;
    }
}
