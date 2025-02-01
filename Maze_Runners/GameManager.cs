using System;
using System.Collections.Generic;
using Spectre.Console;
using System.Threading;

namespace Maze_Runners
{
    static class GameManager
    {
        public static List<Player> PlayersList { get; private set; }

        private static Maze maze;

        // Imprime en la pantalla la historia del juego
        public static void SetGameStory()
        {
            string text = "In the kingdom of Eldoria, a vast territory known for its green meadows and majestic castles, " +
                "King Alaric reigned. He was a just and wise monarch, loved by his people. \nHowever, there was a shadow " +
                "that hung over his reign: the inability to have offspring. As time passed, concern grew among the nobles " +
                "and citizens, as they knew that \nwithout an heir, the future of the kingdom was in danger.";

            string text1 = "Desperate to ensure the continuity of his lineage and the stability of Eldoria, King Alaric " +
                "decided to organize an unusual tournament. He summoned brave men from across\nthe kingdom to participate " +
                "in a challenge that would test not only their strength, but also their ingenuity and determination. The " +
                "tournament would take place in a \nmagical labyrinth, built on the castle grounds.";

            string text2 = "The labyrinth was full of traps and illusions. It was said that only those with a pure heart " +
                "and a cunning mind could find the way out. The first participant to reach \nthe center of the labyrinth would " +
                "receive the decoration of prince or princess and would become the successor to the throne.";

            List<string> list = new List<string>() { text, text1, text2 };

            AnsiConsole.Status()
            .Spinner(Spinner.Known.Line)
            .Start("Loading...", ctx => {
                Thread.Sleep(3000);
            });
            
            foreach (string s in list)
            {
                foreach (char c in s)
                {
                    AnsiConsole.Markup($"[darkmagenta_1]{c}[/]");
                    Thread.Sleep(1);
                }
                Console.WriteLine("\n");
            }

            AnsiConsole.Write(new FigletText("WELCOME").Color(Color.DarkMagenta_1));
            Thread.Sleep(20);
            AnsiConsole.Write(new FigletText("MAZE").Color(Color.DarkMagenta_1).Centered());
            Thread.Sleep(20);
            AnsiConsole.Write(new FigletText("RUNNERS").Color(Color.DarkMagenta_1).RightJustified());

            Console.ReadKey(true);            
        }

        // Determina el ritmo de toda la aplicacion
        public static void Start()
        {
            StartMainMenu();
            GenerateMaze();
            maze.SetTrapsAndPlayers(PlayersList);
            KeepPlaying();
            Start();
        }

        // Menu de Inicio
        private static void StartMainMenu()
        {
            Console.Clear();

            // Imprime en pantalla el logo del juego
            Menu.PrintLogo();

            // Crea el menu e imprime sus opciones
            Menu mainMenu = new Menu("Opciones", new string[] { "Start", "How to play", "Credits", "Exit" });
            mainMenu.SetMenu();

            // Abre seccion de creditos
            if (mainMenu.SelectedOption == "Credits")
            {
                AnsiConsole.MarkupLine("[blue]  >>[/]  [underline]This game was crated by[/] [underline][green3_1]Johan Daniel :heart_suit:[/][/]" + "\n\n");

                AnsiConsole.Markup("[grey37]    (Press any key to return to the Main Menu)...   [/]");
                Console.ReadKey(true);

                StartMainMenu();
            }

            // Explicacion de la logica del juego
            if(mainMenu.SelectedOption=="How to play")
            {
                HowToPlayInfo();
            }

            // Sale del juego
            if (mainMenu.SelectedOption == "Exit")
            {
                AnsiConsole.Write(new FigletText("SEE YOU SOON").Color(Color.DeepSkyBlue1));
                Console.ReadKey(true);
                Console.Beep();
                Environment.Exit(0);
            }

            // Inicia Menu de players
            if(mainMenu.SelectedOption== "Start")
            {
                StartPlayerSelectionMenu();
            }

        }

