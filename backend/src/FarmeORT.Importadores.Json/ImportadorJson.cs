using System.Text.Json;
using FarmeORT.Importacion.Contratos;

namespace FarmeORT.Importadores.Json;

// Espera un arreglo JSON: [{ "codigo": "GEST02", "nombre": "...", "categoria": "..." }]
public class ImportadorJson : IImportadorSkills
{
    public string Nombre => "JSON";

    public IReadOnlyList<ParametroImportador> Parametros { get; } =
    [
        new("contenido", "Arreglo JSON de skills con codigo, nombre y categoria"),
    ];

    public IReadOnlyList<SkillImportada> Importar(IReadOnlyDictionary<string, string> parametros)
    {
        if(!parametros.TryGetValue("contenido", out var contenido) || string.IsNullOrWhiteSpace(contenido))
        {
            throw new ArgumentException("Falta el parámetro 'contenido'.");
        }

        var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var leidas = JsonSerializer.Deserialize<List<SkillJson>>(contenido, opciones) ?? [];
        return leidas.Select(s => new SkillImportada(s.Codigo ?? string.Empty, s.Nombre ?? string.Empty, s.Categoria ?? string.Empty))
            .ToList();
    }

    private sealed record SkillJson(string? Codigo, string? Nombre, string? Categoria);
}
