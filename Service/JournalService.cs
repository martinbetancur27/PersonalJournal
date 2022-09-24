using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IService;
using Data;
using Models;


namespace Service
{
    public class JournalService : CompositeRepository<Journal>, IJournal
    {

        private readonly ApplicationDbContext _databaseContext;
        
        public JournalService(ApplicationDbContext db) : base(db)
        {
            _databaseContext = db;
        }

        public bool CreateJournal(Journal journal)
        {
            try
            {
                _databaseContext.Journals.Add(journal);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public IEnumerable<Journal>? GetJournals(string idUser)
        {
            return _databaseContext.Journals.Where(x => x.IdUser == idUser).OrderByDescending(d => d.CreateDate);
        }


        public IEnumerable<Note> GetNotes(int? idJournal)
        {
            return _databaseContext.Notes.Where(x => x.IdJournal == idJournal).OrderByDescending(d => d.CreateDate);
        }
    }
}