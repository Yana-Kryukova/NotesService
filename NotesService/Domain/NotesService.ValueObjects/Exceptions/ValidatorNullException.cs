namespace NotesService.ValueObjects.Exceptions;

public class ValidatorNullException(string paramName)
: ArgumentNullException(paramName, $"Validator \"{paramName}\" must be specified for type.");