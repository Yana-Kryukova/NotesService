using NotesService.Application.Models.Base;

namespace NotesService.Application.Models.Note;

public record class CreateNoteModel(
    Guid UserId,
    string? Title,
    string Thesis,
    DateTime CreationDate)
    : ICreateModel;
