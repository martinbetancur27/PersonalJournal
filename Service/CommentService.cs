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

        public int? Add(Comment comment)
        {
            return _commentRepository.Add(comment);
        }

        public bool Remove(int idComment)
        {
            return _commentRepository.Remove(idComment);
        }

        public IEnumerable<Comment>? GetOfNote(int idNote)
        {
            return _commentRepository.GetOfNote(idNote);
        }
    }
}