using FarmeORT.Repositorios;
using FarmeORT.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace FarmeORT.Fabrica;

public static class RegistroDependencias
{
    public static IServiceCollection AgregarDependencias(
        this IServiceCollection servicios,
        string connectionString,
        string carpetaPlugins)
    {
        servicios.AgregarServicios(ResolverCarpeta(carpetaPlugins));
        servicios.AgregarRepositorios(connectionString);
        return servicios;
    }

    public static void PrepararBaseDeDatos(this IServiceProvider proveedor) => proveedor.AplicarMigraciones();

    // Una ruta relativa se toma desde la carpeta donde está la dll de la API (AppContext.BaseDirectory),
    // no desde el directorio de trabajo: así funciona igual con dotnet run, con la carpeta publicada,
    // en IIS o en un contenedor.
    private static string ResolverCarpeta(string carpeta) =>
        Path.IsPathRooted(carpeta) ? carpeta : Path.Combine(AppContext.BaseDirectory, carpeta);
}
