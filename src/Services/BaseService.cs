using Geekiam.Persistence;

namespace Services;

public abstract class BaseService
{
    private readonly FeedsDbContext _dbContext;

    protected BaseService(FeedsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task Save<T>(T entity, CancellationToken cancellationToken) where T : class
    {
         _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task Insert<T>(T entity, CancellationToken cancellationToken) where T : class
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Insert<T>(IEnumerable<T> entities, CancellationToken cancellationToken) where T : class
    {
        await _dbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}