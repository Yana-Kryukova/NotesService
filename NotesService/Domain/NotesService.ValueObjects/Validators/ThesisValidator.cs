using NotesService.ValueObjects.Base;
using NotesService.ValueObjects.Exceptions;

namespace NotesService.ValueObjects.Validators;

   /// <summary>
   /// Defines a method that implements the validation of the string.
   /// </summary>
    public class ThesisValidator : IValidator<string>
{
    /// <summary>
    /// Verifies the string to make sure it is not null, empty or doesn't consists only white-space characters. 
    /// </summary>
    /// <param name="value">A string containing data.</param>
    /// <exception cref="ArgumentNullOrWhiteSpaceException"></exception>
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
    }
}