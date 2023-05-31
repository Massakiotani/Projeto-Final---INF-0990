using System;
using System.Collections.Generic;

/// <summary>
/// Representa um mapa do jogo.
/// </summary>
public class Map
{
    /// <summary>
    /// O mapa representado como uma matriz de células.
    /// </summary>
    public Cell[,] mapa;

    /// <summary>
    /// O comprimento da matriz nas linhas.
    /// </summary>
    public int rowLength;

    /// <summary>
    /// O comprimento da matriz nas colunas.
    /// </summary>
    public int colLength;

    /// <summary>
    /// Cria um novo mapa com a altura e largura especificadas.
    /// </summary>
    /// <param name="altura">A altura do mapa.</param>
    /// <param name="largura">A largura do mapa. Se não for especificada, será igual à altura.</param>
    public Map(int altura, int largura = 0)
    {
        if (largura == 0)
        {
            rowLength = altura;
            colLength = altura;
        }
        else
        {
            rowLength = altura;
            colLength = largura;
        }

        mapa = new Cell[rowLength, colLength];

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                mapa[i, j] = new Empty(j, i);
            }
        }
    }

    /// <summary>
    /// Adiciona um elemento ao mapa na posição especificada.
    /// </summary>
    /// <param name="element">O elemento a ser adicionado.</param>
    /// <param name="x">A coordenada X da posição.</param>
    /// <param name="y">A coordenada Y da posição.</param>
    public void AddElement(string element, int x, int y)
    {
        switch (element)
        {
            case "JR":
                mapa[y, x] = new Jewel(JewelType.Red, x, y);
                break;
            case "JB":
                mapa[y, x] = new Jewel(JewelType.Blue, x, y);
                break;
            case "JG":
                mapa[y, x] = new Jewel(JewelType.Green, x, y);
                break;
            case "$$":
                mapa[y, x] = new Obstacle(ObstacleType.Tree, x, y);
                break;
            case "##":
                mapa[y, x] = new Obstacle(ObstacleType.Water, x, y);
                break;
            case "!!":
                mapa[y, x] = new Obstacle(ObstacleType.Uranium, x, y);
                break;
        }
    }

    /// <summary>
    /// Remove o elemento do mapa na posição especificada.
    /// </summary>
    /// <param name="x">A coordenada X da posição.</param>
    /// <param name="y">A coordenada Y da posição.</param>
    public void RemoveElement(int x, int y)
    {
        mapa[y, x] = new Empty(x, y);
    }

    /// <summary>
    /// Remove todos os elementos do mapa, preenchendo-o com células vazias.
    /// </summary>
    public void ClearElements()
    {
        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                mapa[i, j] = new Empty(j, i);
            }
        }
    }

    /// <summary>
    /// Desenha o mapa no console, exibindo os elementos e a posição do jogador.
    /// </summary>
    /// <param name="player">O jogador.</param>
    public void Draw(Robot player)
    {
        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                switch (mapa[i, j].ToString())
                {
                    case "--":
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case "JB":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case "JG":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case "JR":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "$$":
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case "##":
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                }

                if ((j, i) == (player.X, player.Y))
                {
                    Console.Write(string.Format("{0} ", player.ToString()));
                }
                else
                {
                    Console.Write(string.Format("{0} ", mapa[i, j].ToString()));
                }
            }
            Console.Write("\n");
        }
    }

    /// <summary>
    /// Redimensiona o mapa para a nova altura e largura especificadas.
    /// </summary>
    /// <param name="newHeight">A nova altura do mapa.</param>
    /// <param name="newWidth">A nova largura do mapa.</param>
    public void Resize(int newHeight, int newWidth)
    {
        if (newHeight <= 30 && newWidth <= 30)
        {
            Cell[,] newMap = new Cell[newHeight, newWidth];

            for (int i = 0; i < newHeight; i++)
            {
                for (int j = 0; j < newWidth; j++)
                {
                    if (i < rowLength && j < colLength)
                    {
                        newMap[i, j] = mapa[i, j];
                    }
                    else
                    {
                        newMap[i, j] = new Empty(j, i);
                    }
                }
            }

            mapa = newMap;
            rowLength = newHeight;
            colLength = newWidth;
        }
    }
}
