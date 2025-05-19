using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TP_Final_Programacion5.Areas.Act9.Models;

namespace TP_Final_Programacion5.Conexion.Act9
{
    static class MongoDBF
    {
        // Recordar, dar permisos de conexion MongoDB mediante IP fijos o 0.0.0.0/0 (¡Habilita la conexion para cualquiera!)
        static private readonly MongoClient conexion = new(Environment.GetEnvironmentVariable("MONGODB_CONEXION"));
        static private IMongoDatabase? database;
        static private IMongoCollection<Usuario>? usuariodb;
        static private IMongoCollection<Movie>? pelidb;

        public static IMongoDatabase? Database { get => database; set => database = value; }
        public static IMongoCollection<Usuario>? Usuariodb { get => usuariodb; set => usuariodb = value; }
        public static IMongoCollection<Movie>? Pelidb { get => pelidb; set => pelidb = value; }

        static public bool EstablecerConexion()
        {
            try
            {
                Database = conexion.GetDatabase(Environment.GetEnvironmentVariable("BASEDATO"));
                Usuariodb = Database.GetCollection<Usuario>(Environment.GetEnvironmentVariable("TABLAUSUARIO"));
                Pelidb = Database.GetCollection<Movie>(Environment.GetEnvironmentVariable("TABLAPELICULA"));
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        ///--------------------------------------------- Usuarios ---------------------------------------------------///
        static public List<Usuario>? ListarUsuarios()
        {
            try
            {
                List<Usuario> lista = Usuariodb.Find(_ => true).ToList();
                return lista;
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }            
        }
        static public Usuario? IngresarUsuario(string usuario, string contraseña)
        {
            try
            {
                Usuario logeado = Usuariodb.Find(u => u.User == usuario && u.Contraseña == contraseña).First();
                return logeado;
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
        }
        static public Usuario? ExisteUsuario(string usuario)
        {
            try
            {
                Usuario identificado = Usuariodb.Find(u => u.User == usuario).First();
                return identificado;
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
        }
        static public bool CrearUsuario(string usuario, string contraseña)
        {
            try
            {
                Usuariodb.InsertOneAsync(new Usuario(usuario, contraseña));
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        ///---------------------------------------------- Movie -----------------------------------------------------///
        static public List<Movie>? ListarPeliculas()
        {
            try
            {
                List<Movie> lista = Pelidb.Find(_ => true).ToList();
                return lista;
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
        }
        static public Movie? ExistePelicula(string titulo)
        {
            try
            {
                Movie identificado = Pelidb.Find(u => u.Titulo == titulo).First();
                return identificado;
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
        }
        static public bool AñadirPelicula(string titu, DateOnly fePubli, string gen, decimal pre)
        {
            try
            {
                Pelidb.InsertOneAsync(new Movie(titu, fePubli, gen, pre));
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        static public Movie? SeleccionarPelicula(string? id)
        {
            try
            {
                Movie identificado = Pelidb.Find(u => u.Id == id).First();
                return identificado;
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
        }
        static public bool ModificarPelicula(string? id, string titu, DateOnly fePubli, string gen, decimal pre)
        {
            try
            {
                var filter = Builders<Movie>.Filter.Eq(u => u.Id, id);
                var update = Builders<Movie>.Update
                    .Set(u => u.Titulo, titu)
                    .Set(u => u.FechaPublicada, fePubli)
                    .Set(u => u.Genero, gen)
                    .Set(u => u.Precio, pre);
                Pelidb.UpdateOneAsync(filter, update);
                return true;
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return false;
            }
        }
        static public bool EliminarPelicula(string? id)
        {
            try
            {
                var filter = Builders<Movie>.Filter.Eq(u => u.Id, id);
                Pelidb.DeleteOneAsync(filter);
                return true;
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
