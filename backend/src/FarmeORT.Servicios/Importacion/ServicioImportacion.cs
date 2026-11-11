using FarmeORT.Dominio;
using FarmeORT.DTOs;
using FarmeORT.Importacion.Contratos;
using FarmeORT.IRepositorios;
using FarmeORT.IServicios;

namespace FarmeORT.Servicios.Importacion;

public class ServicioImportacion(CargadorDeImportadores cargador, IRepositorioSkills repositorio) : IServicioImportacion
{
    public IReadOnlyList<ImportadorResponse> ListarImportadores() =>
        cargador.Cargar()
            .Select(importador => new ImportadorResponse(
                importador.Nombre,
                importador.Parametros.Select(p => new ParametroResponse(p.Nombre, p.Descripcion)).ToList()))
            .ToList();

    // La importación no es todo o nada: cada skill pasa por las mismas validaciones que el alta.
    public ResultadoImportacionResponse Importar(string nombreImportador, ImportacionRequest pedido)
    {
        var importador = cargador.Cargar()
            .FirstOrDefault(i => string.Equals(i.Nombre, nombreImportador, StringComparison.OrdinalIgnoreCase))
            ?? throw new ExcepcionNoEncontrado($"No hay un importador llamado {nombreImportador}.");

        IReadOnlyList<SkillImportada> leidas;
        try
        {
            leidas = importador.Importar(pedido.Parametros ?? []);
        }
        catch(Exception ex)
        {
            throw new ExcepcionPedidoInvalido($"El importador {importador.Nombre} no pudo leer los datos: {ex.Message}");
        }

        var rechazadas = new List<SkillRechazadaResponse>();
        var importadas = 0;
        foreach(var leida in leidas)
        {
            try
            {
                var skill = new Skill(leida.Codigo, leida.Nombre, leida.Categoria);
                if(repositorio.ExisteCodigo(skill.Codigo))
                {
                    rechazadas.Add(new SkillRechazadaResponse(skill.Codigo, "Ya existe una skill con ese código."));
                    continue;
                }

                repositorio.Agregar(skill);
                repositorio.GuardarCambios();
                importadas++;
            }
            catch(ExcepcionDeDominio ex)
            {
                rechazadas.Add(new SkillRechazadaResponse(leida.Codigo, ex.Message));
            }
        }

        return new ResultadoImportacionResponse(importador.Nombre, importadas, rechazadas);
    }
}
