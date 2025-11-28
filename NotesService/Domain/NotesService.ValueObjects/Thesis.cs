using NotesService.ValueObjects.Base;
using NotesService.ValueObjects.Validators;

namespace NotesService.ValueObjects;

/// <summary>
/// Represents type of the entity's thesis.
/// </summary>
/// <param name="thesis">The thesis of the entity.</param>
public class Thesis(string thesis) : ValueObject<string>(new ThesisValidator(), thesis);