using NotesService.ValueObjects.Base;
using NotesService.ValueObjects.Exceptions;

namespace NotesService.ValueObjects.Validators;

/// <summary>
/// Defines a method that implements the validation of the string.
/// </summary>
public class TitleValidator : IValidator<string>
{

    /// <summary>
    /// The Title's max length
    /// </summary>
    public static int MAX_LENGTH => 50;

    /// <summary>
    /// Verifies the string to make sure it is not null, empty or doesn't consists only white-space characters. 
    /// </summary>
    /// <param name="value">A string containing data.</param>
    /// <exception cref="ArgumentNullOrWhiteSpaceException"></exception>
    /// <exception cref="ArgumentLongValueException"></exception>
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}