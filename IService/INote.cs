using Models;

namespace IService
{
    public interface INote
    {
        public int? Add(Note note);
        public Note? Find(int idNote);
        public bool Edit(Note note);
        public bool Remove(int idNote);
        public IEnumerable<Note>? GetOfJournal(int idJournal);
    }
}