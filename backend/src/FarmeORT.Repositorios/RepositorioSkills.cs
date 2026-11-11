using FarmeORT.Dominio;
using FarmeORT.IRepositorios;

namespace FarmeORT.Repositorios;

public class RepositorioSkills(ContextoFarmeORT contexto) : IRepositorioSkills
{
    public IReadOnlyList<Skill> Listar() => contexto.Skills.OrderBy(s => s.Codigo).ToList();

    public Skill? BuscarPorCodigo(string codigo) => contexto.Skills.FirstOrDefault(s => s.Codigo == codigo);

    public bool ExisteCodigo(string codigo) => contexto.Skills.Any(s => s.Codigo == codigo);

    public void Agregar(Skill skill) => contexto.Skills.Add(skill);

    public void GuardarCambios() => contexto.SaveChanges();
}
