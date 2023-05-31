/// <summary>
/// Representa uma joia no mapa.
/// </summary>
public class Jewel : Cell
{
    /// <summary>
    /// Obtém ou define a quantidade de joias.
    /// </summary>
    public static int JewelQuantity = 0;

    /// <summary>
    /// Obtém ou define o tipo da joia.
    /// </summary>
    public JewelType type;

    /// <summary>
    /// Obtém ou define a coordenada X da joia.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Obtém ou define a coordenada Y da joia.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Retorna uma representação em string da joia.
    /// </summary>
    /// <returns>Uma string que representa a joia.</returns>
    public override string ToString()
    {
        switch (type)
        {
            case JewelType.Blue:
                return "JB";
            case JewelType.Green:
                return "JG";
            case JewelType.Red:
                return "JR";
            default:
                return "--";
        }
    }

    /// <summary>
    /// Inicializa uma nova instância da classe Jewel com o tipo e as coordenadas especificadas.
    /// </summary>
    /// <param name="tipo">O tipo da joia.</param>
    /// <param name="x">A coordenada X da joia.</param>
    /// <param name="y">A coordenada Y da joia.</param>
    public Jewel(JewelType tipo, int x, int y)
    {
        JewelQuantity += 1;
        type = tipo;
        X = x;
        Y = y;
    }
}
