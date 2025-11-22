namespace NotesService.ValueObjects.Exceptions;

public class ArgumentNullOrWhiteSpaceException(string paramName)
: ArgumentNullException(paramName, $"The \"{paramName}\" of note mustn't be null, empty or consists only of white-space characters.");
