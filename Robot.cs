/// <summary>
/// Classe que representa um robô.
/// Herda da classe Cell.
/// </summary>
public class Robot : Cell
{
    /// <summary>
    /// Coordenada X do robô.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Coordenada Y do robô.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Lista de itens na sacola do robô.
    /// </summary>
    public List<int> sacola = new List<int>();

    /// <summary>
    /// Mapa em que o robô está localizado.
    /// </summary>
    public Map mapa;

    /// <summary>
    /// Energia atual do robô.
    /// </summary>
    public int Energy { get; private set; }

    /// <summary>
    /// Construtor da classe Robot.
    /// </summary>
    /// <param name="x">Coordenada X inicial do robô.</param>
    /// <param name="y">Coordenada Y inicial do robô.</param>
    /// <param name="mapa_inicial">Mapa em que o robô está localizado.</param>
    public Robot(int x, int y, Map mapa_inicial)
    {
        X = x;
        Y = y;
        mapa = mapa_inicial;
        Energy = 5;
    }
    /// <summary>
    /// Retorna uma representação em string do robô.
    /// </summary>
    /// <returns>Uma string que representa o robô.</returns>
    public override string ToString()
    {
        return "ME";
    }


    /// <summary>
    /// Verifica se há urânio nas posições adjacentes ao robô e na posição atual do robô.
    /// Perde energia se houver urânio nas posições adjacentes.
    /// Remove o obstáculo de urânio e perde mais energia se o robô estiver sobre o urânio.
    /// </summary>
    public void VerifyUranium()
    {
        bool isAdjacentUranium = false;
        bool isSamePositionUranium = false;

        if (Y - 1 >= 0 && mapa.mapa[Y - 1, X] is Obstacle && ((Obstacle)mapa.mapa[Y - 1, X]).tipo == ObstacleType.Uranium)
        {
            isAdjacentUranium = true;
        }
        else if (X - 1 >= 0 && mapa.mapa[Y, X - 1] is Obstacle && ((Obstacle)mapa.mapa[Y, X - 1]).tipo == ObstacleType.Uranium)
        {
            isAdjacentUranium = true;
        }
        else if (Y + 1 < mapa.rowLength && mapa.mapa[Y + 1, X] is Obstacle && ((Obstacle)mapa.mapa[Y + 1, X]).tipo == ObstacleType.Uranium)
        {
            isAdjacentUranium = true;
        }
        else if (X + 1 < mapa.colLength && mapa.mapa[Y, X + 1] is Obstacle && ((Obstacle)mapa.mapa[Y, X + 1]).tipo == ObstacleType.Uranium)
        {
            isAdjacentUranium = true;
        }

        if (X >= 0 && Y >= 0 && mapa.mapa[Y, X] is Obstacle && ((Obstacle)mapa.mapa[Y, X]).tipo == ObstacleType.Uranium)
        {
            isSamePositionUranium = true;
        }

        if (isAdjacentUranium)
        {
            LoseEnergy(10);
        }

        if (isSamePositionUranium)
        {
            RemoveUraniumObstacle(X, Y);
            LoseEnergy(30);
        }
    }

    /// <summary>
    /// Registra o robô para receber eventos de teclas pressionadas.
    /// </summary>
    public void SubscribeToKeystrokes()
    {
        JewelCollector.KeystrokeEvent += HandleKeystroke;
    }

