using AutoMapper;
using NotesService.Application.Models.User;
using NotesService.Application.Services.Abstractions;
using NotesService.Domain;
using NotesService.Domain.Repositories.Abstractions;
using NotesService.ValueObjects;

namespace NotesService.Application.Services;

public class UsersApplicationService(IUserRepository repository, IMapper mapper) : IUsersApplicationService
{
    public async Task<IEnumerable<UserModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<UserModel>);

    public async Task<UserModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await repository.GetByIdAsync(id, cancellationToken);
        return user is null ? null : mapper.Map<UserModel>(user);
    }

    public async Task<UserModel?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        var user = await repository.GetUserByUsernameAsync(username, cancellationToken);
        return user is null ? null : mapper.Map<UserModel>(user);
    }

    public async Task<UserModel?> CreateModelAsync(CreateUserModel userInformation, CancellationToken cancellationToken = default)
    {
        if (await repository.GetUserByUsernameAsync(userInformation.Username, cancellationToken) is not null)
            return null;

        User user = new(new Username(userInformation.Username));
        var createdUser = await repository.AddAsync(user, cancellationToken);
        return createdUser is null ? null : mapper.Map<UserModel>(createdUser);
    }

    public async Task<bool> UpdateModelAsync(UserModel userInformation, CancellationToken cancellationToken = default)
    {
        var userById = repository.GetByIdAsync(userInformation.Id, cancellationToken);
        var userByUsername = repository.GetUserByUsernameAsync(userInformation.Username, cancellationToken);

        Task.WaitAll(userById, userByUsername);
        if (userById.Result is null || userByUsername.Result is not null)
            return false;

        var user = userById.Result;
        if (user!.ChangeUsername(new(userInformation.Username)))
            return false;

        user = mapper.Map<User>(userInformation);
        return await repository.UpdateAsync(user, cancellationToken);
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await repository.GetByIdAsync(id, cancellationToken);
        return user is null ? false : await repository.DeleteAsync(user, cancellationToken);
    }
}
