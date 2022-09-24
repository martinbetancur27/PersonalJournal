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


        public JournalController(IUserService userService, IAuthorizeOwner authorizeOwner, IJournal journal, INote note)
        {
            _userService = userService;
            _authorizeOwner = authorizeOwner;

            _journal = journal;
            _note = note;
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

                bool responseIdJournal = _journal.CreateJournal(newJournal);

                if(responseIdJournal)
                {
                    //return RedirectToAction("Notes", new { id = 1 });
                    return RedirectToAction("Index");
                }
                return View("~/Views/Shared/Failure.cshtml");
            }
            return View();
        }


        public IActionResult DetailsJournal(int? id)
        {

            if (id == 0 || id == null || !_authorizeOwner.IsOwnerJournal(id.Value, _userService.GetUserId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            Journal? postFromDb = _journal.Get(id.Value);
            if (postFromDb == null)
            {
                return View("~/Views/Shared/Failure.cshtml");
            }

            DetailsJournalViewModel detailJournal = new DetailsJournalViewModel
            {
                IdJournal = postFromDb.IdJournal,
                Title = postFromDb.Title,
                Message = postFromDb.Message,
                CreateDate = postFromDb.CreateDate,
                LastEditDate = postFromDb.LastEditDate
            };
            return View(detailJournal);
        }


        public IActionResult EditJournal(int? id)
        {
            if (id == 0 || id == null)
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            if (_authorizeOwner.IsOwnerJournal(id.Value, _userService.GetUserId()))
            {
                Journal? postFromDb = _journal.Get(id);

                if(postFromDb == null)
                {
                    return View("~/Views/Shared/Failure.cshtml");
                }

                EditJournalViewModels editJournalViewModel = new EditJournalViewModels
                {
                    IdJournal = postFromDb.IdJournal,
                    Title = postFromDb.Title,
                    Message = postFromDb.Message
                };

                return View(editJournalViewModel);
            }
            return View("~/Views/Shared/NotFound.cshtml");
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
                        return View("~/Views/Shared/Failure.cshtml");
                    }

                    postFromDb.Title = editJournal.Title;
                    postFromDb.Message = editJournal.Message;
                    postFromDb.LastEditDate = DateTime.Now;

                    bool response = _journal.Edit(postFromDb);
                    if (response)
                    {
                        return RedirectToAction("DetailsJournal", new { id = editJournal.IdJournal });
                    }
                    return View("~/Views/Shared/Failure.cshtml");
                }
                return View("~/Views/Shared/NotFound.cshtml");
            }
            return View();
        }


        public IActionResult DeleteJournal(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerJournal(id.Value, _userService.GetUserId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            Journal? postFromDb = _journal.Get(id.Value);
            if (postFromDb == null)
            {
                return View("~/Views/Shared/Failure.cshtml");
            }

            DetailsJournalViewModel detailJournal = new DetailsJournalViewModel
            {
                IdJournal = postFromDb.IdJournal,
                Title = postFromDb.Title,
                Message = postFromDb.Message,
                CreateDate = postFromDb.CreateDate,
                LastEditDate = postFromDb.LastEditDate
            };
            return View(detailJournal);
        }


        [HttpPost,ActionName("DeleteJournal")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteJournalPOST(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerJournal(id.Value, _userService.GetUserId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            bool postResponse = _journal.Delete(id);
            if (postResponse)
            {
                return RedirectToAction("Index");
            }
            return View("~/Views/Shared/Failure.cshtml");
        }


        public IActionResult Notes(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerJournal(id.Value, _userService.GetUserId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            IEnumerable<Note> subPosts = _journal.GetNotes(id);
                
            //@ViewBag.idJournalForNotes = id; // Deberia ser con la variable Session. ViewBag es para pasar info a la vista. No entre controladores.
            //ViewData es similar viewbag, la diferencia es que viewdata requiere casteo
            if(subPosts != null){
                HttpContext.Session.SetInt32("idJournalForNotes", id.Value);
                return View(subPosts);
            }
            return View("~/Views/Shared/Failure.cshtml");

        }

        public IActionResult ShowNote(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerNote(id.Value, _userService.GetUserId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            Note? postFromDb = _note.Get(id);
            if(postFromDb == null)
            {
                return View("~/Views/Shared/Failure.cshtml");
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
                    return View("~/Views/Shared/NotFound.cshtml");
                }

                Note newNote = new Note
                {
                    IdJournal = id.Value,
                    Title = post.Title,
                    Message = post.Message,
                    CreateDate = DateTime.Now,
                    LastEditDate = DateTime.Now
                };


                bool responseIdNote = _journal.AddSub(newNote);
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
                return View("~/Views/Shared/NotFound.cshtml");
            }

            Note? postFromDb = _note.Get(id);
            if (postFromDb == null)
            {
                return View("~/Views/Shared/Failure.cshtml");
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
                        return View("~/Views/Shared/Failure.cshtml");
                    }

                    postFromDb.Title = editNote.Title;
                    postFromDb.Message = editNote.Message;
                    postFromDb.LastEditDate = DateTime.Now;

                    bool response = _note.Edit(postFromDb);
                    if (response)
                    {
                        return RedirectToAction("ShowNote", new { id = editNote.IdNote });
                    }
                    return View("~/Views/Shared/Failure.cshtml");
                }
                return View("~/Views/Shared/NotFound.cshtml");
            }
            return View();
        }


        public IActionResult DeleteNote(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerNote(id.Value, _userService.GetUserId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            Note? postFromDb = _note.Get(id);
            if (postFromDb == null)
            {
                return View("~/Views/Shared/Failure.cshtml");
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
            return View(showNoteViewModel);
        }


        [HttpPost,ActionName("DeleteNote")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteNotePOST(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerNote(id.Value, _userService.GetUserId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }
            //bool response = _journal.DeleteSub<Note>(id); //Another option
            bool response = _note.Delete(id);

            if (response)
            {
                int? idJournal = HttpContext.Session.GetInt32("idJournalForNotes");
                return RedirectToAction("Notes", new { id = idJournal });
            }
            return View("~/Views/Shared/Failure.cshtml");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(string commentText, int? idNote)
        {
            if (idNote == null || idNote == 0 || !_authorizeOwner.IsOwnerNote(idNote.Value, _userService.GetUserId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            Comment newComment = new Comment
            {
                IdNote = idNote.Value,
                Message = commentText,
                CreateDate = DateTime.Now
            };

            bool postResponse = _note.AddSub(newComment);
            if(postResponse)
            { 
                return RedirectToAction("ShowNote", new { id = idNote });
            }
            return View("~/Views/Shared/Failure.cshtml");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteComment(int? id, int? idNote) 
        { 
            if(id == null || id == 0 || idNote == null || idNote == 0 || !_authorizeOwner.IsOwnerNote(idNote.Value, _userService.GetUserId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            bool postResponse = _note.DeleteSub<Comment>(id);
            if (postResponse)
            {
                return RedirectToAction("ShowNote", new { id = idNote });
            }
            return View("~/Views/Shared/Failure.cshtml");
        }
    }
}