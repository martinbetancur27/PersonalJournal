using IService;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace VirtualJournalMVC.Controllers
{
    public class CommentController : Controller
    {
        private IComment _commentService;
        private readonly IUserService _userService;
        private readonly IAuthorizeOwner _authorizeOwner;

        public CommentController(IUserService userService, IAuthorizeOwner authorizeOwner, IComment commentService)
        {
            _userService = userService;
            _authorizeOwner = authorizeOwner;
            _commentService = commentService;
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(string commentText, int? idNote)
        {
            if (idNote == null || idNote == 0 || !_authorizeOwner.IsOwnerNote(idNote.Value, _userService.GetId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            Comment newComment = new Comment
            {
                IdNote = idNote.Value,
                Message = commentText,
                CreateDate = DateTime.Now
            };

            int? postResponse = _commentService.Add(newComment);

            if (postResponse == null)
            {
                return View("~/Views/Shared/Failure.cshtml");
            }

            return RedirectToAction("ShowNote", "Note", new { id = idNote });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteComment(int? id, int? idNote)
        {
            if (id == null || id == 0 || idNote == null || idNote == 0 || !_authorizeOwner.IsOwnerNote(idNote.Value, _userService.GetId()))
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            bool postResponse = _commentService.Remove(id.Value);

            if (postResponse)
            {
                return RedirectToAction("ShowNote", "Note", new { id = idNote });
            }

            return View("~/Views/Shared/Failure.cshtml");
        }
    }
}