using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;

namespace VirtualJournalMVC.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private INote _noteService;
        private readonly IUserService _userService;
        private readonly IAuthorizeOwner _authorizeOwner;

        public NoteController(IUserService userService, IAuthorizeOwner authorizeOwner, INote noteService)
        {
            _userService = userService;
            _authorizeOwner = authorizeOwner;
            _noteService = noteService;
        }


        public IActionResult ShowNote(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerNote(id.Value, _userService.GetUserId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            Note? noteFromDb = _noteService.FindNote(id.Value);

            if (noteFromDb == null)
            {
                return View("~/Views/Shared/Failure.cshtml");
            }

            ShowNoteViewModel noteViewModel = new ShowNoteViewModel
            {
                IdNote = noteFromDb.IdNote,
                IdJournal = noteFromDb.IdJournal,
                Title = noteFromDb.Title,
                Message = noteFromDb.Message,
                CreateDate = noteFromDb.CreateDate,
                LastEditDate = noteFromDb.LastEditDate
            };

            ShowNoteAndCommentsViewModel showNoteAndCommentsViewModel = new ShowNoteAndCommentsViewModel();
            showNoteAndCommentsViewModel.Note = noteViewModel;
            showNoteAndCommentsViewModel.Comments = _noteService.GetCommentsOfNote(id.Value);

            return View(showNoteAndCommentsViewModel);
        }


        public IActionResult AddNote()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNote(CreateNoteViewModel createNoteViewModel)
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
                    Title = createNoteViewModel.Title,
                    Message = createNoteViewModel.Message,
                    CreateDate = DateTime.Now,
                    LastEditDate = DateTime.Now
                };

                int? responseIdNote = _noteService.AddNote(newNote);

                if (responseIdNote == null)
                {
                    return View("~/Views/Shared/Failure.cshtml");
                }

                return RedirectToAction("ShowNote", new { id = responseIdNote });
            }

            return View();
        }


        public IActionResult EditNote(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerNote(id.Value, _userService.GetUserId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            Note? noteFromDb = _noteService.FindNote(id.Value);

            if (noteFromDb == null)
            {
                return View("~/Views/Shared/Failure.cshtml");
            }

            EditNoteViewModel editNoteViewModel = new EditNoteViewModel
            {
                IdNote = id.Value,
                Title = noteFromDb.Title,
                Message = noteFromDb.Message
            };

            return View(editNoteViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditNote(EditNoteViewModel editNote)
        {
            if (ModelState.IsValid)
            {
                if (_authorizeOwner.IsOwnerNote(editNote.IdNote, _userService.GetUserId()))
                {
                    Note? noteFromDb = _noteService.FindNote(editNote.IdNote);
                    if (noteFromDb == null)
                    {
                        return View("~/Views/Shared/Failure.cshtml");
                    }

                    noteFromDb.Title = editNote.Title;
                    noteFromDb.Message = editNote.Message;
                    noteFromDb.LastEditDate = DateTime.Now;

                    bool isNoteEdited = _noteService.EditNote(noteFromDb);

                    if (isNoteEdited)
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

            Note? noteFromDb = _noteService.FindNote(id.Value);

            if (noteFromDb == null)
            {
                return View("~/Views/Shared/Failure.cshtml");
            }

            ShowNoteViewModel showNoteViewModel = new ShowNoteViewModel
            {
                IdNote = noteFromDb.IdNote,
                IdJournal = noteFromDb.IdJournal,
                Title = noteFromDb.Title,
                Message = noteFromDb.Message,
                CreateDate = noteFromDb.CreateDate,
                LastEditDate = noteFromDb.LastEditDate
            };

            return View(showNoteViewModel);
        }


        [HttpPost, ActionName("DeleteNote")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteNotePOST(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerNote(id.Value, _userService.GetUserId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }
            
            bool isNoteRemoved = _noteService.RemoveNote(id.Value);

            if (isNoteRemoved)
            {
                int? idJournal = HttpContext.Session.GetInt32("idJournalForNotes");
                return RedirectToAction("NotesOfJournal", "Journal", new { id = idJournal });
            }

            return View("~/Views/Shared/Failure.cshtml");
        }
    }
}