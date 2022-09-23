using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using IService;
using Models;

namespace Service
{
    public class AuthorizeOwner : IAuthorizeOwner
    {
        private readonly ApplicationDbContext _databaseContext;

        public AuthorizeOwner(ApplicationDbContext db)
        {
            _databaseContext = db;
        }


        public bool IsOwnerJournal(int idJournal, string idUser)
        {
            Journal? journal = _databaseContext.Journals.FirstOrDefault(x => x.IdJournal == idJournal && x.IdUser.Equals(idUser));
            if (journal == null)
            {
                return false;
            }
            
            return true;
        }


        public bool IsOwnerNote(int idNote, string idUser)
        {
            Note? note = _databaseContext.Notes.Find(idNote);

            if (note == null)
            {
                return false;
            }

            Journal? journal = _databaseContext.Journals.FirstOrDefault(x => x.IdJournal == note.IdJournal && x.IdUser.Equals(idUser));
            if (journal == null)
            {
                return false;
            }

            return true;
        }
    }
}
