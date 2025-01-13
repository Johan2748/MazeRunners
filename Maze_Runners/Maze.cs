using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;
using System.Threading;

namespace Maze_Runners
{
    class Maze
    {
        public Box[,] maze;

        private int[,] template;
        public int scale { get; private set; }
        private bool is_first = true;

        private enum Direction { West, North, East, South}


        public Maze(int scale)
        {
            this.scale = scale;

            GenerateTemplate();
        }

        // Genera la plantilla del laberinto
        private void GenerateTemplate()
        {
            // Aqui vamos a utilizar una plantilla de enteros donde los 0 son muros y los 1 son caminos libres

            // Genera la matriz plantilla llena de ceros (muros)
            template = new int[scale, scale];

            // Genera e imprime el laberinto lleno de bloques y espera a que se presione una tecla para generar los caminos
            if (is_first)
            {
                GenerateMaze();
                PrintMaze();
                Console.ReadKey();
                is_first = false;
            }


            // Pila q almacena las coordenadas de las casillas recorridas
            Stack<(int, int)> stack = new Stack<(int, int)>();

            // Generamos una posicion aleatoria para iniciar la generacion del laberinto que no sea una frontera
            int open_x = new Random().Next(1, scale - 2);
            int open_y = new Random().Next(1, scale - 2);

            // Aseguramos q la casilla sea impar para una mejor generacion del laberinto
            if (open_x % 2 == 0)
            {
                if (open_x <= 10) open_x += 1;
                else open_x -= 1;
            }
            if (open_y % 2 == 0)
            {
                if (open_y < 10) open_y += 1;
                else open_y -= 1;
            }

            // Inicia el camino procedural en la casilla aleatoria
            template[open_x, open_y] = 1;

            // Agregar la casilla de inicio a la pila
            stack.Push((open_x, open_y));

            
            // Mientras queden casillas por recorrer, seguir generando el laberinto
            while (stack.Count > 0)
            {
                // Almaceno las coordenadas de la casilla actual
                (int x ,int y) current = stack.Peek();

                // Cantidad de revisiones sobre la actual casilla y un booleano para romper el bucle
                int checks = 0;
                bool break_loop = false;              

                // Mientras no se haya chequeado 10 veces las direcciones y la casilla aun tenga vecina libre
                while (checks < 10 && !break_loop)
                {
                    checks++;

                    // Randomizo una direccion
                    Direction direction = (Direction)new Random().Next(4);

                    switch (direction)
                    {

                        case Direction.West:
                            // Me aseguro que no esta en el borde izquierdo
                            if(current.y > 1)
                            {
                                // Me aseguro q la casilla a la izquierna no haya sido visitada aun
                                if (template[current.x, current.y - 2] == 0) 
                                {
                                    // Marco el recorrido como libre
                                    template[current.x, current.y - 1] = 1;
                                    template[current.x, current.y - 2] = 1;

                                    // Agrego la casilla actual a la pila y la marco para salir del bucle
                                    stack.Push((current.x, current.y - 2));
                                    break_loop = true;
                                }
                            }
                            break;

                        case Direction.North:
                            // Me aseguro que no esta en el borde superior
                            if (current.x > 1)
                            {
                                // Me aseguro q la casilla superior no haya sido visitada aun
                                if (template[current.x - 2, current.y] == 0)
                                {
                                    // Marco el recorrido como libre
                                    template[current.x - 1, current.y] = 1;
                                    template[current.x - 2, current.y] = 1;

                                    // Agrego la casilla actual a la pila y la marco para salir del bucle
                                    stack.Push((current.x - 2, current.y));
                                    break_loop = true;
                                }
                            }
                            break;

                        case Direction.East:
                            // Me aseguro que no esta en el borde derecho
                            if (current.y < scale - 2) 
                            {
                                // Me aseguro q la casilla a la derecha no haya sido visitada aun
                                if (template[current.x, current.y + 2] == 0)
                                {
                                    // Marco el recorrido como libre
                                    template[current.x, current.y + 1] = 1;
                                    template[current.x, current.y + 2] = 1;

                                    // Agrego la casilla actual a la pila y la marco para salir del bucle
                                    stack.Push((current.x, current.y + 2));
                                    break_loop = true;
                                }
                            }
                            break;

                        case Direction.South:
                            // Me aseguro que no esta en el borde derecho
                            if (current.x < scale - 2)
                            {
                                // Me aseguro q la casilla a la derecha no haya sido visitada aun
                                if (template[current.x + 2, current.y] == 0)
                                {
                                    // Marco el recorrido como libre
                                    template[current.x + 1, current.y] = 1;
                                    template[current.x + 2, current.y] = 1;

                                    // Agrego la casilla actual a la pila y la marco para salir del bucle
                                    stack.Push((current.x + 2, current.y));
                                    break_loop = true;
                                }
                            }
                            break;

                    }
                }

                if (!break_loop)
                {
                    stack.Pop();
                }

            }
            // Genero la casilla de victoria en el centro del laberinto
            template[scale / 2, scale / 2] = 100;

            // Genera el laberinto con las casillas y lo imprime
            GenerateMaze();
            PrintMaze();

            // Pregunta si va a jugar en ese laberinto o genera uno distinto
            AnsiConsole.Markup("\n([grey]Press [green]{Space}[/] to generate a different maze or [green]{Enter}[/] to play)[/]");

            ConsoleKeyInfo press = Console.ReadKey(true);

            DoItAgain(press);
        }

