using NotesService.Application.Models.Base;

namespace NotesService.Application.Models.Note;

public record class NoteModel(
    Guid Id, 
    Guid UserId,
    string? Title,
    string Thesis,
    DateTime CreationDate,
    DateTime? ModificationDate) 
    : IModel<Guid>;
