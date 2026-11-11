using FarmeORT.IRepositorios;

namespace FarmeORT.Repositorios;

public class EstadoBaseDeDatos(ContextoFarmeORT contexto) : IEstadoBaseDeDatos
{
    public bool PuedeConectar()
    {
        try
        {
            return contexto.Database.CanConnect();
        }
        catch
        {
            return false;
        }
    }
}
