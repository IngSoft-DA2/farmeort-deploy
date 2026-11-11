namespace FarmeORT.IServicios;

public class ExcepcionNoEncontrado(string mensaje) : Exception(mensaje);

public class ExcepcionConflicto(string mensaje) : Exception(mensaje);

public class ExcepcionPedidoInvalido(string mensaje) : Exception(mensaje);
