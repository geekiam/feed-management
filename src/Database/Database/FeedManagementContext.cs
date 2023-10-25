using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Threenine.Configurations.PostgreSql;
using Threenine;

namespace Database.FeedManagements;

public class FeedManagementContext : BaseContext<FeedManagementContext>
{
    public FeedManagementContext(DbContextOptions<FeedManagementContext> options)
        : base(options)
    {
    }
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchema.Name);
        modelBuilder.HasPostgresExtension(PostgreExtensions.UUIDGenerator);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}