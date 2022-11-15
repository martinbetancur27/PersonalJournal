using Models;

namespace IService
{
    public interface IComment
    {
        public int? Add(Comment comment);
        public bool Remove(int idComment);
        public IEnumerable<Comment>? GetOfNote(int idNote);
    }
}