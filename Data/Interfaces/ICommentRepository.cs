using Models;

namespace Data.Interfaces
{
    public interface ICommentRepository
    {
        public int? Add(Comment comment);
        public bool Remove(int idComment);
        public IEnumerable<Comment>? GetOfNote(int idNote);
    }
}