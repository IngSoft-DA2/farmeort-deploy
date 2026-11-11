using FarmeORT.Dominio;
using FarmeORT.IServicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FarmeORT.WebApi.Filtros;

public class FiltroExcepciones(ILogger<FiltroExcepciones> logger) : IExceptionFilter
{
    public void OnException(ExceptionContext contexto)
    {
        var (estado, mensaje) = contexto.Exception switch
        {
            ExcepcionDeDominio ex => (StatusCodes.Status400BadRequest, ex.Message),
            ExcepcionPedidoInvalido ex => (StatusCodes.Status400BadRequest, ex.Message),
            ExcepcionNoEncontrado ex => (StatusCodes.Status404NotFound, ex.Message),
            ExcepcionConflicto ex => (StatusCodes.Status409Conflict, ex.Message),
            _ => (StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado."),
        };

        if(estado == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(contexto.Exception, "Error no controlado");
        }

        contexto.Result = new ObjectResult(new { mensaje }) { StatusCode = estado };
        contexto.ExceptionHandled = true;
    }
}
