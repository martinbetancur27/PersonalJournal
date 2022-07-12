using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IService;
using Data;
using Models;

namespace Service
{
    public class JournalService : IPostRoot, IPostComposite
    {

        private readonly ApplicationDbContext _databaseContext;
        
        public JournalService(ApplicationDbContext db)
        {
            _databaseContext = db;
        }


        public bool Create<Post>(Post post, string idUser) where Post : class
        {
            try
            {
                if (typeof(Post).Equals(typeof(Journal)))
                {
                    Journal? journal = post as Journal;

                    journal.IdUser = idUser;
                    journal.CreateDate = DateTime.Now;
                    journal.LastEditDate = DateTime.Now;

                    _databaseContext.Journals.Add(journal); // Añadir IdUser como foreign key
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

        public IEnumerable<Post> GetList<Post>(string idUser) where Post : class
        {
            try
            {
                /*IEnumerable<Note> notesList = _databaseContext.Notes.Where(x => x.IdJournal == idRoot); //Añadir include.
                return (IEnumerable<IPost>)notesList;*/

                if (idUser != null)
                {
                    IEnumerable<Journal> notesList = _databaseContext.Journals.Where(x => x.IdUser == idUser);
                    return (IEnumerable<Post>?)notesList;
                }
                return null;

                /*IEnumerable<Note> notesList = _databaseContext.Notes.Where(x => x.IdJournal == idRoot).
                Include(x = x.Comments);*/ //Añadir include.

                
                //return (IEnumerable<IPost>)notesList;

            }
            catch (System.Exception)
            {
                return null;
            }
        }


        public bool AddPost<Post>(Post post, int? idPostRoot) where Post : class
        {
            try
            {
                if (typeof(Post).Equals(typeof(Note)))
                {
                    Note? note = post as Note;
                    
                    if(idPostRoot != null)
                    {
                        note.IdJournal = idPostRoot.GetValueOrDefault();
                        note.CreateDate = DateTime.Now;
                        note.LastEditDate = DateTime.Now;

                        _databaseContext.Notes.Add(note); // Añadir IdUser como foreign key
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
                if (typeof(Post).Equals(typeof(Journal)))
                    {
                                                                        
                        Journal? journal = post as Journal;
                        
                        _databaseContext.Journals.Update(journal);
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
                var post = _databaseContext.Journals.Find(idPost);
                if (post == null)
                {
                    return false;
                }

                _databaseContext.Journals.Remove(post);
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
            catch (System.Exception)
            {
                return false;
            }
        }


        public Post ShowPost<Post>(int? idJournal) where Post : class
        {
            try
            {
                Journal postFromDb = _databaseContext.Journals.FirstOrDefault(u => u.IdJournal == idJournal);
                
                if (postFromDb == null)
                {
                    return null;
                }

                Post journal = postFromDb as Post;

                return journal; 
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Post>? GetListSubPosts<Post>(int? idRoot) where Post : class
        {
            try
            {                
                
                IEnumerable<Note> notesList = _databaseContext.Notes.Where(x => x.IdJournal == idRoot);
                return (IEnumerable<Post>?)notesList;
                
            
                /*IEnumerable<Note> notesList = _databaseContext.Notes.Where(x => x.IdJournal == idRoot).
                Include(x = x.Comments);*/ //Añadir include.

                //return (IEnumerable<IPost>)notesList;
                  
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
                int numberContainedPost = _databaseContext.Notes.Where(x => x.IdJournal == idRoot).Count();
                return numberContainedPost;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}