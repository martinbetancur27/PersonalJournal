using Data.Interfaces;
using Models;

namespace Data
{
    public class JournalRepositorySQL : IJournalRepository
    {
        private readonly ApplicationDbContext _databaseContext;

        public JournalRepositorySQL(ApplicationDbContext db)
        {
            _databaseContext = db;
        }

        public int? AddJournal(Journal journal)
        {
            try
            {
                _databaseContext.Journals.Add(journal);
                _databaseContext.SaveChanges();

                return journal.IdJournal;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public Journal? GetJournal(int idJournal)
        {
            try
            {
                return _databaseContext.Journals.Find(idJournal);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public bool EditJournal(Journal journal)
        {
            try
            {
                _databaseContext.Journals.Update(journal);
                _databaseContext.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool DeleteJournal(int idJournal)
        {
            try
            {
                var journalDb = GetJournal(idJournal);
                if (journalDb == null)
                {
                    return false;
                }

                _databaseContext.Journals.Remove(journalDb);
                _databaseContext.SaveChanges();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public IEnumerable<Journal>? GetJournalsOfUser(string idUser)
        {
            try
            {
                return _databaseContext.Journals.Where(x => x.IdUser == idUser).OrderByDescending(d => d.CreateDate);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public IEnumerable<Note>? GetNotesOfJournal(int idJournal)
        {
            try
            {
                return _databaseContext.Notes.Where(x => x.IdJournal == idJournal).OrderByDescending(d => d.CreateDate);
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}