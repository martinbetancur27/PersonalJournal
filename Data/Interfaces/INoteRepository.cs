using Models;

namespace Data.Interfaces
{
    public interface INoteRepository
    {
        public int? AddNote(Note note);
        public Note? FindNote(int idNote);
        public bool EditNote(Note note);
        public bool RemoveNote(int idNote);
        public IEnumerable<Comment>? GetCommentsOfNote(int idNote);
    }
}