        // Vuelve a generar el laberinto
        private void DoItAgain(ConsoleKeyInfo press)
        {
            // Repite el proceso miestras se presione "spacebar"
            if (press.Key == ConsoleKey.Spacebar)
            {
                this.GenerateTemplate();
            }
            // No sale del bucle mientras no se presione una tecla distinta
            else if (press.Key != ConsoleKey.Enter)
            {
                press = Console.ReadKey(true);
                DoItAgain(press);
            }
            // Sale del bucle y genera las trampas y los jugadores al presonar "enter"
            else
            {
                
            }
        }

        // Genera las trampas
        private void SetTraps(int n)
        {
            int ice;
            int portal;
            int speed;
            int shape_shifter;

            // Genera una cantidad fija de trampas en dependencia de los jugadores
            ice = n; portal = n / 2 * 2; speed = n; shape_shifter = n;
            while (ice > 0 || portal > 0 || speed > 0)
            {
                while (ice > 0)
                {
                    int x = new Random().Next(scale);
                    int y = new Random().Next(scale);
                    if (maze[x, y].GetType() == typeof(EmptyBox)) { maze[x, y] = new Trap(Trap.TrapType.Ice); break; }
                }
                ice--;
                while (portal > 0)
                {
                    int x = new Random().Next(scale);
                    int y = new Random().Next(scale);
                    if (maze[x, y].GetType() == typeof(EmptyBox)) { maze[x, y] = new Trap(Trap.TrapType.Portal); break; }
                }
                portal--;
                while (speed > 0)
                {
                    int x = new Random().Next(scale);
                    int y = new Random().Next(scale);
                    if (maze[x, y].GetType() == typeof(EmptyBox)) { maze[x, y] = new Trap(Trap.TrapType.SpeedPotion); break; }
                }
                speed--;
                while (shape_shifter > 0)
                {
                    int x = new Random().Next(scale);
                    int y = new Random().Next(scale);
                    if (maze[x, y].GetType() == typeof(EmptyBox)) { maze[x, y] = new Trap(Trap.TrapType.ShapeShifterPoison); break; }
                }
                shape_shifter--;
            }
        }    

        // Genera e imprime en pantalla las trampas y los jugadores
        public void SetTrapsAndPlayers(List<Player> players)
        {
            // Genera los objetos del laberinto en dependencia de la ctdad de jugadores
            if (players.Count == 2)
            {
                // Ubica al jugador 1 en el laberinto y actualiza su posicion
                maze[1, 1] = players[0].piece.Box;
                players[0].piece.SetOrigin(1, 1);
                // Ubica al jugador 2 en el laberinto y actualiza su posicion
                maze[scale - 2, scale - 2] = players[1].piece.Box;
                players[1].piece.SetOrigin(scale - 2, scale - 2);

                SetTraps(3);
                PrintMaze();
            }
            if (players.Count == 3)
            {
                // Ubica al jugador 1 en el laberinto y actualiza su posicion
                maze[1, 1] = players[0].piece.Box;
                players[0].piece.SetOrigin(1, 1);
                // Ubica al jugador 2 en el laberinto y actualiza su posicion
                maze[scale - 2, scale - 2] = players[1].piece.Box;
                players[1].piece.SetOrigin(scale - 2, scale - 2);
                // Ubica al jugador 3 en el laberinto y actualiza su posicion
                maze[1, scale - 2] = players[2].piece.Box;
                players[2].piece.SetOrigin(1, scale - 2);

                SetTraps(5);
                PrintMaze();
            }
            if (players.Count == 4)
            {
                // Ubica al jugador 1 en el laberinto y actualiza su posicion
                maze[1, 1] = players[0].piece.Box;
                players[0].piece.SetOrigin(1, 1);
                // Ubica al jugador 2 en el laberinto y actualiza su posicion
                maze[scale - 2, scale - 2] = players[1].piece.Box;
                players[1].piece.SetOrigin(scale - 2, scale - 2);
                // Ubica al jugador 3 en el laberinto y actualiza su posicion
                maze[1, scale - 2] = players[2].piece.Box;
                players[2].piece.SetOrigin(1, scale - 2);
                // Ubica al jugador 4 en el laberinto y actualiza su posicion
                maze[scale - 2, 1] = players[3].piece.Box;
                players[3].piece.SetOrigin(scale - 2, 1);

                SetTraps(6);
                PrintMaze();
            }
        }

        // A partir de la plantilla, genera las casillas en su correspondiente lugar
        public void GenerateMaze()
        {
            maze = new Box[scale, scale];

            for (int i = 0; i < scale; i++)
            {
                for (int j = 0; j < scale; j++)
                {
                    // Si la plantilla tiene 0 genera un muro y si tiene un 1 genera un camino
                    if (template[i, j] == 0) maze[i, j] = new Wall();
                    if (template[i, j] == 1) maze[i, j] = new EmptyBox();

                    // Numeracion de las casillas especiales
                    if (template[i, j] == 100) maze[i, j] = new WinnerBox();
                }
            }
        }

        // Imprime el laberinto en pantalla
        public void PrintMaze()
        {
            Console.Clear();
            for(int i = 0; i < maze.GetLength(0); i++)
            {
                for(int j = 0; j < maze.GetLength(1); j++)
                {
                    maze[i, j].PrintBox();
                }
                Console.WriteLine();
            }
        }
    }
}
