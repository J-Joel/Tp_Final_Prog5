using Microsoft.AspNetCore.Mvc;
using TP_Final_Programacion5.Areas.Act5.Models;

namespace TP_Final_Programacion5.Areas.Act5.Controllers
{
    [Area("Act5")]
    public class PeliculasController : Controller
    {
        public IActionResult Index()
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
        public async Task<IActionResult> Detalles(int? id)
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
    }
}
