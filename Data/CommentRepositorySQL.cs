using Data.Interfaces;
using Models;

namespace Data
{
    public class CommentRepositorySQL : ICommentRepository
    {
        private readonly ApplicationDbContext _databaseContext;

        public CommentRepositorySQL(ApplicationDbContext db)
        {
            _databaseContext = db;
        }

        public int? Add(Comment comment)
        {
            try
            {
                _databaseContext.Comments.Add(comment);
                _databaseContext.SaveChanges();

                return comment.IdComment;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public Comment? FindComment(int id)
        {
            try
            {
                return _databaseContext.Comments.Find(id);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public bool Remove(int idComment)
        {
            try
            {
                var commentDb = FindComment(idComment);
                if (commentDb == null)
                {
                    return false;
                }

                _databaseContext.Comments.Remove(commentDb);
                _databaseContext.SaveChanges();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public IEnumerable<Comment>? GetOfNote(int idNote)
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
