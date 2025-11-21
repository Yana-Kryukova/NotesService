using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NotesService.Domain.Base;

namespace NotesService.Domain
{
    public class Note : Entity
    {
        public Note()
        {
            throw new System.NotImplementedException();
        }

        public Title? Title { get; private set; }

        public Thesis Thesis { get; private set; }

        public DateTime CreationData { get; }

        public DateTime? ModificationData { get; private set; }

        public User User { get; }

        public bool SetTitle(Title newTitle)
        {
            throw new System.NotImplementedException();
        }

        public bool SetThesis(Thesis newThesis)
        {
            throw new System.NotImplementedException();
        }

        public bool SetModificationData(DateTime modificationData)
        {
            throw new System.NotImplementedException();
        }
    }
}