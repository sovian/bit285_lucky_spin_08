using System;
using Microsoft.EntityFrameworkCore;
namespace LuckySpin.Models
{
    public class LuckySpinContext : DbContext
    {
        public LuckySpinContext(DbContextOptions<LuckySpinContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Spin> Spins { get; set; }
    }
}
