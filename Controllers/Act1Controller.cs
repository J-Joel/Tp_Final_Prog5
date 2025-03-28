using Microsoft.AspNetCore.Mvc;

namespace TP_Final_Programacion5.Controllers
{
    public class Act1Controller : Controller
    {
        /* Se supone que no deberia de especificar siempre el controlador a usar, solamente para cambiar, pero hay que siempre especificarlo */
        /******************************************************/
        /* Los controladores tienen una funcion especial, esta funcionalidad es la de trabajar con el nombre del controlador
         * [Nombre de controlador]Controller, la acciones del controlador buscan en la carpeta de vistas el [Nombre de controlador]
         * automaticamente, esto quiere decir que accederan a los htmls directamente a esa carpeta */
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Contacto()
        {
            return View();
        }
        public IActionResult Privado()
        {
            return View();
        }
        /******************************************************/
    }
}
