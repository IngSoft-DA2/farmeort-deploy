using FarmeORT.Dominio;
using FarmeORT.DTOs;
using FarmeORT.IRepositorios;
using FarmeORT.IServicios;

namespace FarmeORT.Servicios;

public class ServicioSkills(IRepositorioSkills repositorio) : IServicioSkills
{
    public IReadOnlyList<SkillResponse> Listar() =>
        repositorio.Listar().Select(SkillResponse.Desde).ToList();

    public SkillResponse Crear(SkillRequest pedido)
    {
        var skill = new Skill(pedido.Codigo, pedido.Nombre, pedido.Categoria);
        if(repositorio.ExisteCodigo(skill.Codigo))
        {
            throw new ExcepcionConflicto($"Ya existe una skill con el código {skill.Codigo}.");
        }

        repositorio.Agregar(skill);
        repositorio.GuardarCambios();
        return SkillResponse.Desde(skill);
    }

    public SkillResponse Desactivar(string codigo)
    {
        var skill = repositorio.BuscarPorCodigo(codigo.Trim().ToUpperInvariant())
            ?? throw new ExcepcionNoEncontrado($"No existe la skill {codigo}.");
        skill.Desactivar();
        repositorio.GuardarCambios();
        return SkillResponse.Desde(skill);
    }
}
