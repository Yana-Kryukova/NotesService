// See https://aka.ms/new-console-template for more information
using NotesService.Domain;
using NotesService.ValueObjects;

static void Print(Note note) 
    => Console.WriteLine($"Title: {note.Title}\t " +
        $"Creation Data: {note.CreationData}\t " +
        $"Modification Data: {note.ModificationData}\t" +
        $"Created by User {note.User.Username}\n" +
        $"Thesis: {note.Thesis}\n\n");


var user = new User(Guid.NewGuid(), new Username("Yana"));
Console.WriteLine($"Создан пользователь id = {user.Id} userName = {user.Username}");
user.CreateNote(new Title("Первая заметка"), new Thesis("Ура!"));
user.CreateNote(null, new Thesis("Заметка без заголовка"));
user.CreateNote(null, new Thesis("Заметка без заголовка2"));
foreach (var note in user.Notes)
    Print(note);

foreach (var note in user.Notes)
    if (note.Title is null) user.EditNote(note, new Title("Заголовок"), note.Thesis);

Console.WriteLine("После изменения\n");
foreach (var note in user.Notes)
    Print(note);

var firstNote = user.Notes.First();
user.DeleteNote(firstNote);

Console.WriteLine("После удаления\n");

foreach (var note in user.Notes)
    Print(note);