using FarmeORT.IRepositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FarmeORT.Repositorios;

public static class RegistroRepositorios
{
    public static IServiceCollection AgregarRepositorios(this IServiceCollection servicios, string connectionString)
    {
        // EnableRetryOnFailure: reintenta si la base tarda en responder (por ejemplo, un contenedor
        // que recién arranca o una base en la nube que estaba pausada).
        servicios.AddDbContext<ContextoFarmeORT>(opciones =>
            opciones.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure()));
        servicios.AddScoped<IRepositorioSkills, RepositorioSkills>();
        servicios.AddScoped<IEstadoBaseDeDatos, EstadoBaseDeDatos>();
        return servicios;
    }

    public static void AplicarMigraciones(this IServiceProvider proveedor)
    {
        using var alcance = proveedor.CreateScope();
        alcance.ServiceProvider.GetRequiredService<ContextoFarmeORT>().Database.Migrate();
    }
}
