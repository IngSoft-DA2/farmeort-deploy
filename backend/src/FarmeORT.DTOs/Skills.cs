using FarmeORT.Dominio;

namespace FarmeORT.DTOs;

public record SkillRequest(string Codigo, string Nombre, string Categoria);

public record SkillResponse(int Id, string Codigo, string Nombre, string Categoria, bool Activa)
{
    public static SkillResponse Desde(Skill skill) =>
        new(skill.Id, skill.Codigo, skill.Nombre, skill.Categoria, skill.Activa);
}