        // Escribe el texto con la informacion de como jugar
        private static void HowToPlayInfo()
        {
            string textColor = "mediumorchid3";
            AnsiConsole.Markup($"[{textColor}]" +
                "In this interesting multiplayer adventure your objective is to fight to reach the center of the maze first.\n\n" +
                "[/]");

            WinnerBox winnerBox = new WinnerBox();
            winnerBox.PrintBox();

            AnsiConsole.Markup($"[{winnerBox.color.ToMarkup()}] Goal[/]\n\n");

            AnsiConsole.Markup($"[{textColor}]" +
                "To do this you will have to choose a hero who will lead you to victory.\n" +
                "Each hero has a special skill that will help him reach the goal:\n\n\n " +
                "[/]");

            PlayerBox runner = new PlayerBox("#R", Color.Red);
            PlayerBox jumper = new PlayerBox("#J", Color.Blue);
            PlayerBox skater = new PlayerBox("#S", Color.Lime);
            PlayerBox wallBraker = new PlayerBox("#B", Color.Blue);
            PlayerBox wizard = new PlayerBox("#W", Color.Red);

            runner.PrintBox();
            AnsiConsole.Markup($"[{runner.color.ToMarkup()}] Runner[/]" +
                $"[{textColor}]   Speed:[/] 6 steps\n" +
                $"[{textColor}]             Cooldown:[/] 3 turns\n" +
                $"[{textColor}]             Skill:[/] You con walk 10 extra steps\n\n ");

            jumper.PrintBox();
            AnsiConsole.Markup($"[{jumper.color.ToMarkup()}] Jumper[/]" +
                $"[{textColor}]   Speed:[/] 4 steps\n" +
                $"[{textColor}]             Cooldown:[/] 4 turns\n" +
                $"[{textColor}]             Skill:[/] You can jump over an intermediate square\n\n ");

            skater.PrintBox();
            AnsiConsole.Markup($"[{skater.color.ToMarkup()}] Skater[/]" +
                $"[{textColor}]   Speed:[/] 5 steps\n" +
                $"[{textColor}]             Cooldown:[/] 3 turns\n" +
                $"[{textColor}]             Skill:[/] You can skate in one direction until you find an obstacle\n\n ");

            wallBraker.PrintBox();
            AnsiConsole.Markup($"[{wallBraker.color.ToMarkup()}] Wall Braker[/]" +
                $"[{textColor}]   Speed:[/] 5 steps\n" +
                $"[{textColor}]                  Cooldown:[/] 4 turns\n" +
                $"[{textColor}]                  Skill:[/] You can break a wall\n\n ");

            wizard.PrintBox();
            AnsiConsole.Markup($"[{wizard.color.ToMarkup()}] Wizard[/]" +
                $"[{textColor}]   Speed:[/] 3 steps\n" +
                $"[{textColor}]             Cooldown:[/] 2 turns\n" +
                $"[{textColor}]             Skill:[/] Teleport to a random location in the maze\n\n");


            AnsiConsole.Markup($"[{textColor}]The labyrinth has several traps that will make your passage more difficult.\n\n [/]");

            Trap ice = new Trap(Trap.TrapType.Ice);
            Trap portal = new Trap(Trap.TrapType.Portal);
            Trap shifter = new Trap(Trap.TrapType.ShapeShifterPoison);
            Trap speed = new Trap(Trap.TrapType.SpeedPotion);

            ice.PrintBox();
            AnsiConsole.Markup($"[{textColor}] Ice[/]" +
                $"   End your turn\n\n ");

            portal.PrintBox();
            AnsiConsole.Markup($"[{textColor}] Portal[/]" +
                $"   Teleports you to another portal\n\n ");

            shifter.PrintBox();
            AnsiConsole.Markup($"[{textColor}] Shape Shifter Poison[/]" +
                $"   Change your skill to a random one\n\n ");

            speed.PrintBox();
            AnsiConsole.Markup($"[{textColor}] Speed Poison[/]" +
                $"   Gives you 5 extra steps");


            Console.WriteLine("\n\n\n");
            AnsiConsole.Markup("[grey37]    (Press any key to go next)...   [/]");
            Console.ReadKey(true);
            Console.Clear();

            AnsiConsole.Markup($"[{textColor}]Controls:[/]\n\n");

            AnsiConsole.Markup($"[{textColor}]Move up:[/] W / UpArrow\n\n");
            AnsiConsole.Markup($"[{textColor}]Move left:[/] A / LeftArrow\n\n");
            AnsiConsole.Markup($"[{textColor}]Move down:[/] S / DownArrow\n\n");
            AnsiConsole.Markup($"[{textColor}]Move right:[/] D / RightArrow\n\n");
            AnsiConsole.Markup($"[{textColor}]Activate power:[/] P\n\n");
            AnsiConsole.Markup($"[{textColor}]Swicht player:[/] Enter\n\n");
            AnsiConsole.Markup($"[{textColor}]Menu Pause:[/] Esc\n\n");

            Console.WriteLine("\n\n\n");
            AnsiConsole.Markup("[grey37]    (Press any key to return to the Main Menu)...   [/]");

            Console.ReadKey(true);
            StartMainMenu();
        }

        // Menu de selecion de jugadores
        private static void StartPlayerSelectionMenu()
        {
            // Crea las opciones de cuantos jugadores van a jugar
            Menu playerSelectionMenu = new Menu("Choose the number of players", new string[] { "2 players", "3 players", "4 players" });
            playerSelectionMenu.SetMenu();

            PlayersList = new List<Player>();

            if (playerSelectionMenu.SelectedOption == "2 players") 
            {
                Player player1 = GeneratePlayer(1);
                PlayersList.Add(player1);
                Player player2 = GeneratePlayer(2);
                PlayersList.Add(player2);
            }

            if (playerSelectionMenu.SelectedOption == "3 players") 
            {
                Player player1 = GeneratePlayer(1);
                PlayersList.Add(player1);
                Player player2 = GeneratePlayer(2);
                PlayersList.Add(player2);
                Player player3 = GeneratePlayer(3);
                PlayersList.Add(player3);
            }

            if (playerSelectionMenu.SelectedOption == "4 players") 
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

            AnsiConsole.Write(new FigletText("LET'S PLAY!!!").Color(Color.Red));

            Console.ReadKey(true);
            Console.Clear();
        }  

