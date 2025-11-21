using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NotesService.Domain.Base;

namespace NotesService.Domain
{
    /// <summary>
    /// Represents the user.
    /// </summary>
    public class User(Guid id, Username username) : Entity<Guid>(id)
    {
        /// <summary> 
        /// The user's notes.
        /// </summary>
        private readonly ICollection<Note> _notes = [];

        /// <summary> 
        /// Gets the user's Username. 
        /// </summary>
        public Username Username { get; private set; } = username ?? throw new ArgumentNullValueException(nameof(username));

        /// <summary>
        /// Gets the user's notes 
        /// </summary>
        public IReadOnlyCollection<Note> Notes =>
            _notes.ToList().AsReadOnly();

        /// <summary> 
        /// Changes the user's username. 
        /// </summary>
        /// <param name="newUsername">New user's username.</param>
        internal bool ChangeUsername(Username newUsername)
        {
            if (Username == newUsername) return false;
            Username = newUsername;
            return true;
        }

        public bool CreateNote(Title title, Thesis thesis)
        {
            throw new System.NotImplementedException();
        }

        public bool EditNote(Note note, Title newTitle, Thesis newThesis)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteNote(Note note)
        {
            throw new System.NotImplementedException();
        }
    }
}