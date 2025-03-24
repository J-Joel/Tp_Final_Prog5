using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_Final_Programacion5.Models;

namespace TP_Final_Programacion5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        // Aqui se vinculan las vistas o plantillas html
        public IActionResult Index()
        {
            return View();
            // Devuelve la vista/plantilla segun como se llame la funcion interface(En este caso index),
            // si no existe una vista de igual nombre, tira error al no encontrar el csHTML
        }

        /******************************************************/
        public IActionResult Act1()
        {
            return View("../Act1/Act1");
        }
        public IActionResult Contacto1()
        {
            return View("../Act1/Contacto1");
        }
        public IActionResult Privado1()
        {
            return View("../Act1/Privado1");
        }
        /******************************************************/

        // Funcion interface que informa los errores al momento de cargar una vista
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
