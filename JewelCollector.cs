using System;
using System.Collections.Generic;

/// <summary>
/// Classe responsável por coletar joias em um mapa.
/// </summary>
public class JewelCollector
{
    /// <summary>
    /// Delegado para tratar eventos de teclas pressionadas.
    /// </summary>
    /// <param name="key">Tecla pressionada.</param>
    public delegate void KeystrokeEventHandler(ConsoleKey key);

    /// <summary>
    /// Evento acionado quando uma tecla é pressionada.
    /// </summary>
    public static event KeystrokeEventHandler? KeystrokeEvent;

    /// <summary>
    /// Método de entrada do programa.
    /// </summary>
    public static void Main()
    {
        StartGame();
    }

    /// <summary>
    /// Inicia o jogo.
    /// </summary>
    private static void StartGame()
    {
        Map stage = new Map(10);
        Robot player = new Robot(0, 0, stage);
        player.SubscribeToKeystrokes();
        bool running = true;

        Random random = new Random();
        int maxElements = 4; // Quantidade máxima de elementos por estágio

        do
        {
            stage.Draw(player);
            player.VerifyUranium();
            player.JewelQuantity();
            player.JewelTotalValue();
            player.CurrentEnergy();
            Console.WriteLine("Enter the command: ");
            ConsoleKeyInfo command = Console.ReadKey();
            KeystrokeEvent?.Invoke(command.Key);
            if (command.Key == ConsoleKey.Q)
            {
                running = false;
            }
            Console.WriteLine("player position: {0},{1}", player.X, player.Y);
            if (player.Energy <= 0)
            {
                Console.WriteLine("A energia do robô acabou! O jogo termina.");
                Console.WriteLine("Estágio: {0}", maxElements - 4);
                running = false;
            }
            if (Jewel.JewelQuantity == 0)
            {
                Console.WriteLine("Parabéns! Você coletou todas as joias! Reiniciando o jogo...");
                Console.WriteLine("Estágio: {0}", maxElements - 3);
                stage.ClearElements();
                player.ResetPosition();
                player.ResetEnergy();
                if (stage.rowLength < 30 && stage.colLength < 30)
                {
                    stage.Resize(stage.rowLength + 1, stage.colLength + 1);
                    maxElements++; // Aumenta a quantidade máxima de elementos por estágio
                    stage.AddElement("$$", 1, 0);
                    AddRandomElements(stage, random, "##", maxElements);
                    AddRandomElements(stage, random, "$$", maxElements);
                    AddRandomElements(stage, random, "JR", maxElements);
                    AddRandomElements(stage, random, "JB", maxElements);
                    AddRandomElements(stage, random, "JG", maxElements);
                    if (maxElements - 3 >= 3)
                    {
                        AddRandomElements(stage, random, "!!", maxElements - 3);
                    }
                }
            }
        } while (running);
    }

    /// <summary>
    /// Adiciona elementos aleatórios ao mapa.
    /// </summary>
    /// <param name="stage">Mapa.</param>
    /// <param name="random">Objeto Random para geração de números aleatórios.</param>
    /// <param name="element">Elemento a ser adicionado.</param>
    /// <param name="quantity">Quantidade de elementos a serem adicionados.</param>
    private static void AddRandomElements(Map stage, Random random, string element, int quantity)
    {
        List<(int, int)> emptyCells = new List<(int, int)>();
        List<(int, int)> playerAdjacentCells = new List<(int, int)>();

        for (int i = 0; i < stage.rowLength; i++)
        {
            for (int j = 0; j < stage.colLength; j++)
            {
                if (stage.mapa[i, j] is Empty)
                {
                    emptyCells.Add((j, i));
                }
                else if (stage.mapa[i, j] is Robot)
                {
                    // Adiciona as células adjacentes ao jogador
                    playerAdjacentCells.Add((j - 1, i));
                    playerAdjacentCells.Add((j + 1, i));
                    playerAdjacentCells.Add((j, i - 1));
                    playerAdjacentCells.Add((j, i + 1));
                }
            }
        }

        if (emptyCells.Count < quantity)
        {
            throw new ArgumentException("Not enough empty cells in the map to add the requested elements.");
        }

        // Remove células adjacentes ao jogador da lista de células vazias
        emptyCells.RemoveAll(cell => playerAdjacentCells.Contains(cell));

        // Remove células bloqueadas por obstáculos da lista de células vazias
        emptyCells.RemoveAll(cell =>
        {
            int x = cell.Item1;
            int y = cell.Item2;
            return !IsEmptyCell(stage, x - 1, y) || !IsEmptyCell(stage, x + 1, y) || !IsEmptyCell(stage, x, y - 1) || !IsEmptyCell(stage, x, y + 1);
        });

        // Se ainda há células vazias disponíveis após a remoção das células adjacentes ao jogador e dos obstáculos
        if (emptyCells.Count > 0)
        {
            for (int i = 0; i < quantity; i++)
            {
                int index = random.Next(emptyCells.Count);
                (int x, int y) = emptyCells[index];
                stage.AddElement(element, x, y);
                emptyCells.RemoveAt(index);
            }
        }
        else
        {
            throw new ArgumentException("No available empty cells to add elements near the player.");
        }
    }

    /// <summary>
    /// Verifica se uma célula do mapa está vazia.
    /// </summary>
    /// <param name="stage">Mapa.</param>
    /// <param name="x">Coordenada X da célula.</param>
    /// <param name="y">Coordenada Y da célula.</param>
    /// <returns>True se a célula estiver vazia, False caso contrário.</returns>
    private static bool IsEmptyCell(Map stage, int x, int y)
    {
        return x >= 0 && x < stage.colLength && y >= 0 && y < stage.rowLength && stage.mapa[y, x] is Empty;
    }
}