        // Menu de generacion de jugador y ficha del jugador
        private static Player GeneratePlayer(int n)
        {
            // Colores de los equipos
            Color[] colors = new Color[] { Color.Red, Color.Blue, Color.Lime, Color.White };

            // Color del equipo actual
            Color usercolor = colors[n - 1];
            string usercolor_markup = usercolor.ToMarkup();
            string color_team;

            // Definir el color segun el equipo
            if (n == 1) color_team = "RED";
            else if (n == 2) color_team = "BLUE";
            else if (n == 3) color_team = "GREEN";
            else color_team = "WHITE";

            // Imprime el color del equipo
            AnsiConsole.Markup($"[{usercolor_markup}]{color_team} TEAM[/]\n\n\n");

            // Pregunta al usuario por el nombre
            string name = AnsiConsole.Prompt(new TextPrompt<string>($"[{usercolor_markup}]Username[/]")
                .DefaultValue($"Player {n}")
                .Validate(username =>
                {
                    if (username.Length > 20)
                    {
                        return ValidationResult.Error("[red] Too long!!![/]");
                    }
                    else return ValidationResult.Success();
                }
                ));
            Console.WriteLine();

            // Define con q tipo de ficha va a jugar
            string pieceType;

            pieceType = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"[green] Choose your hero[/]")
                    .AddChoices(new string[] { "Runner", "Jumper", "Skater", "Wall Braker", "Wizard" }
                    )) ;

            // Determinacion automatica de los atributos de la ficha
            int speed;
            int cooldown;
            Power power;
            string id;

            if (pieceType == "Runner")
            {
                speed = 6;
                cooldown = 3;
                power = Power.Run;
                id = n + "R";
            }
            else if (pieceType == "Jumper")
            {
                speed = 4;
                cooldown = 4;
                power = Power.Jump;
                id = n + "J";
            }
            else if (pieceType == "Skater")
            {
                speed = 5;
                cooldown = 3;
                power = Power.Skate;
                id = n + "S";
            }
            else if (pieceType == "Wall Braker")
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
                id = n + "W";
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

        // Proporciona un laberinto en correlacion a la ctdad de jugadores
        private static void GenerateMaze()
        {
            if (PlayersList.Count == 2) maze = new Maze(31);
            if (PlayersList.Count == 3) maze = new Maze(35);
            if (PlayersList.Count == 4) maze = new Maze(39);
        }

        // Mantiene el juego mientras no se cumpla la condicion de victoria
        private static void KeepPlaying()
        {
            while (maze.maze[maze.scale / 2, maze.scale / 2].GetType() == typeof(WinnerBox))
            {
                foreach (Player player in PlayersList)
                {
                    ChangePlayer(player);
                    PrintInfo(PlayersList);
                    player.piece.Move(maze);

                    if (maze.maze[maze.scale / 2, maze.scale / 2].GetType() != typeof(WinnerBox))
                    {
                        SetWinner(player);
                        break;
                    }
                }
            }
        }

        // Menu de pausa
        public static void SetPauseMenu()
        {
            Console.Clear();

            Menu pauseMenu = new Menu("Do you want to leave the Game", new string[] { "Back to the Game", "Go to Main Menu" });
            pauseMenu.SetMenu();

            if(pauseMenu.SelectedOption== "Back to the Game")
            {
                maze.PrintMaze();
                PrintInfo(PlayersList);
            }
            if(pauseMenu.SelectedOption== "Go to Main Menu")
            {
                maze.maze[maze.scale / 2, maze.scale / 2] = new EmptyBox();
                Start();
            }
        }

        // Presiona enter para cambiar de jugador
        private static void ChangePlayer( Player player)
        {
            ConsoleKeyInfo press = Console.ReadKey(true);
            while (press.Key != ConsoleKey.Enter)
            {
                if (press.Key == ConsoleKey.Escape) SetPauseMenu();
                press = Console.ReadKey(true);
            }
            Console.Clear();
            AnsiConsole.Write(new FigletText(player.Username).Color(player.playerColor));
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
            maze.PrintMaze();

        }

        // Imprine la informacion de los jugadores
        public static void PrintInfo(List<Player> players)
        {
            List<Text> columns = new List<Text>();

            foreach(Player player in players)
            {
                columns.Add(player.SetInfo());
            }

            Columns info = new Columns(columns);

            Console.WriteLine();
            AnsiConsole.Write(info);

        }

        // Imprime el nombre del ganador y termina el juego
        public static void SetWinner(Player player)
        {
            Console.Clear();
            AnsiConsole.Write(new FigletText(player.Username).Color(player.playerColor).Centered());
            Console.WriteLine();
            AnsiConsole.Write(new FigletText("IS THE WINNER").Color(player.playerColor).Centered());
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        }

    }
}
