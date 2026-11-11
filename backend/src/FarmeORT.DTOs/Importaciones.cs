namespace FarmeORT.DTOs;

public record ParametroResponse(string Nombre, string Descripcion);

public record ImportadorResponse(string Nombre, IReadOnlyList<ParametroResponse> Parametros);

public record ImportacionRequest(Dictionary<string, string> Parametros);

public record SkillRechazadaResponse(string Codigo, string Motivo);

public record ResultadoImportacionResponse(
    string Importador,
    int Importadas,
    IReadOnlyList<SkillRechazadaResponse> Rechazadas);
