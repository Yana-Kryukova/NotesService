using AutoMapper;
using NotesService.Application.Models.Note;
using NotesService.Application.Models.User;
using NotesService.Application.Services.Abstractions;
using NotesService.Application.Services.Abstractions.Base;
using NotesService.Domain;
using NotesService.Domain.Repositories.Abstractions;
using NotesService.Domain.Repositories.Abstractions.Base;
using NotesService.ValueObjects;

namespace NotesService.Application.Services;

public class NotesApplicationService(
    IRepository<Note, Guid> noteRepository,
    IUserRepository userRepository, IMapper mapper)
    : IApplicationService<NoteModel, CreateNoteModel, Guid>
{
    public async Task<IEnumerable<NoteModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await noteRepository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<NoteModel>);

    public async Task<NoteModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var note = await noteRepository.GetByIdAsync(id, cancellationToken);
        return note is null ? null : mapper.Map<NoteModel>(note);
    }

    public async Task<NoteModel?> CreateModelAsync(CreateNoteModel noteInformation, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(noteInformation.UserId, cancellationToken);
        if (user is null)
            return null;

        var note = user.CreateNote(
            noteInformation.Title is null ? null : new Title(noteInformation.Title),
            new Thesis(noteInformation.Thesis));

        if (note is null)
            return null;

        var createdNote = await noteRepository.AddAsync(note, cancellationToken);
        return createdNote is null ? null : mapper.Map<NoteModel>(createdNote);
    }

    public async Task<bool> UpdateModelAsync(NoteModel noteInformation, CancellationToken cancellationToken = default)
    {
        var userTask = userRepository.GetByIdAsync(noteInformation.UserId, cancellationToken);
        var noteTask = noteRepository.GetByIdAsync(noteInformation.Id, cancellationToken);

        Task.WaitAll(userTask, noteTask);
        if (userTask.Result is null || noteTask.Result is not null)
            return false;

        var user = userTask.Result;
        var note = noteTask.Result;
        var editionNote = user.EditNote(
            note!, 
            noteInformation.Title is null ? null : new Title(noteInformation.Title),
            new Thesis(noteInformation.Thesis));
        
        if (editionNote is null)
            return false;

        return await noteRepository.UpdateAsync(editionNote, cancellationToken);
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await noteRepository.GetByIdAsync(id, cancellationToken);
        return user is null ? false : await noteRepository.DeleteAsync(user, cancellationToken);
    }
}
