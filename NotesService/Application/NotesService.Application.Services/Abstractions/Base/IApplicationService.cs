using NotesService.Application.Models.Base;

namespace NotesService.Application.Services.Abstractions.Base;

public interface IApplicationService<TModel, in TId>
     where TModel : IModel<TId>
     where TId : struct, IEquatable<TId>
{
    Task<TModel> GetUserByIdAsync(TId id);

    Task<IEnumerable<TModel>> GetUsersAsync();

    Task<bool> CreateUserAsync(ICreateModel userInformation);

    Task<bool> UpdateUserAsync(TModel user);

    Task<bool> DeleteUserAsync(TId id);
}

