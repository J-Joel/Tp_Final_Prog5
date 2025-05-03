using Microsoft.AspNetCore.Mvc;
using TP_Final_Programacion5.BaseDeDatoLocal.Act6;

namespace TP_Final_Programacion5.Areas.Act6.Controllers
{
    [Area("Act6")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(TablaUsuarios.Usuarios);
        }
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "¡Hola " + name + "!";
            ViewData["NumTimes"] = numTimes;
            return View();
        }
        public IActionResult Privacidad()
        {
            return View();
        }
    }
}
