using System.ComponentModel.DataAnnotations;

namespace TP_Final_Programacion5.Areas.Act6.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaPublicada { get; set; }
        public required string Genero { get; set; }
        public decimal Precio { get; set; }
    }
}
