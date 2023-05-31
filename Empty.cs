/// <summary>
/// Representa uma célula vazia no mapa.
/// </summary>
public class Empty : Cell
{
    /// <summary>
    /// Obtém ou define a coordenada X da célula vazia.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Obtém ou define a coordenada Y da célula vazia.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Inicializa uma nova instância da classe Empty com as coordenadas especificadas.
    /// </summary>
    /// <param name="x">A coordenada X da célula vazia.</param>
    /// <param name="y">A coordenada Y da célula vazia.</param>
    public Empty(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Retorna uma representação em string da célula vazia.
    /// </summary>
    /// <returns>Uma string que representa a célula vazia.</returns>
    public override string ToString()
    {
        return "--";
    }
}