    /// <summary>
    /// Manipula a tecla pressionada pelo usuário.
    /// Movimenta o robô na direção especificada pela tecla.
    /// Coleta joias se estiverem nas posições adjacentes ao robô.
    /// Ganha energia se a joia coletada for do tipo Blue.
    /// </summary>
    /// <param name="key">Tecla pressionada.</param>
    private void HandleKeystroke(ConsoleKey key)
    {
        bool jewelCollected = false;

        switch (key)
        {
            case ConsoleKey.W:
                {
                    if (Y - 1 >= 0 && (mapa.mapa[Y - 1, X] is not (Obstacle or Jewel) | (mapa.mapa[Y - 1, X] is Obstacle && ((Obstacle)mapa.mapa[Y - 1, X]).tipo == ObstacleType.Uranium)))
                    {
                        Y -= 1;
                        ConsumeEnergy();
                    }
                }
                break;
            case ConsoleKey.A:
                {
                    if (X - 1 >= 0 && mapa.mapa[Y, X - 1] is not (Obstacle or Jewel) | (mapa.mapa[Y, X - 1] is Obstacle && ((Obstacle)mapa.mapa[Y, X - 1]).tipo == ObstacleType.Uranium))
                    {
                        X -= 1;
                        ConsumeEnergy();
                    }
                }
                break;
            case ConsoleKey.S:
                {
                    if (Y + 1 < mapa.rowLength && mapa.mapa[Y + 1, X] is not (Obstacle or Jewel) | (mapa.mapa[Y + 1, X] is Obstacle && ((Obstacle)mapa.mapa[Y + 1, X]).tipo == ObstacleType.Uranium))
                    {
                        Y += 1;
                        ConsumeEnergy();
                    }
                }
                break;
            case ConsoleKey.D:
                {
                    if (X + 1 < mapa.colLength && mapa.mapa[Y, X + 1] is not (Obstacle or Jewel) | (mapa.mapa[Y, X + 1] is Obstacle && ((Obstacle)mapa.mapa[Y, X + 1]).tipo == ObstacleType.Uranium))
                    {
                        X += 1;
                        ConsumeEnergy();
                    }
                }
                break;
            case ConsoleKey.G:
                {
                    // Verificar se há joias nas posições adjacentes ao robô
                    if (Y - 1 >= 0 && mapa.mapa[Y - 1, X] is Jewel)
                    {
                        var jewel = (Jewel)mapa.mapa[Y - 1, X];
                        sacola.Add((int)jewel.type);
                        mapa.RemoveElement(X, Y - 1);
                        jewelCollected = true;
                        Jewel.JewelQuantity--;

                        if (jewel.type == JewelType.Blue)
                        {
                            GainEnergy(5);
                        }
                    }
                    else if (X - 1 >= 0 && mapa.mapa[Y, X - 1] is Jewel)
                    {
                        var jewel = (Jewel)mapa.mapa[Y, X - 1];
                        sacola.Add((int)jewel.type);
                        mapa.RemoveElement(X - 1, Y);
                        jewelCollected = true;
                        Jewel.JewelQuantity--;

                        if (jewel.type == JewelType.Blue)
                        {
                            GainEnergy(5);
                        }
                    }
                    else if (Y + 1 < mapa.rowLength && mapa.mapa[Y + 1, X] is Jewel)
                    {
                        var jewel = (Jewel)mapa.mapa[Y + 1, X];
                        sacola.Add((int)jewel.type);
                        mapa.RemoveElement(X, Y + 1);
                        jewelCollected = true;
                        Jewel.JewelQuantity--;

                        if (jewel.type == JewelType.Blue)
                        {
                            GainEnergy(5);
                        }
                    }
                    else if (X + 1 < mapa.colLength && mapa.mapa[Y, X + 1] is Jewel)
                    {
                        var jewel = (Jewel)mapa.mapa[Y, X + 1];
                        sacola.Add((int)jewel.type);
                        mapa.RemoveElement(X + 1, Y);
                        jewelCollected = true;
                        Jewel.JewelQuantity--;

                        if (jewel.type == JewelType.Blue)
                        {
                            GainEnergy(5);
                        }
                    }

                    if (!jewelCollected)
                    {
                        // Verificar se há uma árvore nas posições adjacentes ao robô
                        if (Y - 1 >= 0 && mapa.mapa[Y - 1, X] is Obstacle && ((Obstacle)mapa.mapa[Y - 1, X]).tipo == ObstacleType.Tree)
                        {
                            GainEnergy(3);
                        }
                        else if (X - 1 >= 0 && mapa.mapa[Y, X - 1] is Obstacle && ((Obstacle)mapa.mapa[Y, X - 1]).tipo == ObstacleType.Tree)
                        {
                            GainEnergy(3);
                        }
                        else if (Y + 1 < mapa.rowLength && mapa.mapa[Y + 1, X] is Obstacle && ((Obstacle)mapa.mapa[Y + 1, X]).tipo == ObstacleType.Tree)
                        {
                            GainEnergy(3);
                        }
                        else if (X + 1 < mapa.colLength && mapa.mapa[Y, X + 1] is Obstacle && ((Obstacle)mapa.mapa[Y, X + 1]).tipo == ObstacleType.Tree)
                        {
                            GainEnergy(3);
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Remove o obstáculo de urânio na posição especificada.
    /// </summary>
    /// <param name="x">Coordenada X do obstáculo.</param>
    /// <param name="y">Coordenada Y do obstáculo.</param>
    private void RemoveUraniumObstacle(int x, int y)
    {
        mapa.RemoveElement(x, y);
    }

    /// <summary>
    /// Perde uma quantidade específica de energia.
    /// </summary>
    /// <param name="amount">Quantidade de energia a ser perdida.</param>
    private void LoseEnergy(int amount)
    {
        Energy -= amount;
        Console.WriteLine("Energia perdida: {0}", amount);
        CurrentEnergy();
    }

    /// <summary>
    /// Reseta a posição do robô para (0, 0).
    /// </summary>
    public void ResetPosition()
    {
        X = 0;
        Y = 0;
    }

    /// <summary>
    /// Imprime a energia atual do robô.
    /// </summary>
    public void CurrentEnergy()
    {
        Console.WriteLine("Energia atual: {0}", Energy);
    }

    /// <summary>
    /// Consume energia ao se mover.
    /// </summary>
    private void ConsumeEnergy()
    {
        Energy--;
        CurrentEnergy();
    }

    /// <summary>
    /// Ganha uma quantidade específica de energia.
    /// </summary>
    /// <param name="amount">Quantidade de energia a ser ganha.</param>
    private void GainEnergy(int amount)
    {
        Energy += amount;
        Console.WriteLine("Energia ganha: {0}", amount);
        CurrentEnergy();
    }
    /// <summary>
    /// Reseta a energia do robô para o valor inicial.
    /// </summary>
    public void ResetEnergy()
    {
        Energy = 5;
    }
        /// <summary>
    /// Retorna a quantidade de joias coletadas pelo robô.
    /// </summary>
    public void JewelQuantity()
    {
        Console.WriteLine("Quantidade de joias: {0}", sacola.Count);
    }

    /// <summary>
    /// Retorna o valor total das joias coletadas pelo robô.
    /// </summary>
    public void JewelTotalValue()
    {
        Console.WriteLine("Valor total das joias: {0}", sacola.Sum());
    }


}
