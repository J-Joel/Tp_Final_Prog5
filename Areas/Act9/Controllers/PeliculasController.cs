using Microsoft.AspNetCore.Mvc;
using TP_Final_Programacion5.Conexion.Act9;

namespace TP_Final_Programacion5.Areas.Act9.Controllers
{
    [Area("Act9")]
    public class PeliculasController : Controller
    {
        public IActionResult Index()
        {
            if (!MongoDBF.EstablecerConexion())
            {
                TempData["Error"] = "Conexion fallida";
                return View(null);
            }
            var lista = MongoDBF.ListarPeliculas();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Añadir()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Añadir(string titulo, DateOnly fePubli, string genero, decimal precio)
        {
            if (!MongoDBF.EstablecerConexion())
            {
                TempData["Error"] = "Conexion fallida";
                return View();
            }

            if (MongoDBF.ExistePelicula(titulo) != null)
            {
                TempData["Error"] = "El nombre de la pelicula ya existe";
                return View();
            }
            if (!MongoDBF.AñadirPelicula(titulo, fePubli, genero, precio))
            {
                TempData["Error"] = "Error al registrar la pelicula";
                return View();
            }
            TempData["Completado"] = $"Se ha registrado la pelicula nueva: {titulo}";
            return RedirectToAction("Index", "Peliculas");
        }

        [HttpGet]
        public IActionResult Modificar(string? id)
        {
            if (!MongoDBF.EstablecerConexion())
            {
                TempData["Error"] = "Conexion fallida";
                return View();
            }
            if (id != null) // Si no se ingresa una id de peliculas
            {
                var pelicula = MongoDBF.SeleccionarPelicula(id);
                if (pelicula != null)
                {
                    return View(pelicula);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Modificar(string id, string titulo, DateOnly fePubli, string genero, decimal precio)
        {
            if (!MongoDBF.EstablecerConexion())
            {
                TempData["Error"] = "Conexion fallida";
                return View();
            }
            if (MongoDBF.SeleccionarPelicula(id) == null)
            {
                TempData["Advertencia"] = "La película que intenta modificar, no se encuentra registrada";
                return View();
            }
            if (!MongoDBF.ModificarPelicula(id, titulo, fePubli, genero, precio))
            {
                TempData["Error"] = "No se ha podido modificar la película";
                return View();
            }
            TempData["Completado"] = $"La película ID: {id}, ha sido modificada";
            return RedirectToAction("Index", "Peliculas");
        }

        public IActionResult Detalles(string? id)
        {
            if (!MongoDBF.EstablecerConexion())
            {
                TempData["Error"] = "Conexion fallida";
                return View();
            }
            if (id != null) // Si no se ingresa una id de peliculas
            {
                var pelicula = MongoDBF.SeleccionarPelicula(id);
                if (pelicula != null)
                {
                    TempData["Completado"] = $"Se ha cargado correctamente los datos de: {id}";
                    return View(pelicula);
                }
            }
            TempData["Error"] = $"No se a encontrado la pelicula {id}";
            return RedirectToAction("Index", "Peliculas");
        }
        public IActionResult Eliminar(string? id)
        {
            if (!MongoDBF.EstablecerConexion())
            {
                TempData["Error"] = "Conexion fallida";
                return RedirectToAction("Index", "Peliculas");
            }
            if (MongoDBF.SeleccionarPelicula(id) == null)
            {
                TempData["Advertencia"] = "La película que intenta eliminar, no se encuentra registrada";
                return RedirectToAction("Index", "Peliculas");
            }
            if (!MongoDBF.EliminarPelicula(id))
            {
                TempData["Error"] = "No se ha podido eliminar la película";
                return RedirectToAction("Index", "Peliculas");
            }
            TempData["Completado"] = $"La película ID: {id}, ha sido eliminada";
            return RedirectToAction("Index", "Peliculas");
        }
    }
}
