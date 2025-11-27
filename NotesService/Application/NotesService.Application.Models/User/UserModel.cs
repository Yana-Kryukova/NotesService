using NotesService.Application.Models.Base;

namespace NotesService.Application.Models.User;

public record class UserModel(Guid Id, string Username) : IModel<Guid>;

