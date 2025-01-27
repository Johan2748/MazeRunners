using System;
using Spectre.Console;

namespace Maze_Runners
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            // Establece el tamaño de la consola
            Console.BufferHeight = 50;
            Console.BufferWidth = 180;

            // Imprime la Historia del juego cuando se abre la aplicacion
            //GameManager.SetGameStory();

            // Abre el Menu de Inicio del juego
            GameManager.Start();
            
            
            

        

        }


    }
}
