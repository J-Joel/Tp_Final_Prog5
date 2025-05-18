using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace TP_Final_Programacion5.Areas.Act9.Models
{
    public class Usuario
    {
        /// Atributos
        private string id = "";
        private string user = "";
        private string contraseña = "";

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

        /// Metodo constructor
        public Usuario(string user, string pas)
        {
            User = user;
            Contraseña = pas;
        }
    }
}
