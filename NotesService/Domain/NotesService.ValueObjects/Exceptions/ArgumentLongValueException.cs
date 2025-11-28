namespace NotesService.ValueObjects.Exceptions;

public class ArgumentLongValueException(string paramName, string value,int maxLength)
: FormatException($"The \"{paramName}\" length {value} greater than maximum allowed length {maxLength}")
{
    public string Value => value;
    public int MaxLength => maxLength;
}