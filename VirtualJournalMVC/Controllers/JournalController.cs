using Microsoft.AspNetCore.Mvc;
using Data;
using Models;

namespace VirtualJournalMVC.Controllers
{
    public class JournalController : Controller
    {
        private readonly ApplicationDbContext _db;

        public JournalController(ApplicationDbContext db)
        {
            _db = db;
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
