using Microsoft.AspNetCore.Mvc;
using Data;
using Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using IService;
using Service;
using System.Collections.Generic;


namespace VirtualJournalMVC.Controllers
{
    [Authorize]
    public class JournalController : Controller
    {
        private readonly IUserService _userService;
        private IPostRoot? _journalRoot;
        private IPostComposite? _journal;
        private IPostComposite? _note;
        private IPostLeaf? _comment;

        public JournalController(IUserService userService, IEnumerable<IPostComposite> postComposite, IEnumerable<IPostLeaf> postLeaf, IEnumerable<IPostRoot> postRoot)
        {
            
            _userService = userService;

            this._journalRoot = postRoot.SingleOrDefault(s => s.GetType() == typeof(JournalService));
            this._journal = postComposite.SingleOrDefault(s => s.GetType() == typeof(JournalService));
            this._note = postComposite.SingleOrDefault(s => s.GetType() == typeof(NoteService));
            this._comment = postLeaf.SingleOrDefault(s => s.GetType() == typeof(CommentService));
        }

        public IActionResult Index()
        {
            string idUser = _userService.GetUserId();

            IEnumerable<Journal> journalsIndex = _journalRoot.GetList<Journal>(idUser); //Parameter 0 is select idUser as Root
            return View(journalsIndex);
        }

        public IActionResult CreateJournal()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateJournal(Journal? journal)
        {
            bool response = _journalRoot.Create(journal, _userService.GetUserId());
            if(response)
            {
                
                return RedirectToAction("Notes", new { idJournal = journal.IdJournal });
            }

            //return RedirectToAction("Index");
            return View();
        }


        public IActionResult DetailsJournal(int id)
        {
            Journal post = _journal.ShowPost<Journal>(id);
            if(post == null)
            {
                return NotFound();
            }
            return View(post);
        }


        public IActionResult EditJournal(int id)
        {
            Journal post = _journal.ShowPost<Journal>(id);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditJournal(int IdJournal, Journal journal)
        {
            bool response = _journal.EditPost<Journal>(journal);
            if(response)
            {
                return RedirectToAction("DetailsJournal", new { id = journal.IdJournal });
            }
            return View();
        }

        public IActionResult DeleteJournal(int id)
        {
            Journal post = _journal.ShowPost<Journal>(id);
            return View(post);
        }

        [HttpPost,ActionName("DeleteJournal")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteJournalPOST(int id)
        {
            bool postResponse = _journal.DeletePost(id);
            return RedirectToAction("Index");
        }


        public IActionResult Notes(int id)
        {
            if (id != null || id != 0)
            { 
                IEnumerable<Note> subPosts = _journal.GetListSubPosts<Note>(id);
                
                //@ViewBag.idJournalForNotes = id; // Deberia ser con la variable Session. ViewBag es para pasar info a la vista. No entre controladores.
                //ViewData es similar viewbag, la diferencia es que viewdata requiere casteo
                HttpContext.Session.SetInt32("idJournalForNotes", id);
                return View(subPosts);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ShowNote(int id)
        {
            Note post = _note.ShowPost<Note>(id);
            ViewBag.Comments = _note.GetListSubPosts<Comment>(id);
            return View(post);
        }

        public IActionResult AddNote()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNote(Note post)
        {
            int? id = HttpContext.Session.GetInt32("idJournalForNotes");
            if (id != null && id != 0)
            { 
                bool response = _journal.AddPost<Note>(post, id);
                if(response)
                    return RedirectToAction("ShowNote", new { id = post.IdNote });
            }
            return View();
        }


        public IActionResult EditNote(int id)
        {
            Note post = _note.ShowPost<Note>(id);

            return View(post);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditNote(Note post, int id)
        {
            bool response = _note.EditPost(post);
            if (response)
            {
                return RedirectToAction("ShowNote", new { id = post.IdNote });
            }
            return View();
        }


        public IActionResult DeleteNote(int id)
        {
            Note post = _note.ShowPost<Note>(id);
            return View(post);
        }

        [HttpPost,ActionName("DeleteNote")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteNotePOST(int id)
        {
            bool response = _note.DeletePost(id);
            if(response)
            {
                return RedirectToAction("Notes");
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(string commentText, int id)
        {
            Comment post = new Comment();
            post.Message = commentText;
            
            bool postResponse = _note.AddPost(post, id);
                       
            return RedirectToAction("ShowNote", new { id = post.IdNote });
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteComment(int id, int idNote)
        { 
            bool postResponse = _comment.DeletePost(id);
            return RedirectToAction("ShowNote", new { id = idNote });
        }
    }
}
