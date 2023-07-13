using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewProject.Domain;

namespace NewProject.EntityFrameworkCore;

public class MainDbContext : IdentityDbContext<Person, IdentityRole, string>
{
    public DbSet<Address> Addresses { get; set; }
    public MainDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // UserConfiguration
        new PersonConfiguration().Configure(builder.Entity<Person>());
        
        base.OnModelCreating(builder);
    }
}
