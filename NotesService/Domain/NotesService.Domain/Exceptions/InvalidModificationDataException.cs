namespace NotesService.Domain.Exceptions;

public class InvalidModificationDataException(Note note, DateTime modificationData)
    : ArgumentException($"The note time {modificationData} is not correct")
{
    public Note Note => note;
    public DateTime ModificationData => modificationData;
}
