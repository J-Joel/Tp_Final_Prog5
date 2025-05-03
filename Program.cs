var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication()
    // Define el esquema de autenticación para una Area, si no se usan areas, se borra el "", y solamente se deja options =>...
    .AddCookie("AreaAct5Cookies",options =>
    {
        options.Cookie.Name = "AreaAct5Cookies"; // Nombre único
        options.Cookie.Path = "/Act5"; // Scope limitado al área
        options.LoginPath = "/Act5/Usuario/Login"; // La ruta a tu página de login
        options.AccessDeniedPath = "/Act5/Usuario/Login"; // La ruta a tu página de acceso denegado
        options.Cookie.Path = "/Act5"; // Especifica la ruta para la cookie de AreaAct5
        options.ExpireTimeSpan = TimeSpan.FromMinutes(2); // Tiempo de expiración de la cookie de autenticación

        // Si quieres que se renueve con cada request activa
        options.SlidingExpiration = true;
    })
    .AddCookie("AreaAct6Cookies", options =>
    {
        options.Cookie.Name = "AreaAct6Cookies";
        options.Cookie.Path = "/Act6";
        options.LoginPath = "/Act6/Usuario/Login";
        options.AccessDeniedPath = "/Act6/Usuario/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(2);

        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(); // Agrega el servicio de autorización
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

// Aqui iran las 16 Areas a crear
#region Areas
app.MapAreaControllerRoute(
    name: "Act6Area",
    areaName: "Act6",
    pattern: "Act6/{controller=Home}/{action=Index}/{id?}"
);

app.MapAreaControllerRoute(
    name: "Act5Area",
    areaName: "Act5",
    pattern: "Act5/{controller=Home}/{action=Index}/{id?}"
);

app.MapAreaControllerRoute(
    name: "Act4Area",
    areaName: "Act4",
    pattern: "Act4/{controller=Home}/{action=Index}/{id?}"
);

app.MapAreaControllerRoute(
    name: "Act3Area",
    areaName: "Act3",
    pattern: "Act3/{controller=Home}/{action=Index}/{id?}"
);

app.MapAreaControllerRoute(
    name: "Act2Area",
    areaName: "Act2",
    pattern: "Act2/{controller=Home}/{action=Index}/{id?}"
);

app.MapAreaControllerRoute(
    name: "Act1Area",          // Un nombre único para esta ruta de área
    areaName: "Act1",          // El nombre del área (debe coincidir con [Area("Act1")] y la carpeta)
    pattern: "Act1/{controller=Home}/{action=Index}/{id?}"// El patrón de la URL para esta área
);
#endregion

// Ruta principal de la aplicacion
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"// Aqui se indica con que controlador se inicia, junto a su accion.
);

app.Run();
