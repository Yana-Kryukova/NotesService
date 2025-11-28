using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NotesService.Domain.Base;
using NotesService.Domain.Exceptions;
using NotesService.ValueObjects;

namespace NotesService.Domain
{
    public class Note : Entity<Guid>
    {
        public Title? Title { get; private set; } = null;

        public Thesis Thesis { get; private set; } = default!;

        public DateTime CreationData { get; }

        public DateTime? ModificationData { get; private set; } = null;

        public User User { get; } = default!;

        protected Note()
        {
        }

        public Note(
            User user,
            Thesis thesis,
            DateTime creationData,
            Title? title = null)
            : this(Guid.NewGuid(), user, thesis, creationData, title) { }

        protected Note(Guid id,
            User user,
            Thesis thesis,
            DateTime creationData,
            Title? title = null,
            DateTime? modificationData = null)
            : base(id)
        {
            User = user ?? throw new ArgumentNullValueException(nameof(user));
            Thesis = thesis ?? throw new ArgumentNullValueException(nameof(thesis));
            Title = title;

            if (modificationData is not null && modificationData < creationData)
                throw new InvalidModificationDataException(this, modificationData.Value);

            CreationData = creationData;
            ModificationData = modificationData;
        }

        public bool SetTitle(Title newTitle)
        {
            if (Title == newTitle)
                return false;
            Title = newTitle;
            return true;
        }

        public bool SetThesis(Thesis newThesis)
        {
            if (Thesis == null) throw new ArgumentNullValueException(nameof(newThesis));
            if (Thesis == newThesis)
                return false;
            Thesis = newThesis;
            return true;
        }

        public bool SetModificationData(DateTime modificationData)
        {
            if (ModificationData == null) throw new ArgumentNullValueException(nameof(ModificationData));
            if (CreationData > modificationData) throw new InvalidModificationDataException(this, modificationData);
            if (ModificationData > modificationData) throw new InvalidModificationDataException(this, modificationData);
            if (ModificationData == modificationData)
                return false;
            ModificationData = modificationData;
            return true;
        }
    }
}