using Microsoft.EntityFrameworkCore;
using NotesService.Domain.Base;
using NotesService.Domain.Repositories.Abstractions.Base;
using NotesService.Infrastructure.EntityFramework;

namespace NotesService.Infrastructure.EntityFramework.RepositoriesEF;

public class EfRepository<TEntity, TId>(ApplicationDbContext context)
        : IRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : struct, IEquatable<TId>
{
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        => await (asNoTracking ? context.Set<TEntity>().AsNoTracking() : context.Set<TEntity>())
        .ToListAsync(cancellationToken);

    public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
        => await context.Set<TEntity>().FindAsync(id, cancellationToken);

    public async Task<TEntity?> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        await context.Set<TEntity>().AddAsync(entity, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken) > 0 ? entity : null;
    }

    public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        context.Set<TEntity>().Update(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        context.Set<TEntity>().Remove(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(TId id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        return entity is null ? false : await DeleteAsync(entity, cancellationToken);

    }
}
