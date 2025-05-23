try
{
    // Aseg�rate de tener el paquete DotNetEnv instalado: Install-Package DotNetEnv
    // Aseg�rate de que tu archivo .env est� en la ra�z del proyecto y .gitignore
    DotNetEnv.Env.Load();
    Console.WriteLine(".env file loaded successfully (for local development).");
}

catch (FileNotFoundException)
{
    Console.WriteLine(".env file not found. Skipping loading (This is expected in production or without local .env).");
}

catch (Exception ex)
{
    Console.Error.WriteLine($"Error loading .env file: {ex.Message}");
    // Decidir si este error debe detener el arranque
    throw;
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication()
    // Define el esquema de autenticaci�n para una Area, si no se usan areas, se borra el "", y solamente se deja options =>...
    .AddCookie("AreaAct5Cookies",options =>
    {
        options.Cookie.Name = "AreaAct5Cookies"; // Nombre �nico
        options.Cookie.Path = "/Act5"; // Scope limitado al �rea
        options.LoginPath = "/Act5/Usuario/Login"; // La ruta a tu p�gina de login
        options.AccessDeniedPath = "/Act5/Usuario/Login"; // La ruta a tu p�gina de acceso denegado
        options.Cookie.Path = "/Act5"; // Especifica la ruta para la cookie de AreaAct5
        options.ExpireTimeSpan = TimeSpan.FromMinutes(2); // Tiempo de expiraci�n de la cookie de autenticaci�n

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
    })
    .AddCookie("AreaAct7Cookies", options =>
    {
        options.Cookie.Name = "AreaAct7Cookies";
        options.Cookie.Path = "/Act7";
        options.LoginPath = "/Act7/Usuario/Login";
        options.AccessDeniedPath = "/Act7/Usuario/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(2);

        options.SlidingExpiration = true;
    })
    .AddCookie("AreaAct8Cookies", options =>
    {
        options.Cookie.Name = "AreaAct8Cookies";
        options.Cookie.Path = "/Act8";
        options.LoginPath = "/Act8/Usuario/Login";
        options.AccessDeniedPath = "/Act8/Usuario/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(2);

        options.SlidingExpiration = true;
    })
    .AddCookie("AreaAct9Cookies", options =>
    {
        options.Cookie.Name = "AreaAct9Cookies";
        options.Cookie.Path = "/Act9";
        options.LoginPath = "/Act9/Usuario/Login";
        options.AccessDeniedPath = "/Act9/Usuario/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(2);

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

// Aqui iran las 16 Areas a crear
#region Areas
app.MapAreaControllerRoute(
    name: "Act9Area",
    areaName: "Act9",
    pattern: "Act9/{controller=Home}/{action=Index}/{id?}"
);

app.MapAreaControllerRoute(
    name: "Act8Area",
    areaName: "Act8",
    pattern: "Act8/{controller=Home}/{action=Index}/{id?}"
);

app.MapAreaControllerRoute(
    name: "Act7Area",
    areaName: "Act7",
    pattern: "Act7/{controller=Home}/{action=Index}/{id?}"
);

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
    name: "Act1Area",          // Un nombre �nico para esta ruta de �rea
    areaName: "Act1",          // El nombre del �rea (debe coincidir con [Area("Act1")] y la carpeta)
    pattern: "Act1/{controller=Home}/{action=Index}/{id?}"// El patr�n de la URL para esta �rea
);
#endregion

// Ruta principal de la aplicacion
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"// Aqui se indica con que controlador se inicia, junto a su accion.
);

app.Run();
