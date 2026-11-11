using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FarmeORT.Repositorios;

// Solo la usa `dotnet ef` (migraciones y scripts). Toma el connection string de la variable de entorno;
// para generar migraciones o scripts no hace falta que la base exista.
public class FabricaContextoDisenio : IDesignTimeDbContextFactory<ContextoFarmeORT>
{
    public ContextoFarmeORT CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__FarmeORT")
            ?? "Server=localhost,1433;Database=FarmeORT;User Id=sa;Password=no-se-usa;TrustServerCertificate=True";
        var opciones = new DbContextOptionsBuilder<ContextoFarmeORT>().UseSqlServer(connectionString).Options;
        return new ContextoFarmeORT(opciones);
    }
}
