using Microsoft.AspNetCore.Mvc;
using TP_Final_Programacion5.BaseDeDatoLocal.Act7;

namespace TP_Final_Programacion5.Areas.Act7.Controllers
{
    [Area("Act7")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(TablaUsuarios.Usuarios);
        }
        public IActionResult Welcome(int numTimes = 1)
        {
            ViewData["NumTimes"] = numTimes;
            return View();
        }
        public IActionResult Privacidad()
        {
            return View();
        }
    }
}
