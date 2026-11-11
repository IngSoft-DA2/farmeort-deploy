using FarmeORT.DTOs;
using FarmeORT.IServicios;
using Microsoft.AspNetCore.Mvc;

namespace FarmeORT.WebApi.Controllers;

[ApiController]
[Route("api/importadores")]
public class ImportadoresController(IServicioImportacion servicio) : ControllerBase
{
    // La UI arma el menú y el formulario de cada importador a partir de esta lista.
    [HttpGet]
    public ActionResult<IReadOnlyList<ImportadorResponse>> Listar() => Ok(servicio.ListarImportadores());

    [HttpPost("{nombre}/importaciones")]
    public ActionResult<ResultadoImportacionResponse> Importar(string nombre, ImportacionRequest pedido) =>
        Ok(servicio.Importar(nombre, pedido));
}
