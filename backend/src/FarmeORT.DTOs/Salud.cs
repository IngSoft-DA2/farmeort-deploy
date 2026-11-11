namespace FarmeORT.DTOs;

// Lo que devuelve GET /api/salud: sirve para verificar un deploy de un vistazo.
public record SaludResponse(
    string Estado,
    string Entorno,
    bool BaseDeDatosConectada,
    string CarpetaPlugins,
    IReadOnlyList<string> Importadores);
