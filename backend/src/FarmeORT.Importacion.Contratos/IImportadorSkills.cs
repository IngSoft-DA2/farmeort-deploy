namespace FarmeORT.Importacion.Contratos;

// Contrato que implementa cada importador. Se publica como un assembly aparte para que un tercero
// pueda escribir un importador referenciando solo este proyecto, sin el resto de la solución.
public interface IImportadorSkills
{
    // Nombre único con el que la API y la UI identifican al importador.
    string Nombre { get; }

    // Parámetros que la UI le pide al usuario (por ejemplo, el contenido del archivo).
    IReadOnlyList<ParametroImportador> Parametros { get; }

    IReadOnlyList<SkillImportada> Importar(IReadOnlyDictionary<string, string> parametros);
}

public record ParametroImportador(string Nombre, string Descripcion);

public record SkillImportada(string Codigo, string Nombre, string Categoria);
