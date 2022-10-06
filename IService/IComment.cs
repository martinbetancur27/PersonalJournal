using Models;

namespace IService
{
    public interface IComment
    {
        public int? AddComment(Comment comment);
        public bool RemoveComment(int idComment);
    }
}