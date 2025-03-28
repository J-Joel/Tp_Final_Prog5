using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;

namespace TP_Final_Programacion5.Controllers
{
    public class Act2Controller : Controller
    {
        // 
        // GET: /HelloWorld/ Reemplazar el método index:
        public string Index()
        {
            return "Hola esto es una cadena de texto(¿o un Json?), recuerde acceder a la otra accion mediante la url con ../welcome/[N°]?name=[Nombre] o ../welcome?name=[Nombre]&id=[N°]";
        }
        /*

        public IActionResult Index()
        {
            return Json(new { mensaje = "Hola esto es Json(¿o un diccionario?), recuerde acceder a la otra accion mediante la url con ../welcome/[N°]?name=[Nombre] o ../welcome?name=[Nombre]&id=[N°]" });
        }*/

        // GET: /HelloWorld/Welcome/ 
        // Requires using System.Text.Encodings.Web;
        public string Welcome(string name, int ID = 1)
        {
            return HtmlEncoder.Default.Encode($"Hola {name}, ID: {ID}");
        }
    }
}
