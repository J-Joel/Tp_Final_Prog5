using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TP_Final_Programacion5.BaseDeDatoLocal.Act8;

namespace TP_Final_Programacion5.Areas.Act8.Controllers
{
    [Area("Act8")]
    public class UsuarioController : Controller
    {
        [Authorize(AuthenticationSchemes = "AreaAct8Cookies")]
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
            var user = TablaUsuarios.Usuarios.FirstOrDefault(u => u.User == usuario && u.Contraseña == contraseña);
            if (user != null) {
                var claims = new List<Claim>
                {
                    // Almacena los datos del usuario
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.User),
                };
                var claimsIdentity = new ClaimsIdentity(claims, "AreaAct8Cookies"); // Tiene que tener el mismo nombre que en el program
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                // Propiedades de autenticación
                HttpContext.SignInAsync("AreaAct8Cookies", claimsPrincipal);
                HttpContext.User = new ClaimsPrincipal(claimsIdentity);

                TempData["Completado"] = "Se ha iniciado sesión correctamente.";
                return Json(new { success = true, redirect = Url.Action("Welcome", "Home") });
            }
            ViewBag.Error = "Usuario o contraseña incorrecta";
            return PartialView("_LoginModal");
        }
        [HttpGet]
        public IActionResult Registro()
        {
            return PartialView("_RegistroModal");
        }
        [HttpPost]
        public IActionResult Registro(string usuario, string contraseña)
        {
            // Verifica si la petición fue hecha por AJAX
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var usuarioExistente = TablaUsuarios.Usuarios.FirstOrDefault(u => u.User == usuario);
                if (usuarioExistente == null)
                {
                    if (!TablaUsuarios.CrearUsuario(usuario, contraseña))
                    {
                        ViewBag.Error = "Error al crear el usuario";
                        return PartialView("_RegistroModal");
                    }
                    ViewBag.Error = "Usuario creado";
                    return PartialView("_LoginModal");       
                }
                ViewBag.Error = "El usuario ya está registrado";
                return PartialView("_RegistroModal");
            }
            else // Si la petición NO fue AJAX
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("AreaAct8Cookies");
            return RedirectToAction("Index", "Home");
        }
    }
}
