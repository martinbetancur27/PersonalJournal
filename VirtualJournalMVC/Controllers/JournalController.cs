using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using IService;

namespace VirtualJournalMVC.Controllers
{
    [Authorize]
    public class JournalController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthorizeOwner _authorizeOwner;
        private IJournal _journalService;


        public JournalController(IUserService userService, IAuthorizeOwner authorizeOwner, IJournal journalService)
        {
            _userService = userService;
            _authorizeOwner = authorizeOwner;
            _journalService = journalService;
        }


        public IActionResult Index()
        {
            string idUser = _userService.GetId();

            IEnumerable<Journal>? journalsOfUsers = _journalService.GetOfUser(idUser);

            return View(journalsOfUsers);
        }


        public IActionResult CreateJournal()
        {
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateJournal(CreateJournalViewModel createJournalViewModel)
        {           
            if(ModelState.IsValid){

                Journal newJournal = new Journal
                {
                    IdUser = _userService.GetId(),
                    Title = createJournalViewModel.Title,
                    Message = createJournalViewModel.Message,
                    CreateDate = DateTime.Now,
                    LastEditDate = DateTime.Now
                };

                int? responseIdJournal = _journalService.Add(newJournal);

                if (responseIdJournal == null)
                {
                    return View("~/Views/Shared/Failure.cshtml");
                }

                return RedirectToAction("NotesOfJournal", new { id = responseIdJournal });
            }

            return View();
        }


        public IActionResult DetailsJournal(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerJournal(id.Value, _userService.GetId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            Journal? journalFromDb = _journalService.Find(id.Value);

            if (journalFromDb == null)
            {
                return View("~/Views/Shared/Failure.cshtml");
            }

            DetailsJournalViewModel detailJournal = new DetailsJournalViewModel
            {
                IdJournal = journalFromDb.IdJournal,
                Title = journalFromDb.Title,
                Message = journalFromDb.Message,
                CreateDate = journalFromDb.CreateDate,
                LastEditDate = journalFromDb.LastEditDate
            };

            return View(detailJournal);
        }


        public IActionResult EditJournal(int? id)
        {
            if (id == 0 || id == null)
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            if (_authorizeOwner.IsOwnerJournal(id.Value, _userService.GetId()))
            {
                Journal? journalFromDb = _journalService.Find(id.Value);

                if (journalFromDb == null)
                {
                    return View("~/Views/Shared/Failure.cshtml");
                }

                EditJournalViewModels editJournalViewModel = new EditJournalViewModels
                {
                    IdJournal = journalFromDb.IdJournal,
                    Title = journalFromDb.Title,
                    Message = journalFromDb.Message
                };

                return View(editJournalViewModel);
            }

            return View("~/Views/Shared/NotFound.cshtml");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditJournal(EditJournalViewModels editJournal)
        {
            if (ModelState.IsValid)
            {
                if (_authorizeOwner.IsOwnerJournal(editJournal.IdJournal, _userService.GetId()))
                {
                    Journal? journalFromDb = _journalService.Find(editJournal.IdJournal);

                    if (journalFromDb == null)
                    {
                        return View("~/Views/Shared/Failure.cshtml");
                    }

                    journalFromDb.Title = editJournal.Title;
                    journalFromDb.Message = editJournal.Message;
                    journalFromDb.LastEditDate = DateTime.Now;

                    bool isJournalEdited = _journalService.Edit(journalFromDb);

                    if (isJournalEdited)
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
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerJournal(id.Value, _userService.GetId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            Journal? journalFromDb = _journalService.Find(id.Value);

            if (journalFromDb == null)
            {
                return View("~/Views/Shared/Failure.cshtml");
            }

            DetailsJournalViewModel detailsJournal = new DetailsJournalViewModel
            {
                IdJournal = journalFromDb.IdJournal,
                Title = journalFromDb.Title,
                Message = journalFromDb.Message,
                CreateDate = journalFromDb.CreateDate,
                LastEditDate = journalFromDb.LastEditDate
            };

            return View(detailsJournal);
        }


        [HttpPost,ActionName("DeleteJournal")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteJournalPOST(int? id)
        {
            if (id == 0 || id == null || !_authorizeOwner.IsOwnerJournal(id.Value, _userService.GetId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            bool isJournalRemoved = _journalService.Remove(id.Value);

            if (isJournalRemoved)
            {
                return RedirectToAction("Index");
            }

            return View("~/Views/Shared/Failure.cshtml");
        }
    }
}