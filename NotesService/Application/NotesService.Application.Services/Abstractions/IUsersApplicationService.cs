using NotesService.Application.Models.User;
using NotesService.Application.Services.Abstractions.Base;

namespace NotesService.Application.Services.Abstractions;

public interface IUsersApplicationService : IApplicationService<UserModel, Guid>
{

    Task<UserModel?> GetUserByUsernameAsync(string username);
}
