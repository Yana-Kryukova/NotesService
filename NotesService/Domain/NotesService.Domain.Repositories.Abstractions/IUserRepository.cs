using NotesService.Domain.Repositories.Abstractions.Base;

namespace NotesService.Domain.Repositories.Abstractions;

public interface IUserRepository : IRepository<User, Guid>
{
    // Так как имя пользователя уникальное
    Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
}

