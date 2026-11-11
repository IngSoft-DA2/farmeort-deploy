using FarmeORT.IServicios;
using FarmeORT.Servicios.Importacion;
using Microsoft.Extensions.DependencyInjection;

namespace FarmeORT.Servicios;

public static class RegistroServicios
{
    public static IServiceCollection AgregarServicios(this IServiceCollection servicios, string carpetaPlugins)
    {
        servicios.AddSingleton(new OpcionesPlugins(carpetaPlugins));
        servicios.AddSingleton<CargadorDeImportadores>();
        servicios.AddScoped<IServicioSkills, ServicioSkills>();
        servicios.AddScoped<IServicioImportacion, ServicioImportacion>();
        servicios.AddScoped<IServicioSalud, ServicioSalud>();
        return servicios;
    }
}
