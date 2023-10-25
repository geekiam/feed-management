
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database.FeedManagements;

internal class FeedManagementContextFactory : IDesignTimeDbContextFactory<FeedManagementContext>
{
    public FeedManagementContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<FeedManagementContext> dbContextOptionsBuilder =
            new();

        dbContextOptionsBuilder.UseNpgsql(ConnectionStringNames.LocalBuild);
        return new FeedManagementContext(dbContextOptionsBuilder.Options);
    }
}
