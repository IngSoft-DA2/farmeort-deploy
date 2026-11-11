using FarmeORT.DTOs;
using FarmeORT.IServicios;
using Microsoft.AspNetCore.Mvc;

namespace FarmeORT.WebApi.Controllers;

[ApiController]
[Route("api/skills")]
public class SkillsController(IServicioSkills servicio) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyList<SkillResponse>> Listar() => Ok(servicio.Listar());

    [HttpPost]
    public ActionResult<SkillResponse> Crear(SkillRequest pedido)
    {
        var creada = servicio.Crear(pedido);
        return Created($"/api/skills/{creada.Codigo}", creada);
    }

    [HttpDelete("{codigo}")]
    public ActionResult<SkillResponse> Desactivar(string codigo) => Ok(servicio.Desactivar(codigo));
}
