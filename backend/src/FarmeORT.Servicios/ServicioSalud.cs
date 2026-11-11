using FarmeORT.DTOs;
using FarmeORT.IRepositorios;
using FarmeORT.IServicios;
using FarmeORT.Servicios.Importacion;

namespace FarmeORT.Servicios;

public class ServicioSalud(IEstadoBaseDeDatos baseDeDatos, CargadorDeImportadores cargador) : IServicioSalud
{
    public SaludResponse Consultar(string entorno)
    {
        var conectada = baseDeDatos.PuedeConectar();
        return new SaludResponse(
            conectada ? "OK" : "SIN_BASE",
            entorno,
            conectada,
            cargador.Carpeta,
            cargador.Cargar().Select(i => i.Nombre).ToList());
    }
}
