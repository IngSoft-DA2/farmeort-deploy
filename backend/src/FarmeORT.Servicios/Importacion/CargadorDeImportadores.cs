using System.Reflection;
using FarmeORT.Importacion.Contratos;

namespace FarmeORT.Servicios.Importacion;

// Descubre importadores por reflection en la carpeta de plugins. Recorre la carpeta en cada pedido,
// así que un importador nuevo se toma al copiar su dll, sin recompilar ni reiniciar la API.
public class CargadorDeImportadores(OpcionesPlugins opciones)
{
    public string Carpeta => opciones.Carpeta;

    public IReadOnlyList<IImportadorSkills> Cargar()
    {
        if(!Directory.Exists(opciones.Carpeta))
        {
            return [];
        }

        var importadores = new List<IImportadorSkills>();
        foreach(var ruta in Directory.GetFiles(opciones.Carpeta, "*.dll"))
        {
            importadores.AddRange(CrearImportadores(ruta));
        }

        return importadores.OrderBy(importador => importador.Nombre).ToList();
    }

    private static IEnumerable<IImportadorSkills> CrearImportadores(string ruta)
    {
        Type[] tipos;
        try
        {
            // LoadFrom: el contrato (FarmeORT.Importacion.Contratos) ya está cargado por la API,
            // así que el plugin usa esa misma versión. Por eso en Plugins va solo la dll del importador.
            tipos = Assembly.LoadFrom(ruta).GetExportedTypes();
        }
        catch(Exception ex) when(ex is BadImageFormatException or FileLoadException or ReflectionTypeLoadException)
        {
            // Una dll que no es un importador válido no debe tirar abajo a las demás.
            yield break;
        }

        foreach(var tipo in tipos.Where(EsImportador))
        {
            yield return (IImportadorSkills)Activator.CreateInstance(tipo)!;
        }
    }

    private static bool EsImportador(Type tipo) =>
        typeof(IImportadorSkills).IsAssignableFrom(tipo)
        && tipo is { IsClass: true, IsAbstract: false }
        && tipo.GetConstructor(Type.EmptyTypes) is not null;
}
