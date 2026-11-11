using FarmeORT.DTOs;

namespace FarmeORT.IServicios;

public interface IServicioSkills
{
    IReadOnlyList<SkillResponse> Listar();

    SkillResponse Crear(SkillRequest pedido);

    SkillResponse Desactivar(string codigo);
}
