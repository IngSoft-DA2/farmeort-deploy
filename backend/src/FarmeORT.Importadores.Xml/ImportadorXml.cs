using System.Xml.Linq;
using FarmeORT.Importacion.Contratos;

namespace FarmeORT.Importadores.Xml;

// Espera: <skills><skill codigo="BAIL02" nombre="..." categoria="..." /></skills>
// Este importador NO se copia solo al publicar: en la demo se copia a mano a la carpeta Plugins
// con la API corriendo, para mostrar que aparece sin recompilar ni reiniciar.
public class ImportadorXml : IImportadorSkills
{
    public string Nombre => "XML";

    public IReadOnlyList<ParametroImportador> Parametros { get; } =
    [
        new("contenido", "XML con elementos <skill codigo nombre categoria />"),
    ];

    public IReadOnlyList<SkillImportada> Importar(IReadOnlyDictionary<string, string> parametros)
    {
        if(!parametros.TryGetValue("contenido", out var contenido) || string.IsNullOrWhiteSpace(contenido))
        {
            throw new ArgumentException("Falta el parámetro 'contenido'.");
        }

        return XDocument.Parse(contenido)
            .Descendants("skill")
            .Select(s => new SkillImportada(
                (string?)s.Attribute("codigo") ?? string.Empty,
                (string?)s.Attribute("nombre") ?? string.Empty,
                (string?)s.Attribute("categoria") ?? string.Empty))
            .ToList();
    }
}
