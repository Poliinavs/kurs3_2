using lab3b_vd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lab3b_vd.Data;

public class AppDbContext : IdentityDbContext<User>
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Name = "Administrator", NormalizedName = "Administrator" },
            new IdentityRole {  Name = "User", NormalizedName = "User" }
        );
    }
}
