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

        public int? Add(Journal journal)
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

        public Journal? Get(int idJournal)
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

        public bool Edit(Journal journal)
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

        public bool Delete(int idJournal)
        {
            try
            {
                var journalDb = Get(idJournal);
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

        public IEnumerable<Journal>? GetOfUser(string idUser)
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
    }
}