using FarmeORT.Fabrica;
using FarmeORT.WebApi.Filtros;

var builder = WebApplication.CreateBuilder(args);

// 1. Connection string: NO va en appsettings.json (quedaría la contraseña en el repo).
//    Llega por variable de entorno ConnectionStrings__FarmeORT o por appsettings.Development.json (ignorado por git).
var connectionString = builder.Configuration.GetConnectionString("FarmeORT")
    ?? throw new InvalidOperationException(
        "Falta el connection string 'FarmeORT'. Definí la variable de entorno ConnectionStrings__FarmeORT.");

// 2. CORS: los orígenes del front vienen de la configuración (Cors:OrigenesPermitidos).
//    En la nube se agregan con variables de entorno Cors__OrigenesPermitidos__0, __1, ...
var origenesPermitidos = builder.Configuration.GetSection("Cors:OrigenesPermitidos").Get<string[]>() ?? [];

// 3. Carpeta de plugins: relativa a la carpeta de la API (la resuelve la Fábrica).
var carpetaPlugins = builder.Configuration["Plugins:Carpeta"] ?? "Plugins";

builder.Services.AddControllers(opciones => opciones.Filters.Add<FiltroExcepciones>());
builder.Services.AddCors(opciones => opciones.AddPolicy(
    "Frontend",
    politica => politica.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod()));
builder.Services.AgregarDependencias(connectionString, carpetaPlugins);

var app = builder.Build();

app.Logger.LogInformation("Entorno: {Entorno}", app.Environment.EnvironmentName);
app.Logger.LogInformation("Orígenes CORS permitidos: {Origenes}", string.Join(", ", origenesPermitidos));

// 4. Base de datos: aplica las migraciones al arrancar si así se configura. Así una base vacía
//    (contenedor nuevo, base en la nube) queda con las tablas y los datos de prueba.
if(builder.Configuration.GetValue<bool>("BaseDeDatos:AplicarMigracionesAlIniciar"))
{
    app.Services.PrepararBaseDeDatos();
    app.Logger.LogInformation("Migraciones aplicadas.");
}

// 5. Sin UseHttpsRedirection: la API corre en http detrás de quien pone el https
//    (el túnel de Cloudflare, IIS o el proveedor de la nube). Redirigir a https acá rompe esos casos.
app.UseCors("Frontend");
app.MapControllers();

app.Run();
