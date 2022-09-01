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
using Models.ViewModels;

namespace Service
{
    public class JournalService : IPostRoot, IPostComposite
    {

        private readonly ApplicationDbContext _databaseContext;
        
        public JournalService(ApplicationDbContext db)
        {
            _databaseContext = db;
        }


        public int? Create<Post>(Post post, string idUser) where Post : class
        {
            try
            {
                /*if (typeof(Post).Equals(typeof(Journal)))
                {
                    Journal? journal = post as Journal;

                    _databaseContext.Journals.Add(journal); // Añadir IdUser como foreign key
                    _databaseContext.SaveChanges();
                
                    return true;
                }*/
                if (post.GetType().Equals(typeof(CreateJournalViewModel)))
                {
                    CreateJournalViewModel? createJournal = post as CreateJournalViewModel;

                    Journal journal = new Journal();
                    journal.IdUser = idUser;
                    journal.Title = createJournal.Title;
                    journal.Message = createJournal.Message;
                    journal.CreateDate = DateTime.Now;
                    journal.LastEditDate = DateTime.Now;

                    _databaseContext.Journals.Add(journal); // Añadir IdUser como foreign key
                    _databaseContext.SaveChanges();

                    return journal.IdJournal;
                }
            return 0;
            }
            catch (System.Exception)
            {
                return 0;
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

                DetailsJournalViewModel detailsJournal = new DetailsJournalViewModel();
                detailsJournal.IdJournal = postFromDb.IdJournal;
                detailsJournal.Title = postFromDb.Title;
                detailsJournal.Message = postFromDb.Message;
                detailsJournal.CreateDate = postFromDb.CreateDate;
                detailsJournal.LastEditDate = postFromDb.LastEditDate;

                Post journal = detailsJournal as Post;

                return journal;
            }
            catch (Exception)
            {
                return null;
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


        public int? AddPost<Post>(Post post, int? idPostRoot) where Post : class
        {
            try
            {
                if (typeof(Post).Equals(typeof(CreateNoteViewModel)))
                {
                    CreateNoteViewModel? createNote = post as CreateNoteViewModel;
                    
                    if(idPostRoot != null)
                    {
                        Note newNote = new Note();
                        newNote.IdJournal = idPostRoot.GetValueOrDefault();
                        newNote.Title = createNote.Title;
                        newNote.Message = createNote.Message;
                        newNote.CreateDate = DateTime.Now;
                        newNote.LastEditDate = DateTime.Now;

                        _databaseContext.Notes.Add(newNote);
                        _databaseContext.SaveChanges();
                        
                        return newNote.IdNote;
                    }
                }
                
                return 0;
            
            }
            catch (System.Exception)
            {
                return 0;
            }
        }


        public Post ShowEditPost<Post>(int? idJournal) where Post : class
        {
            try
            {
                Journal postFromDb = _databaseContext.Journals.FirstOrDefault(u => u.IdJournal == idJournal);

                if (postFromDb == null)
                {
                    return null;
                }

                EditJournalViewModels editJournal = new EditJournalViewModels();
                editJournal.IdJournal = postFromDb.IdJournal;
                editJournal.Title = postFromDb.Title;
                editJournal.Message = postFromDb.Message;

                Post journal = editJournal as Post;

                return journal;
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
                if (post.GetType().Equals(typeof(EditJournalViewModels)))
                {

                    EditJournalViewModels? editJournal = post as EditJournalViewModels;

                    Journal journal = _databaseContext.Journals.Find(editJournal.IdJournal);
                    journal.Title = editJournal.Title;
                    journal.Message = editJournal.Message;
                    journal.LastEditDate = DateTime.Now;

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