using FarmeORT.DTOs;
using FarmeORT.IServicios;
using Microsoft.AspNetCore.Mvc;

namespace FarmeORT.WebApi.Controllers;

// GET /api/salud: primer endpoint a probar después de cada deploy.
[ApiController]
[Route("api/salud")]
public class SaludController(IServicioSalud servicio, IWebHostEnvironment entorno) : ControllerBase
{
    [HttpGet]
    public ActionResult<SaludResponse> Consultar() => Ok(servicio.Consultar(entorno.EnvironmentName));
}
