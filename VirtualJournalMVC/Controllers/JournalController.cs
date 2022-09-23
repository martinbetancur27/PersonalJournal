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
using System.Xml.Linq;

namespace VirtualJournalMVC.Controllers
{
    [Authorize]
    public class JournalController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthorizeOwner _authorizeOwner;
        private IJournal _journal;
        private INote _note;
        private IComment _comment;


        public JournalController(IUserService userService, IAuthorizeOwner authorizeOwner, IJournal journal, INote note, IComment comment)
        {
            _userService = userService;
            _authorizeOwner = authorizeOwner;

            _journal = journal;
            _note = note;
            _comment = comment;
        }


        public IActionResult Index()
        {
            string idUser = _userService.GetUserId();

            IEnumerable<Journal>? journalsIndex = _journal.GetJournals(idUser); //Parameter 0 is select idUser as Root
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
            if(ModelState.IsValid){

                Journal newJournal = new Journal
                {
                    IdUser = _userService.GetUserId(),
                    Title = createJournal.Title,
                    Message = createJournal.Message,
                    CreateDate = DateTime.Now,
                    LastEditDate = DateTime.Now
                };

                bool responseIdJournal = _journal.Add(newJournal);

                if(responseIdJournal)
                {
                    //return RedirectToAction("Notes", new { id = 1 });
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            return View();
        }


        public IActionResult DetailsJournal(int? id)
        {

            if (id == 0 || id == null || !_authorizeOwner.IsOwnerJournal(id.Value, _userService.GetUserId()))
            {
                return NotFound();
            }

            Journal? postFromDb = _journal.Get(id.Value);
            if (postFromDb == null)
            {
                return NotFound();
            }

            DetailsJournalViewModel detailJournal = new DetailsJournalViewModel
            {
                IdJournal = postFromDb.IdJournal,
                Title = postFromDb.Title,
                Message = postFromDb.Message,
                CreateDate = postFromDb.CreateDate,
                LastEditDate = postFromDb.LastEditDate
            };

            if(postFromDb == null)
            {
                return NotFound();
            }
            return View(detailJournal);
        }


        public IActionResult EditJournal(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            if (_authorizeOwner.IsOwnerJournal(id.Value, _userService.GetUserId()))
            {
                Journal? postFromDb = _journal.Get(id);

                if(postFromDb == null)
                {
                    return NotFound();
                }

                EditJournalViewModels editJournalViewModel = new EditJournalViewModels
                {
                    IdJournal = postFromDb.IdJournal,
                    Title = postFromDb.Title,
                    Message = postFromDb.Message
                };

                if (postFromDb == null)
                {
                    return NotFound();
                }
                return View(editJournalViewModel);
            }
            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditJournal(EditJournalViewModels editJournal)
        {
            if(ModelState.IsValid)
            {
                if (_authorizeOwner.IsOwnerJournal(editJournal.IdJournal, _userService.GetUserId()))
                {
                    Journal? postFromDb = _journal.Get(editJournal.IdJournal);
                    if (postFromDb == null)
                    {
                        return NotFound();
                    }

                    postFromDb.Title = editJournal.Title;
                    postFromDb.Message = editJournal.Message;
                    postFromDb.LastEditDate = DateTime.Now;

                    bool response = _journal.Edit(postFromDb);
                    if (response)
                    {
                        return RedirectToAction("DetailsJournal", new { id = editJournal.IdJournal });
                    }
                    return NotFound();
                }
            }
            return View();
        }


        public IActionResult DeleteJournal(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerJournal(id.Value, _userService.GetUserId()))
            {
                return NotFound();
            }

            Journal? postFromDb = _journal.Get(id.Value);
            if (postFromDb == null)
            {
                return NotFound();
            }

            DetailsJournalViewModel detailJournal = new DetailsJournalViewModel
            {
                IdJournal = postFromDb.IdJournal,
                Title = postFromDb.Title,
                Message = postFromDb.Message,
                CreateDate = postFromDb.CreateDate,
                LastEditDate = postFromDb.LastEditDate
            };

            if (postFromDb == null)
            {
                return NotFound();
            }
            return View(detailJournal);
        }


        [HttpPost,ActionName("DeleteJournal")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteJournalPOST(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerJournal(id.Value, _userService.GetUserId()))
            {
                return NotFound();
            }

            bool postResponse = _journal.Delete(id);
            if (postResponse)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }


        public IActionResult Notes(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerJournal(id.Value, _userService.GetUserId()))
            {
                return NotFound();
            }

            IEnumerable<Note> subPosts = _journal.GetNotes(id);
                
            //@ViewBag.idJournalForNotes = id; // Deberia ser con la variable Session. ViewBag es para pasar info a la vista. No entre controladores.
            //ViewData es similar viewbag, la diferencia es que viewdata requiere casteo
            if(subPosts != null){
                HttpContext.Session.SetInt32("idJournalForNotes", id.Value);
                return View(subPosts);
            }
            return NotFound();
            
        }

        public IActionResult ShowNote(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerNote(id.Value, _userService.GetUserId()))
            {
                return NotFound();
            }

            Note? postFromDb = _note.Get(id);
            if(postFromDb == null)
            {
                return NotFound();
            }

            ShowNoteViewModel noteViewModel = new ShowNoteViewModel
            {
                IdNote = postFromDb.IdNote,
                IdJournal = postFromDb.IdJournal,
                Title = postFromDb.Title,
                Message = postFromDb.Message,
                CreateDate = postFromDb.CreateDate,
                LastEditDate = postFromDb.LastEditDate
            };

            ShowNoteAndCommentsViewModel post = new ShowNoteAndCommentsViewModel();
            post.Note = noteViewModel;
            post.Comments = _note.GetComments(id);

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

                if (id == 0 || id == null || !_authorizeOwner.IsOwnerJournal(id.Value, _userService.GetUserId()))
                {
                    return NotFound();
                }

                Note newNote = new Note
                {
                    IdJournal = id.Value,
                    Title = post.Title,
                    Message = post.Message,
                    CreateDate = DateTime.Now,
                    LastEditDate = DateTime.Now
                };


                bool responseIdNote = _journal.Add(newNote);
                if (responseIdNote)
                {
                    //return RedirectToAction("ShowNote", new { id = responseIdNote });
                    return RedirectToAction("Notes", new { id = id });
                }
            }
            return View();
        }


        public IActionResult EditNote(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerNote(id.Value, _userService.GetUserId()))
            {
                return NotFound();
            }

            Note? postFromDb = _note.Get(id);
            if (postFromDb == null)
            {
                return NotFound();
            }

            EditNoteViewModel editNoteViewModel = new EditNoteViewModel
            {
                IdNote = id.Value,
                Title = postFromDb.Title,
                Message = postFromDb.Message

            };
            return View(editNoteViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditNote(EditNoteViewModel editNote)
        {
            if (ModelState.IsValid)
            {
                if(_authorizeOwner.IsOwnerNote(editNote.IdNote, _userService.GetUserId()))
                {
                    Note? postFromDb = _note.Get(editNote.IdNote);
                    if (postFromDb == null)
                    {
                        return NotFound();
                    }

                    postFromDb.Title = editNote.Title;
                    postFromDb.Message = editNote.Message;
                    postFromDb.LastEditDate = DateTime.Now;

                    bool response = _note.Edit(postFromDb);
                    if (response)
                    {
                        return RedirectToAction("ShowNote", new { id = editNote.IdNote });
                    }
                }
                return NotFound();
            }

            return View();
        }


        public IActionResult DeleteNote(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerNote(id.Value, _userService.GetUserId()))
            {
                return NotFound();
            }

            Note? postFromDb = _note.Get(id);
            if (postFromDb == null)
            {
                return NotFound();
            }

            ShowNoteViewModel showNoteViewModel = new ShowNoteViewModel
            {
                IdNote = postFromDb.IdNote,
                IdJournal = postFromDb.IdJournal,
                Title = postFromDb.Title,
                Message = postFromDb.Message,
                CreateDate = postFromDb.CreateDate,
                LastEditDate = postFromDb.LastEditDate
            };

            if (postFromDb == null)
            {
                return NotFound();
            }
            return View(showNoteViewModel);
        }


        [HttpPost,ActionName("DeleteNote")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteNotePOST(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerNote(id.Value, _userService.GetUserId()))
            {
                return NotFound();
            }

            bool response = _note.Delete(id);

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
            if (idNote == null || idNote == 0 || !_authorizeOwner.IsOwnerNote(idNote.Value, _userService.GetUserId()))
            {
                return NotFound();
            }

            Comment newComment = new Comment
            {
                IdNote = idNote.Value,
                Message = commentText,
                CreateDate = DateTime.Now
            };

            _note.Add(newComment);
            return RedirectToAction("ShowNote", new { id = idNote });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteComment(int? id, int? idNote) 
        { 
            if(id == null || id == 0 || idNote == null || idNote == 0 || !_authorizeOwner.IsOwnerNote(idNote.Value, _userService.GetUserId()))
            {
                return NotFound();
            }

            bool postResponse = _comment.Delete(id);
            return RedirectToAction("ShowNote", new { id = idNote });
        }
    }
}
