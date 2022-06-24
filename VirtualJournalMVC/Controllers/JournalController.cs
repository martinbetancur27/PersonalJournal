using Microsoft.AspNetCore.Mvc;

namespace VirtualJournalMVC.Controllers
{
    public class JournalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
