using NotesService.ValueObjects.Base;
using NotesService.ValueObjects.Exceptions;

namespace NotesService.ValueObjects.Validators;

/// <summary>
/// Defines a method that implements the validation of the string.
/// </summary>
public class UsernameValidator : IValidator<string>
{
    /// <summary>
    /// The Username's max length
    /// </summary>
    public static int MAX_LENGTH => 30;

    /// <summary>
    /// The Username's min length
    /// </summary>
    public static int MIN_LENGTH => 3;

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
            throw new ArgumentLongValueException(nameof(value),value, MAX_LENGTH);

        if (value.Length < MIN_LENGTH)
            throw new ArgumentShortValueException(nameof(value), value, MIN_LENGTH);
    }
}
