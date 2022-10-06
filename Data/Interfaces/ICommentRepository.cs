using Models;

namespace Data.Interfaces
{
    public interface ICommentRepository
    {
        public int? AddComment(Comment comment);
        public bool RemoveComment(int idComment);
    }
}