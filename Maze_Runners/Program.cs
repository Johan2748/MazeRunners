using System;
using Spectre.Console;

namespace Maze_Runners
{
    class Program
    {
        static void Main(string[] args)
        {
            // Establece el tamaño de la consola
            Console.BufferHeight = 44;
            Console.BufferWidth = 180;


            // Abre el Menu de Inicio del juego
            //GameManager.Start();

            

            Maze maze = new Maze(10);
            maze.GenerateMaze();

            Console.ReadKey(true);

        }
    }
}
