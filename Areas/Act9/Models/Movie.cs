using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TP_Final_Programacion5.Areas.Act9.Models
{
    public class Movie
    {
        /// Atributos
        private string id = "";
        private string titulo = "";
        private DateOnly fechaPublicada;
        private string genero = "";
        private decimal precio;

        /// Encapsulamiento y etiquetas MongoDB
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get => id; set => id = value; }

        [BsonElement("titulo")]
        public string Titulo { get => titulo; set => titulo = value; }

        [BsonElement("fechaPublicada")]
        public DateOnly FechaPublicada { get => fechaPublicada; set => fechaPublicada = value; }

        [BsonElement("genero")]
        public string Genero { get => genero; set => genero = value; }

        [BsonElement("precio")]
        public decimal Precio { get => precio; set => precio = value; }

        /// Metodo constructor
        public Movie(string titu, DateOnly fepubli, string gen, decimal pre)
        {
            Titulo = titu;
            FechaPublicada = fepubli;
            Genero = gen;
            Precio = pre;
        }
    }
}
