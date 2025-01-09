using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace Maze_Runners
{
    class GameManager
    {
        private static List<Player> PlayersList = new List<Player>();

        private Maze maze;


        public static void Start()
        {
            StartMainMenu();
            Maze m = new Maze(21);
        }

        private static void StartMainMenu()
        {
            Console.Clear();

            // Imprime en pantalla el logo del juego
            Menu.PrintLogo();

            // Crea el menu e imprime sus opciones
            Menu mainMenu = new Menu("Opciones", new string[] { "Iniciar Partida", "Como jugar", "Creditos", "Salir" });
            mainMenu.SetMenu();

            // Abre seccion de creditos
            if (mainMenu.SelectedOption == "Creditos")
            {
                AnsiConsole.MarkupLine("[blue]  >>[/]  [underline]This game was crated by[/] [underline][green3_1]Johan Daniel :heart_suit:[/][/]" + "\n\n");

                AnsiConsole.Markup("[grey37]    (Press any key to return to the Main Menu)...   [/]");
                Console.ReadKey(true);

                StartMainMenu();
            }

            // Explicacion de la logica del juego
            if(mainMenu.SelectedOption=="Como jugar")
            {

            }

            // Sale del juego
            if (mainMenu.SelectedOption == "Salir")
            {
                AnsiConsole.Write(new FigletText("HASTA LA PROXIMA").Color(Color.DeepSkyBlue1));
                Console.ReadKey(true);
                Console.Beep();
                Environment.Exit(0);
            }

            // Inicia Menu de jugadores
            if(mainMenu.SelectedOption== "Iniciar Partida")
            {
                StartPlayerSelectionMenu();
            }

        } // PENDIENTE LA INFORMACION DEL JUEGO

        private static void StartPlayerSelectionMenu()
        {
            // Crea las opciones de cuantos jugadores van a jugar
            Menu playerSelectionMenu = new Menu("Elija la cantidad de jugadores", new string[] { "2 jugadores", "3 jugadores", "4 jugadores" });
            playerSelectionMenu.SetMenu();

            if (playerSelectionMenu.SelectedOption == "2 jugadores") 
            {
                Player player1 = GeneratePlayer(1);
                PlayersList.Add(player1);
                Player player2 = GeneratePlayer(2);
                PlayersList.Add(player2);
            }

            if (playerSelectionMenu.SelectedOption == "3 jugadores") 
            {
                Player player1 = GeneratePlayer(1);
                PlayersList.Add(player1);
                Player player2 = GeneratePlayer(2);
                PlayersList.Add(player2);
                Player player3 = GeneratePlayer(3);
                PlayersList.Add(player3);
            }

            if (playerSelectionMenu.SelectedOption == "4 jugadores") 
            {
                Player player1 = GeneratePlayer(1);
                PlayersList.Add(player1);
                Player player2 = GeneratePlayer(2);
                PlayersList.Add(player2);
                Player player3 = GeneratePlayer(3);
                PlayersList.Add(player3);
                Player player4 = GeneratePlayer(4);
                PlayersList.Add(player4);
            }

            AnsiConsole.Write(new FigletText("VAMOS A JUGAR!!!").Color(Color.Red));

            Console.ReadKey(true);
            Console.Clear();
        }  

        private static Player GeneratePlayer(int n)
        {
            // Colores de los equipos
            Color[] colors = new Color[] { Color.Red, Color.Blue, Color.Lime, Color.White };

            // Color del equipo actual
            Color usercolor = colors[n - 1];
            string usercolor_markup = usercolor.ToMarkup();
            string color_team;

            // Definir el color segun el equipo
            if (n == 1) color_team = "ROJO";
            else if (n == 2) color_team = "AZUL";
            else if (n == 3) color_team = "VERDE";
            else color_team = "BLANCO";

            // Imprime el color del equipo
            AnsiConsole.Markup($"[{usercolor_markup}]EQUIPO {color_team}[/]\n\n\n");

            // Pregunta al usuario por el nombre
            string name = AnsiConsole.Ask<string>($"[{usercolor_markup}]Nombre[/]", $"Jugador {n}");
            Console.WriteLine();

            // Define con q tipo de ficha va a jugar
            string pieceType;

            pieceType = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"[green] Selecciona tu heroe [/]")
                    .AddChoices(new string[] { "Corredor", "Saltador", "Patinador", "Rompe Muros", "Mago" }
                    )) ;

            // Determinacion automatica de los atributos de la ficha
            int speed;
            int cooldown;
            Power power;
            string id;

            if (pieceType == "Corredor")
            {
                speed = 6;
                cooldown = 3;
                power = Power.Run;
                id = n + "R";
            }
            else if (pieceType == "Saltador")
            {
                speed = 4;
                cooldown = 4;
                power = Power.Jump;
                id = n + "J";
            }
            else if (pieceType == "Patinador")
            {
                speed = 5;
                cooldown = 3;
                power = Power.Skate;
                id = n + "S";
            }
            else if (pieceType == "Rompe Muros")
            {
                speed = 5;
                cooldown = 4;
                power = Power.Break;
                id = n + "B";
            }
            else
            {
                speed = 3;
                cooldown = 2;
                power = Power.Teleport;
                id = n + "T";
            }

            // Crea la ficha del jugador
            Piece piece = new Piece(speed, cooldown, power, usercolor, id);
            piece.Box.PrintBox();
            AnsiConsole.Markup($"[{usercolor_markup}] {pieceType}[/]\n\n\n");

            //Crea la instancia del jugador
            Player player = new Player(name, piece);

            Console.ReadKey(true);
            return player;
        }













































































    }
}
