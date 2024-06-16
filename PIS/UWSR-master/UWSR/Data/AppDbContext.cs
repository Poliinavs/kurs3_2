using Microsoft.EntityFrameworkCore;
using Lab7.Models;

namespace Lab7.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Links> Links { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
