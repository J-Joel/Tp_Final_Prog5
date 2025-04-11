using System.ComponentModel.DataAnnotations;

namespace TP_Final_Programacion5.Models.Act4
{
    public class Movie
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public required string Genre { get; set; }
        public decimal Price { get; set; }
    }
}
