/// <summary>
/// Interface que representa uma célula no mapa.
/// </summary>
public interface Cell
{
    /// <summary>
    /// Obtém ou define a coordenada X da célula.
    /// </summary>
    int X { get; set; }

    /// <summary>
    /// Obtém ou define a coordenada Y da célula.
    /// </summary>
    int Y { get; set; }
}
