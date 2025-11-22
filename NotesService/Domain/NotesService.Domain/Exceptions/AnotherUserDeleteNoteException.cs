namespace NotesService.Domain.Exceptions;

public class AnotherUserDeleteNoteException(Note note, User user)
    : InvalidOperationException($"The user {user.Username} can't delete the {note.Title} note owned by the user  {note.User.Username} (note id = {note.Id}).")
{
    public Note Note => note;
    public User User => user;
}
