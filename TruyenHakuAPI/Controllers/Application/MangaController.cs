using Microsoft.AspNetCore.Mvc;

namespace TruyenHakuAPI.Controllers.Application
{
    public class MangaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
