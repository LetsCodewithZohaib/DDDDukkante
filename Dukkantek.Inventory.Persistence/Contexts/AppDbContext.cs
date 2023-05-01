using Dukkantek.Inventory.Application.Interfaces.Persistence;
using Dukkantek.Inventory.Domain.Categories;
using Dukkantek.Inventory.Domain.Common;
using Dukkantek.Inventory.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Persistence.Contexts
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Entity> entry in ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }
            await DispatchEvents();
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            var decimalProps = modelBuilder.Model
    .GetEntityTypes()
    .SelectMany(t => t.GetProperties())
    .Where(p => (System.Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

            foreach (var property in decimalProps)
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        private async Task DispatchEvents()
        {
            var domainEventEntities = ChangeTracker.Entries<IHasDomainEvent>()
                 .Select(x => x.Entity.DomainEvents)
                 .SelectMany(x => x)
                 .Where(domainEvent => !domainEvent.IsPublished);
            foreach (var domainEvent in domainEventEntities)
            {
                domainEvent.IsPublished = true;
                // ToDo: Add Domain Event Service Here
                // await _domainEventService.Publish(domainEvent);
            }
        }
    }
}
