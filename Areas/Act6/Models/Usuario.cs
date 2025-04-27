using System.ComponentModel.DataAnnotations;

namespace TP_Final_Programacion5.Areas.Act6.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public required string User { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public required string Contraseña { get; set; }
    }
}
