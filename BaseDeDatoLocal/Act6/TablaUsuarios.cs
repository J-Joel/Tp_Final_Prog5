using TP_Final_Programacion5.Areas.Act6.Models;

namespace TP_Final_Programacion5.BaseDeDatoLocal.Act6
{
    static class TablaUsuarios
    {
        static public List<Usuario> usuarios =
        [
            new Usuario
            {
                Id = 0,
                User = "admin",
                Contraseña = "admin"
            },
            new Usuario
            {
                Id = 1,
                User = "1",
                Contraseña = "1"
            },
        ];
        static private int usuariostotal = 2;
        static public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }
        static public int Usuariostotal { get => usuariostotal; set => usuariostotal = value; }

        static public bool CrearUsuario(string user, string contra) 
        {
            try
            {
                var nuevoUsuario = new Usuario
                {
                    Id = Usuariostotal,
                    User = user,
                    Contraseña = contra
                };
                Usuarios.Add(nuevoUsuario);
                usuariostotal++;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
