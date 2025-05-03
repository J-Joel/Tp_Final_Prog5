using Microsoft.AspNetCore.Mvc;
using TP_Final_Programacion5.Areas.Act4.Models;

namespace TP_Final_Programacion5.Areas.Act4.Controllers
{
    [Area("Act4")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "¡Hola " + name + "!";
            ViewData["NumTimes"] = numTimes;
            return View();
        }
        public IActionResult Peliculas()
        {
            var listMovies = new List<Movie>();

            var movie1 = new Movie
            {
                Genre = "Terror",
                Id = 1,
                Price = 1,
                ReleaseDate = DateTime.Now,
                Title = "La noche del terror"
            };
            listMovies.Add(movie1);

            var movie2 = new Movie
            {
                Genre = "Terror",
                Id = 1,
                Price = 1,
                ReleaseDate = DateTime.Now,
                Title = "La noche del terror II"
            };
            listMovies.Add(movie2);

            return View(listMovies);
        }
        // GET: Movies/Detalles/5
        public IActionResult Detalles(int? id)
        {
            if (id == null) // Si no se ingresa una id de peliculas
            {
                return NotFound();
            }
            //Simulación de creación de un objeto (model)
            //Mas adelante vamos a ver como usar una base de datos
            var movie = new Movie
            {
                Genre = "Terror",
                Id = 1,
                Price = 1,
                ReleaseDate = DateTime.Now,
                Title = "La noche del terror"
            };
            return View(movie);
        }
        public IActionResult Privacidad()
        {
            return View();
        }
    }
}
