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

        public int? AddNote(Note note)
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

        public Note? FindNote(int idNote)
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

        public bool EditNote(Note note)
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

        public bool RemoveNote(int idNote)
        {
            try
            {
                Note? noteDb = FindNote(idNote);
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

        public IEnumerable<Comment>? GetCommentsOfNote(int idNote)
        {
            try
            {
                return _databaseContext.Comments.Where(x => x.IdNote == idNote).OrderByDescending(d => d.CreateDate);
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
