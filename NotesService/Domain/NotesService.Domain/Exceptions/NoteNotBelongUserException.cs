namespace NotesService.Domain.Exceptions;

public class NoteNotBelongUserException(Note note, User user)
    : InvalidOperationException($"The note {note.Title} is not in the user's note sequence (user {user.Username}, note id = {note.Id}).")
{
    public Note Note => note;
    public User User => user;
}
