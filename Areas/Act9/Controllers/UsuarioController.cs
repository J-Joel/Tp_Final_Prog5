using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TP_Final_Programacion5.Conexion.Act9;

namespace TP_Final_Programacion5.Areas.Act9.Controllers
{
    [Area("Act9")]
    public class UsuarioController : Controller
    {
        [Authorize(AuthenticationSchemes = "AreaAct9Cookies")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login() 
        {
            return PartialView("_LoginModal");
        }
        [HttpPost]
        public IActionResult Login(string usuario, string contraseña)
        {
            if (!MongoDBF.EstablecerConexion())
            {
                ViewBag.Error = "Conexion fallida";
                return PartialView("_LoginModal");
            }

            var user = MongoDBF.IngresarUsuario(usuario, contraseña);
            if (user == null) {
                ViewBag.Error = "Usuario o contraseña incorrecta";
                return PartialView("_LoginModal");
            }

            var claims = new List<Claim>
                {
                    // Almacena los datos del usuario
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Name, user.User),
                };
            var claimsIdentity = new ClaimsIdentity(claims, "AreaAct9Cookies"); // Tiene que tener el mismo nombre que en el program
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            // Propiedades de autenticación
            HttpContext.SignInAsync("AreaAct9Cookies", claimsPrincipal);
            HttpContext.User = new ClaimsPrincipal(claimsIdentity);

            TempData["Completado"] = "Se ha iniciado sesión correctamente.";
            return Json(new { success = true, redirect = Url.Action("Welcome", "Home") });
        }
        [HttpGet]
        public IActionResult Registro()
        {
            return PartialView("_RegistroModal");
        }
        [HttpPost]
        public IActionResult Registro(string usuario, string contraseña)
        {
            // Verifica si la petición no fue hecha por AJAX
            if (!(Request.Headers["X-Requested-With"] == "XMLHttpRequest"))
            {
                return RedirectToAction("Index", "Usuario");
            }

            if (!MongoDBF.EstablecerConexion())
            {
                ViewBag.Error = "Conexion fallida";
                return PartialView("_RegistroModal");
            }

            if (MongoDBF.ExisteUsuario(usuario) != null)
            {
                ViewBag.Error = "El usuario ya está registrado";
                return PartialView("_RegistroModal");
            }
            if (!MongoDBF.CrearUsuario(usuario, contraseña))
            {
                ViewBag.Error = "Error al crear el usuario";
                return PartialView("_RegistroModal");
            }
            ViewBag.Error = "Usuario creado";
            return PartialView("_LoginModal");

        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("AreaAct9Cookies");
            return RedirectToAction("Index", "Home");
        }
    }
}
