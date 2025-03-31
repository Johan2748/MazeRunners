using System;
using Spectre.Console;

namespace Maze_Runners
{
    class Program
    {
        static void Main(string[] args)
        {
            // Establece el tamaño de la consola
            Console.BufferHeight = Console.LargestWindowHeight;
            Console.BufferWidth = Console.LargestWindowWidth;

            // Imprime la Historia del juego cuando se abre la aplicacion
            GameManager.SetGameStory();

            // Abre el Menu de Inicio del juego
            GameManager.Start();
            
        }

    }
}
