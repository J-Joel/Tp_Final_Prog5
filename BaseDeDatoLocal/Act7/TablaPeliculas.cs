using TP_Final_Programacion5.Areas.Act7.Models;

namespace TP_Final_Programacion5.BaseDeDatoLocal.Act7
{
    static class TablaPeliculas
    {
        static public List<Movie> peliculas = new List<Movie>
        {
            new Movie
            {
                Id = 0,
                Titulo = "La noche del Terror",
                FechaPublicada = DateTime.Now,
                Genero = "Terror",
                Precio = 50
            },

            new Movie
            {
                Id = 1,
                Titulo = "La noche del Terror II",
                FechaPublicada = DateTime.Now,
                Genero = "Terror",
                Precio = 50
            },
        };
        static private int moviesTotales = 2;
        static public List<Movie> Peliculas { get => peliculas; set => peliculas = value; }
        static public int PeliculasTotales { get => moviesTotales; set => moviesTotales = value; }
        static public bool CrearPelicula(string titulo, DateTime fecha, string genero, int precio)
        {
            try
            {
                var nuevaPelicula = new Movie
                {
                    Id = moviesTotales,
                    Titulo = titulo,
                    FechaPublicada = fecha,
                    Genero = genero,
                    Precio = precio
                };
                Peliculas.Add(nuevaPelicula);
                moviesTotales = moviesTotales + 1;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
