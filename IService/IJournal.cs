﻿using System;
using System.Collections.Generic;
using Models;

namespace IService
{
    public interface IJournal
    {
        public int? AddJournal(Journal journal);
        public Journal? FindJournal(int idJournal);
        public bool EditJournal(Journal journal);
        public bool RemoveJournal(int idJournal);
        public IEnumerable<Journal>? GetJournalsOfUser(string idUser);
        public IEnumerable<Note>? GetNotesOfJournal(int idJournal);
    }
}