using NotesService.Application.Models.Base;

namespace NotesService.Application.Models.User;

public record class CreateUserModel(string Username) : ICreateModel;
