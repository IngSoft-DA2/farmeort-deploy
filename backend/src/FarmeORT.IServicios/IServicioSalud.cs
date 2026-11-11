using FarmeORT.DTOs;

namespace FarmeORT.IServicios;

public interface IServicioSalud
{
    SaludResponse Consultar(string entorno);
}
