using FarmeORT.DTOs;

namespace FarmeORT.IServicios;

public interface IServicioImportacion
{
    IReadOnlyList<ImportadorResponse> ListarImportadores();

    ResultadoImportacionResponse Importar(string nombreImportador, ImportacionRequest pedido);
}
