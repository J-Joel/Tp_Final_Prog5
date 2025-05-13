using Microsoft.AspNetCore.Mvc;
using TP_Final_Programacion5.BaseDeDatoLocal.Act8;

namespace TP_Final_Programacion5.Areas.Act8.Controllers
{
    [Area("Act8")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Ejemplo de uso de datos temporales, es aplicable para el redireccionamiento de vista
            //TempData["Completado"] = "aaaa";
            //TempData["Error"] = "bbbb";
            //TempData["Advertencia"] = "cccc";
            return View(TablaUsuarios.Usuarios);
        }
        public IActionResult Welcome()
        {
            return View();
        }
        public IActionResult Privacidad()
        {
            return View();
        }
    }
}
