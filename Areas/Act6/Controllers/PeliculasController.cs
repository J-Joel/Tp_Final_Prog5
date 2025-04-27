using Microsoft.AspNetCore.Mvc;
using TP_Final_Programacion5.BaseDeDatoLocal.Act6;

namespace TP_Final_Programacion5.Areas.Act6.Controllers
{
    [Area("Act6")]
    public class PeliculasController : Controller
    {
        public IActionResult Index()
        {
            return View(TablaPeliculas.Peliculas);
        }
        public IActionResult Detalles(int? id)
        {
            if (id != null) // Si no se ingresa una id de peliculas
            {
                var pelicula = TablaPeliculas.Peliculas.FirstOrDefault(peli => peli.Id == id);
                if (pelicula != null)
                {
                    return View(pelicula);
                }
            }
            return NotFound();
        }
    }
}
