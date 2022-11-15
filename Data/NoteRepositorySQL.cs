using Data.Interfaces;
using Models;

namespace Data
{
    public class NoteRepositorySQL : INoteRepository
    {
        private readonly ApplicationDbContext _databaseContext;

        public NoteRepositorySQL(ApplicationDbContext db)
        {
            _databaseContext = db;
        }

        public int? Add(Note note)
        {
            try
            {
                _databaseContext.Notes.Add(note);
                _databaseContext.SaveChanges();

                return note.IdNote;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public Note? Find(int idNote)
        {
            try
            {
                return _databaseContext.Notes.Find(idNote);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public bool Edit(Note note)
        {
            try
            {
                _databaseContext.Notes.Update(note);
                _databaseContext.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool Remove(int idNote)
        {
            try
            {
                Note? noteDb = Find(idNote);
                if (noteDb == null)
                {
                    return false;
                }

                _databaseContext.Notes.Remove(noteDb);
                _databaseContext.SaveChanges();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public IEnumerable<Note>? GetOfJournal(int idJournal)
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
