var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("AreaAct5Cookies")// �Tanto el nombre de autentificacion y la cookie a�adida, tienen que tener el mismo nombre!
    // Define el esquema de autenticaci�n para una Area, si no se usan areas, se borra el "", y solamente se deja options =>...
    .AddCookie("AreaAct5Cookies",options =>
    {
        options.LoginPath = "/Act5/Usuario/Login"; // La ruta a tu p�gina de login
        options.AccessDeniedPath = "/Act5/Usuario/Login"; // La ruta a tu p�gina de acceso denegado
        options.ExpireTimeSpan = TimeSpan.FromMinutes(2); // Tiempo de expiraci�n de la cookie de autenticaci�n
        // Otras opciones de configuraci�n de la cookie

        // Si quieres que se renueve con cada request activa
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(); // Agrega el servicio de autorizaci�n
builder.Services.AddHttpContextAccessor(); // "Facilita" el acceso a los datos de la cookies
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseSession(); // Permite a la aplicacion utilizar variables de sesiones

app.UseAuthentication(); // Este middleware debe estar antes de UseAuthorization
app.UseAuthorization();


// Areas definidas
app.MapAreaControllerRoute(
    name: "Act5Area",
    areaName: "Act5",
    pattern: "Act5/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Act4Area",
    areaName: "Act4",
    pattern: "Act4/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Act3Area",
    areaName: "Act3",
    pattern: "Act3/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Act2Area",
    areaName: "Act2",
    pattern: "Act2/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Act1Area",          // Un nombre �nico para esta ruta de �rea
    areaName: "Act1",          // El nombre del �rea (debe coincidir con [Area("Act1")] y la carpeta)
    pattern: "Act1/{controller=Home}/{action=Index}/{id?}"); // El patr�n de la URL para esta �rea
// ************************************************************** //
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Aqui se indica con que controlador se inicia, junto a su accion.

app.Run();
