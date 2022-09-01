using Microsoft.AspNetCore.Mvc;
using Data;
using Models;
using Models.ViewModels;
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
        public IActionResult CreateJournal(CreateJournalViewModel createJournal)
        {
            /*journal.IdUser = _userService.GetUserId();
            journal.CreateDate = DateTime.Now;
            journal.LastEditDate = DateTime.Now;*/
            
            if(ModelState.IsValid){
                                
                int? responseIdJournal = _journalRoot.Create(createJournal, _userService.GetUserId());
                if(responseIdJournal != 0 && responseIdJournal != null)
                {
                    return RedirectToAction("Notes", new { id = responseIdJournal });
                }
                return NotFound();
            }
            return View();
        }


        public IActionResult DetailsJournal(int? id)
        {
            DetailsJournalViewModel post = _journal.ShowPost<DetailsJournalViewModel>(id);
            if(post == null)
            {
                return NotFound();
            }
            return View(post);
        }


        public IActionResult EditJournal(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            EditJournalViewModels post = _journal.ShowEditPost<EditJournalViewModels>(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditJournal(EditJournalViewModels editJournal)
        {
            if(ModelState.IsValid){
                bool response = _journal.EditPost(editJournal);
                if(response)
                {
                    return RedirectToAction("DetailsJournal", new { id = editJournal.IdJournal });
                }
                return NotFound();
            }
            return View();
        }

        public IActionResult DeleteJournal(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            DetailsJournalViewModel post = _journal.ShowPost<DetailsJournalViewModel>(id);
            return View(post);
        }

        [HttpPost,ActionName("DeleteJournal")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteJournalPOST(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            bool postResponse = _journal.DeletePost(id);
            if (postResponse)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }


        public IActionResult Notes(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            IEnumerable<Note> subPosts = _journal.GetListSubPosts<Note>(id);
                
            //@ViewBag.idJournalForNotes = id; // Deberia ser con la variable Session. ViewBag es para pasar info a la vista. No entre controladores.
            //ViewData es similar viewbag, la diferencia es que viewdata requiere casteo
            HttpContext.Session.SetInt32("idJournalForNotes", id.Value);
            return View(subPosts);
            
            
        }

        public IActionResult ShowNote(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            ShowNoteAndCommentsViewModel post = new ShowNoteAndCommentsViewModel();
            post.Note = _note.ShowPost<ShowNoteViewModel>(id);
            post.Comments = _note.GetListSubPosts<Comment>(id);

            return View(post);
        }

        public IActionResult AddNote()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNote(CreateNoteViewModel post)
        {
            if (ModelState.IsValid)
            {
                int? id = HttpContext.Session.GetInt32("idJournalForNotes");

                if (id == 0 || id == null)
                {
                    return NotFound();
                }

                int? responseIdNote = _journal.AddPost(post, id);
                if (responseIdNote != 0 && responseIdNote != null)
                    return RedirectToAction("ShowNote", new { id = responseIdNote });
               
            }
            return View();
        }


        public IActionResult EditNote(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            EditNoteViewModel post = _note.ShowEditPost<EditNoteViewModel>(id);

            if(post == null)
            {
                return NotFound();
            }
            return View(post);
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditNote(EditNoteViewModel editNote)
        {

            if (ModelState.IsValid){
                bool response = _note.EditPost(editNote);
                if (response)
                {
                    return RedirectToAction("ShowNote", new { id = editNote.IdNote });
                }
                return NotFound();
            }

            return View();
        }


        public IActionResult DeleteNote(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            ShowNoteViewModel post = _note.ShowPost<ShowNoteViewModel>(id);
            return View(post);
        }

        [HttpPost,ActionName("DeleteNote")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteNotePOST(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            bool response = _note.DeletePost(id);

            if (response)
            {
                int? idJournal = HttpContext.Session.GetInt32("idJournalForNotes");
                return RedirectToAction("Notes", new { id = idJournal });
            }
            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(string commentText, int? idNote)
        {
            if (idNote == null || idNote == 0)
            {
                return NotFound();
            }

            if (commentText != null)
            { 
                
                CreateCommentViewModel post = new CreateCommentViewModel();
                post.Message = commentText;
            
                int? postResponse = _note.AddPost(post, idNote);
            }
            return RedirectToAction("ShowNote", new { id = idNote });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteComment(int? id, int? idNote) 
        { 
            if(id == null || id == 0 || idNote == null || idNote == 0)
            {
                return NotFound();
            }

            bool postResponse = _comment.DeletePost(id);
            return RedirectToAction("ShowNote", new { id = idNote });
            
        }
    }
}
