namespace FarmeORT.Dominio;

public class Skill
{
    public const int LargoMinimoCodigo = 3;
    public const int LargoMaximoCodigo = 10;
    public const int LargoMaximoNombre = 50;
    public const int LargoMaximoCategoria = 30;

    public Skill(string codigo, string nombre, string categoria)
    {
        Codigo = ValidarCodigo(codigo);
        Nombre = ValidarTexto(nombre, "nombre", LargoMaximoNombre);
        Categoria = ValidarTexto(categoria, "categoría", LargoMaximoCategoria);
        Activa = true;
    }

    // Constructor para Entity Framework.
    private Skill()
    {
        Codigo = string.Empty;
        Nombre = string.Empty;
        Categoria = string.Empty;
    }

    public int Id { get; private set; }

    public string Codigo { get; private set; }

    public string Nombre { get; private set; }

    public string Categoria { get; private set; }

    public bool Activa { get; private set; }

    public void Desactivar()
    {
        if(!Activa)
        {
            throw new ExcepcionDeDominio($"La skill {Codigo} ya está inactiva.");
        }

        Activa = false;
    }

    private static string ValidarCodigo(string codigo)
    {
        var limpio = (codigo ?? string.Empty).Trim().ToUpperInvariant();
        if(limpio.Length < LargoMinimoCodigo || limpio.Length > LargoMaximoCodigo || !limpio.All(char.IsLetterOrDigit))
        {
            throw new ExcepcionDeDominio(
                $"El código debe tener entre {LargoMinimoCodigo} y {LargoMaximoCodigo} letras o números.");
        }

        return limpio;
    }

    private static string ValidarTexto(string valor, string campo, int largoMaximo)
    {
        var limpio = (valor ?? string.Empty).Trim();
        if(limpio.Length == 0 || limpio.Length > largoMaximo)
        {
            throw new ExcepcionDeDominio($"El campo {campo} es obligatorio y admite hasta {largoMaximo} caracteres.");
        }

        return limpio;
    }
}
