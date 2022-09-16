using AlertSystem.Model;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace AlertSystem.Persistence
{
    public class AlertContext : DbContext
    { 
        public AlertContext(DbContextOptions<AlertContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder.UseSqlServer("AlertDb", builder =>
        //    {
        //        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        //    });

        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().Property(p => p.Amount).HasColumnType("decimal(18,2)");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
