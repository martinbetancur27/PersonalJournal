using System;
using System.Collections.Generic;
using Models;

namespace IService
{
    public interface IJournal
    {
        public int? Add(Journal journal);
        public Journal? Find(int idJournal);
        public bool Edit(Journal journal);
        public bool Remove(int idJournal);
        public IEnumerable<Journal>? GetOfUser(string idUser);
    }
}