using FarmeORT.Dominio;

namespace FarmeORT.IRepositorios;

public interface IRepositorioSkills
{
    IReadOnlyList<Skill> Listar();

    Skill? BuscarPorCodigo(string codigo);

    bool ExisteCodigo(string codigo);

    void Agregar(Skill skill);

    void GuardarCambios();
}
