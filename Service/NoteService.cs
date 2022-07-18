using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService;
using Data;
using Models;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class NoteService : IPostComposite
    {
        private readonly ApplicationDbContext _databaseContext;

        public NoteService(ApplicationDbContext db)
        {
            _databaseContext = db;
        }


        public bool AddPost<Post>(Post post, int? idPostRoot) where Post : class
        {
            try
            {
                if (typeof(Post).Equals(typeof(Comment)))
                {
                    Comment? comment = post as Comment;

                    if(idPostRoot != null)
                    {
                        comment.IdNote = idPostRoot.GetValueOrDefault(); // GetValueOrDefault(); para pasar de int? a int
                        comment.CreateDate = DateTime.Now;
                        _databaseContext.Comments.Add(comment);
                        _databaseContext.SaveChanges();
                        return true;
                    }                   
                }
                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }


        public bool EditPost<Post>(Post post) where Post : class
        {
            try
            {
                
                if (typeof(Post).Equals(typeof(Note)))
                {
                    Note? note = post as Note;
                    _databaseContext.Notes.Update(note);
                    _databaseContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }


        public bool DeletePost(int? idPost)
        {
            try
            {
                var post = _databaseContext.Notes.Find(idPost);
                if (post == null)
                {
                    return false;
                }
                
                DeleteSubPosts(idPost);
                
                _databaseContext.Notes.Remove(post);
                _databaseContext.SaveChanges();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }


        public bool DeleteSubPosts(int? idPost)
        {
            try
            {
                var post = _databaseContext.Notes.Find(idPost);
                if (post == null)
                {
                    return false;
                }
                
                IEnumerable<Comment> subPost = _databaseContext.Comments.Where(x => x.IdNote == idPost);

                foreach (var comment in subPost)
                {
                    _databaseContext.Comments.Remove(comment);
                }
                
                _databaseContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public Post? ShowPost<Post>(int? idPost) where Post : class
        {
            try
            {
                if (idPost == 0)
                {
                    return null;
                }
                //Note postFromDb = _databaseContext.Notes.Where(x => x.IdNote == idPost).Include(c => c.Comments).FirstOrDefault();

                Note postFromDb = _databaseContext.Notes.FirstOrDefault(x => x.IdNote == idPost);
                
                if (postFromDb == null)
                {
                    return null;
                }

                Post? note = postFromDb as Post;

                return note;
            }
            catch (Exception)
            {
                return null;
            }  
        }


        public IEnumerable<Post> GetListSubPosts<Post>(int? idRoot) where Post : class
        {
            try
            {
                IEnumerable<Comment> commentsList = _databaseContext.Comments.Where(x => x.IdNote == idRoot);
                return (IEnumerable<Post>)commentsList;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public int? GetNumberContainedPost(int? idRoot)
        {
            try
            {
                int numberContainedPost = _databaseContext.Comments.Where(x => x.IdNote == idRoot).Count();
                return numberContainedPost;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}