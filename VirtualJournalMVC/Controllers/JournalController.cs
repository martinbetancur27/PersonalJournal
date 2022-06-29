using Microsoft.AspNetCore.Mvc;
using Data;
using Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using IService;


namespace VirtualJournalMVC.Controllers
{
    [Authorize]
    public class JournalController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserService _userService;


        public JournalController(ApplicationDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddJournal()
        {
            return View();
        }

        public IActionResult DetailsJournal()
        {
            return View();
        }

        public IActionResult EditJournal()
        {
            return View();
        }

        public IActionResult DeleteJournal(int? IdJournal)
        {
            return View();
        }

        public IActionResult Notes()
        {
            return View();
        }

        public IActionResult ShowNote()
        {
            return View();
        }

        public IActionResult AddNote()
        {
            return View();
        }

        public IActionResult EditNote()
        {
            return View();
        }

        public IActionResult DeleteNote()
        {
            return View();
        }

    }
}
