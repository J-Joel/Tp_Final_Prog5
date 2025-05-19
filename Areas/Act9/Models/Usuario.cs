using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TP_Final_Programacion5.Areas.Act9.Models
{
    public class Usuario
    {
        /// Atributos
        private string id = "";
        private string user = "";
        private string contraseña = "";
        private string email = "";
        private string nombre = "";
        private string apellido = "";
        private DateOnly fechaNacimiento;
        private DateTime fechaAlta;

        /// Encapsulamiento y etiquetas MongoDB
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get => id; set => id = value; }

        [BsonElement("user")]
        public string User { get => user; set => user = value; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [BsonElement("contraseña")]
        public string Contraseña { get => contraseña; set => contraseña = value; }
        [BsonElement("email")]
        public string Email { get => email; set => email = value; }
        [BsonElement("nombre")]
        public string Nombre { get => nombre; set => nombre = value; }
        [BsonElement("apellido")]
        public string Apellido { get => apellido; set => apellido = value; }
        [BsonElement("fechaNacimiento")]
        public DateOnly FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        [BsonElement("fechaAlta")]
        public DateTime FechaAlta { get => fechaAlta; set => fechaAlta = value; }

        /// Metodo constructor
        public Usuario(string user, string pas)
        {
            User = user;
            Contraseña = pas;
            Email = "";
            Nombre = "";
            Apellido = "";
            FechaNacimiento = DateOnly.FromDateTime(DateTime.UtcNow.Date);
            FechaAlta = DateTime.Now;
        }
    }
}
