using Microsoft.EntityFrameworkCore;
using NotesService.Domain;
using NotesService.Domain.Repositories.Abstractions;
using NotesService.Infrastructure.EntityFramework;
using NotesService.ValueObjects;

namespace AuctionTrading.Infrastructure.EntityFramework.RepositoriesEF;

public class EfSellerRepository(ApplicationDbContext context)
    : EfRepository<User, Guid>(context), IUserRepository
{
    private readonly DbSet<User> _users = context.Set<User>();

    public override Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _users.Include("_notes")
        .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
        => _users.Include("_notes")
        .FirstOrDefaultAsync(u => u.Username.Equals(new Username(username)), cancellationToken);
}