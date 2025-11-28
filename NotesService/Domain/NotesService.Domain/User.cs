using NotesService.Domain.Base;
using NotesService.Domain.Exceptions;
using NotesService.ValueObjects;

namespace NotesService.Domain
{
    /// <summary>
    /// Represents the user.
    /// </summary>
    public class User : Entity<Guid>
    {
        /// <summary> 
        /// The user's notes.
        /// </summary>
        private readonly ICollection<Note> _notes = [];

        /// <summary> 
        /// Gets the user's Username. 
        /// </summary>
        public Username Username { get; private set; } = default!;

        /// <summary>
        /// Gets the user's notes 
        /// </summary>
        public IReadOnlyCollection<Note> Notes =>
            _notes.ToList().AsReadOnly();

        protected User()
        {
            
        }

        public User(Username username) : this(Guid.NewGuid(), username)
        { }

        protected User(Guid id, Username username) : base(id)
        {
            Username = username ?? throw new ArgumentNullValueException(nameof(username));
        }

        /// <summary> 
        /// Changes the user's username. 
        /// </summary>
        /// <param name="newUsername">New user's username.</param>
        public bool ChangeUsername(Username newUsername)
        {
            if (newUsername == null) throw new ArgumentNullValueException(nameof(newUsername));

            if (Username == newUsername) return false;

            Username = newUsername;
            return true;
        }

        public Note CreateNote(Title? title, Thesis thesis)
        {
            var note = new Note(this, thesis, DateTime.UtcNow, title);
            _notes.Add(note);
            return note;
        }

        public Note? EditNote(Note note, Title? newTitle, Thesis newThesis)
        {
            if (note.User != this) throw new AnotherUserEditNoteException(note, this);

            if (!_notes.Contains(note)) throw new NoteNotBelongUserException(note, this);

            var isEdit = note.SetTitle(newTitle) || note.SetThesis(newThesis);

            if (isEdit) note.SetModificationData(DateTime.UtcNow);

            return isEdit ? note : null;
        }

        public void DeleteNote(Note note)
        {
            if (note.User != this) throw new AnotherUserDeleteNoteException(note, this);

            if (!_notes.Contains(note)) throw new NoteNotBelongUserException(note, this);

            _notes.Remove(note);

        }
    }
}