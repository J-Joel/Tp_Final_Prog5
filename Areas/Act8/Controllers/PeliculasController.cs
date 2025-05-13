using Microsoft.AspNetCore.Mvc;
using TP_Final_Programacion5.BaseDeDatoLocal.Act8;

namespace TP_Final_Programacion5.Areas.Act8.Controllers
{
    [Area("Act8")]
    public class PeliculasController : Controller
    {
        public IActionResult Index()
        {
            return View(TablaPeliculas.Peliculas);
        }

        [HttpGet]
        public IActionResult Añadir()
        { 
            return View();
        }
        [HttpPost]
        public IActionResult Añadir(string titulo, DateTime fePubli, string genero, decimal precio)
        {
            var peliExistente = TablaPeliculas.Peliculas.FirstOrDefault(u => u.Titulo == titulo);
            if (peliExistente == null)
            {
                if (!TablaPeliculas.CrearPelicula(titulo, fePubli, genero, precio))
                {
                    TempData["Error"] = "Error al registrar la pelicula";
                    return View();
                }
                TempData["Completado"] = $"Se ha registrado la pelicula nueva: {titulo}";
                return RedirectToAction("Index", "Peliculas");
            }
            TempData["Error"] = "El nombre de la pelicula ya existe";
            return View();
            
        }

        [HttpGet]
        public IActionResult Modificar(int? id)
        {
            if (id != null) // Si no se ingresa una id de peliculas
            {
                var pelicula = TablaPeliculas.Peliculas.FirstOrDefault(u => u.Id == id);
                if (pelicula != null)
                {
                    return View(pelicula);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Modificar(int id, string titulo, DateTime fePubli, string genero, decimal precio)
        {
            var pelicula = TablaPeliculas.Peliculas.FirstOrDefault(u => u.Id == id);
            if (pelicula != null)
            {
                // Esto funcion debido al manejo de la memoria, al crear una variable con direccion al objeto, este refleja los cambios, por ende, se modifica automaticamente el objeto de la lista
                pelicula.Titulo = titulo;
                pelicula.FechaPublicada = fePubli;
                pelicula.Genero = genero;
                pelicula.Precio = precio;

                TempData["Completado"] = $"La película ID: {id}, ha sido modificada";
                return RedirectToAction("Index", "Peliculas");
            }
            TempData["Advertencia"] = "No se ha encontrado una Id de película";
            return View();
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
