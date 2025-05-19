using Microsoft.AspNetCore.Mvc;
using TP_Final_Programacion5.Conexion.Act9;

namespace TP_Final_Programacion5.Areas.Act9.Controllers
{
    [Area("Act9")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Ejemplo de uso de datos temporales, es aplicable para el redireccionamiento de vista
            //TempData["Completado"] = "aaaa";
            //TempData["Error"] = "bbbb";
            //TempData["Advertencia"] = "cccc";
            if (!MongoDBF.EstablecerConexion())
            {
                TempData["Error"] = "Conexion fallida";
                return View(null);
            }
            return View(MongoDBF.ListarUsuarios());
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
