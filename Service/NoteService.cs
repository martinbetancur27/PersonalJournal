using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService;
using Data;
using Models;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;

namespace Service
{
    public class NoteService : IPostComposite
    {
        private readonly ApplicationDbContext _databaseContext;

        public NoteService(ApplicationDbContext db)
        {
            _databaseContext = db;
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

                ShowNoteViewModel showNote = new ShowNoteViewModel();
                showNote.IdNote = postFromDb.IdNote;
                showNote.IdJournal = postFromDb.IdJournal;
                showNote.Title = postFromDb.Title;
                showNote.Message = postFromDb.Message;
                showNote.CreateDate = postFromDb.CreateDate;
                showNote.LastEditDate = postFromDb.LastEditDate;

                Post? note = showNote as Post;

                return note;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public int? AddPost<Post>(Post post, int? idPostRoot) where Post : class
        {
            try
            {
                if (typeof(Post).Equals(typeof(CreateCommentViewModel)))
                {
                    CreateCommentViewModel? commentCreate = post as CreateCommentViewModel;

                    if (idPostRoot != null)
                    {
                        Comment comment = new Comment();
                        comment.IdNote = idPostRoot.GetValueOrDefault();
                        comment.Message = commentCreate.Message;
                        comment.CreateDate = DateTime.Now;

                        _databaseContext.Comments.Add(comment);
                        _databaseContext.SaveChanges();

                        return comment.IdNote;
                    }
                }
                
                return 0;
            }
            catch (System.Exception)
            {
                return 0;
            }
        }

        public Post ShowEditPost<Post>(int? idNote) where Post : class
        {
            try
            {
                Note postFromDb = _databaseContext.Notes.FirstOrDefault(u => u.IdNote == idNote);

                if (postFromDb == null)
                {
                    return null;
                }

                EditNoteViewModel editNote = new EditNoteViewModel();
                editNote.IdNote = postFromDb.IdNote;
                editNote.Title = postFromDb.Title;
                editNote.Message = postFromDb.Message;

                Post note = editNote as Post;

                return note;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public bool EditPost<Post>(Post post) where Post : class
        {
            try
            {
                if (post.GetType().Equals(typeof(EditNoteViewModel)))
                {
                    EditNoteViewModel? editNote = post as EditNoteViewModel;

                    Note note = _databaseContext.Notes.Find(editNote.IdNote);

                    note.Title = editNote.Title;
                    note.Message = editNote.Message;
                    note.LastEditDate = DateTime.Now;

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