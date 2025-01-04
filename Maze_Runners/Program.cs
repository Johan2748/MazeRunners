using System;

namespace Maze_Runners
{
    class Program
    {
        static void Main(string[] args)
        {
            // Establece el tamaño de la consola
            Console.BufferHeight = 40;
            Console.BufferWidth = 120;

            // Abre el Menu de Inicio del juego
            GameManager.Start();

        }
    }
}
