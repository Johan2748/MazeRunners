using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace Maze_Runners
{
    class GameManager
    {
        private int players;




        public static void Start()
        {
            StartMainMenu();
        }

        public static void StartMainMenu()
        {
            Console.Clear();

            // Imprime en pantalla el logo del juego
            Menu.PrintLogo();

            // Crea el menu e imprime sus opciones
            Menu mainMenu = new Menu("Opciones", new string[] { "Iniciar Partida", "Creditos", "Salir" });
            mainMenu.SetMenu();

            // Abre seccion de creditos
            if (mainMenu.SelectedOption == "Creditos")
            {
                AnsiConsole.MarkupLine("[blue]  >>[/]  [underline]This game was crated by[/] [underline][green3_1]Johan Daniel :heart_suit:[/][/]" + "\n\n");

                AnsiConsole.Markup("[grey37]    (Press any key to return to the Main Menu)...   [/]");
                Console.ReadKey(true);

                StartMainMenu();
            }

            // Sale del juego
            if (mainMenu.SelectedOption == "Salir")
            {
                AnsiConsole.Write(new FigletText("Hasta la proxima").Color(Color.DeepSkyBlue1));
                Console.ReadKey(true);
                Console.Beep();
            }

            // Inicia Menu de jugadores
            if(mainMenu.SelectedOption== "Iniciar Partida")
            {
                StartPlayerSelectionMenu();
            }

        }

        public static void StartPlayerSelectionMenu()
        {
            // Crea las opciones de cuantos jugadores van a jugar
            Menu playerSelectionMenu = new Menu("Elija la cantidad de jugadores", new string[] { "2 jugadores", "3 jugadores", "4 jugadores" });
            playerSelectionMenu.SetMenu();




        }
















































































    }
}
