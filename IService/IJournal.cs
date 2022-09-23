using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Models;

namespace IService
{
    public interface IJournal: ICompositeRepository<Journal>
    {
        
        public IEnumerable<Journal>? GetJournals(string idUser);
        public IEnumerable<Note> GetNotes(int? idPost);
    }
}