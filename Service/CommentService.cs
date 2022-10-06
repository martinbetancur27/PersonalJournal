using Data.Interfaces;
using IService;
using Models;

namespace Service
{
    public class CommentService : IComment
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public int? AddComment(Comment comment)
        {
            return _commentRepository.AddComment(comment);
        }

        public bool RemoveComment(int idComment)
        {
            return _commentRepository.RemoveComment(idComment);
        }
    }
}