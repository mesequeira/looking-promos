using LookingPromos.SharedKernel.Domain.Categories.Entities;
using LookingPromos.SharedKernel.Domain.Networks.Entities;
using LookingPromos.SharedKernel.Domain.Stores.Entities;
using Microsoft.EntityFrameworkCore;

namespace LookingPromos.SharedKernel.Persistence.Abstractions.Contexts;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    private readonly bool _isDevelopment =
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "DEVELOPMENT";

    /// <summary>
    /// Override the method to implement automatically all the configurations of our entities.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Override method to enable the sensitive data logging only if the current environment is development to avoid leak sensitive information.
    /// </summary>
    /// <param name="optionsBuilder">An instance of the <see cref="DbContextOptionsBuilder"/> class used to configure the context.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_isDevelopment)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        base.OnConfiguring(optionsBuilder);
    }
    
    public DbSet<Network> Networks { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Store> Stores { get; set; }
}
