/// <summary>
/// Representa um obstáculo no mapa.
/// </summary>
public class Obstacle : Cell
{
    /// <summary>
    /// A coordenada X do obstáculo.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// A coordenada Y do obstáculo.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// O tipo de obstáculo.
    /// </summary>
    public ObstacleType tipo;

    /// <summary>
    /// Cria um novo obstáculo com o tipo, coordenada X e coordenada Y especificados.
    /// </summary>
    /// <param name="type">O tipo de obstáculo.</param>
    /// <param name="x">A coordenada X do obstáculo.</param>
    /// <param name="y">A coordenada Y do obstáculo.</param>
    public Obstacle(ObstacleType type, int x, int y)
    {
        tipo = type;
        X = x;
        Y = y;
    }

    /// <summary>
    /// Retorna a representação do obstáculo em forma de string.
    /// </summary>
    /// <returns>A representação do obstáculo.</returns>
    public override string ToString()
    {
        switch (tipo)
        {
            case ObstacleType.Tree:
                return "$$";
            case ObstacleType.Water:
                return "##";
            case ObstacleType.Uranium:
                return "!!";
            default:
                return "--";
        }
    }
}
