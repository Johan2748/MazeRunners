using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace Maze_Runners
{
    class Piece
    {
        public int x;
        public int y;

        private int o_speed;
        public int speed;

        private int o_cooldown;
        public int cooldown;

        public Power power;

        public PlayerBox Box { get; private set; }

        

        public Piece(int speed,int cooldown, Power power, Color color, string id)
        {
            o_speed = speed;
            this.speed = speed;
            o_cooldown = cooldown;
            this.cooldown = 0;
            this.power = power;
            Box = new PlayerBox(id, color);
        }

        // Establece las coordenadas de origen de la ficha
        public void SetOrigin(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Mueve la ficha en el laberinto y actualiza las coordenadas
        public void Move(Maze maze)
        {
            // se mantiene en movimiento mientras tenga capacidad
            while (speed > 0)
            {
                // Lee la tecla presionada
                ConsoleKeyInfo move = Console.ReadKey(true);

                // Se mueve hacia arriba si esta disponible
                if (move.Key == ConsoleKey.UpArrow || move.Key == ConsoleKey.W)
                {
                    // Chequea q no sea un muro u otro jugador
                    if (maze.maze[x - 1, y].GetType() != typeof(Wall) && maze.maze[x - 1, y].GetType() != typeof(PlayerBox))
                    {
                        // Chequea si es una trampa y activa el efecto
                        if (maze.maze[x - 1, y].GetType() == typeof(Trap))
                        {
                            Trap trap = new Trap();
                            trap = maze.maze[x - 1, y] as Trap;
                            trap.SetEffect(this);
                            if (trap.Type == Trap.TrapType.Portal)
                            {
                                maze.maze[x, y] = new EmptyBox();
                                trap.Teleport(maze, this, x - 1, y);
                                GameManager.PrintInfo(GameManager.PlayersList);
                                continue;
                            }
                        }

                        // Actualiza las casillas del laberinto
                        maze.maze[x - 1, y] = Box;
                        maze.maze[x, y] = new EmptyBox();
                        x--;
                        maze.PrintMaze();
                        speed--;
                        GameManager.PrintInfo(GameManager.PlayersList);
                    }
                }
                // Se mueve hacia la izquierda si esta disponible
                else if (move.Key == ConsoleKey.LeftArrow || move.Key == ConsoleKey.A) 
                {
                    // Chequea q no sea un muro u otro jugador
                    if (maze.maze[x, y - 1].GetType() != typeof(Wall) && maze.maze[x, y - 1].GetType() != typeof(PlayerBox))
                    {
                        // Chequea si es una trampa y activa el efecto
                        if (maze.maze[x, y - 1].GetType() == typeof(Trap))
                        {
                            Trap trap = new Trap();
                            trap = maze.maze[x, y - 1] as Trap;
                            trap.SetEffect(this);
                            if (trap.Type == Trap.TrapType.Portal)
                            {
                                maze.maze[x, y] = new EmptyBox();
                                trap.Teleport(maze, this, x, y - 1);
                                GameManager.PrintInfo(GameManager.PlayersList);
                                continue;
                            }
                        }

                        // Actualiza las casillas del laberinto
                        maze.maze[x, y - 1] = Box;
                        maze.maze[x, y] = new EmptyBox();
                        y--;
                        maze.PrintMaze();
                        speed--;
                        GameManager.PrintInfo(GameManager.PlayersList);
                    }
                }
                // Se mueve hacia la derecha si esta disponible
                else if (move.Key == ConsoleKey.RightArrow || move.Key == ConsoleKey.D)
                {
                    // Chequea q no sea un muro u otro jugador
                    if (maze.maze[x, y + 1].GetType() != typeof(Wall) && maze.maze[x, y + 1].GetType() != typeof(PlayerBox))
                    {
                        // Chequea si es una trampa y activa el efecto
                        if (maze.maze[x, y + 1].GetType() == typeof(Trap))
                        {
                            Trap trap = new Trap();
                            trap = maze.maze[x, y + 1] as Trap;
                            trap.SetEffect(this);
                            if (trap.Type == Trap.TrapType.Portal)
                            {
                                maze.maze[x, y] = new EmptyBox();
                                trap.Teleport(maze, this, x, y + 1);
                                GameManager.PrintInfo(GameManager.PlayersList);
                                continue;
                            }
                        }

                        // Actualiza las casillas del laberinto
                        maze.maze[x, y + 1] = Box;
                        maze.maze[x, y] = new EmptyBox();
                        y++;
                        maze.PrintMaze();
                        speed--;
                        GameManager.PrintInfo(GameManager.PlayersList);
                    }
                }
                // Se mueve hacia abajo si esta disponible
                else if (move.Key == ConsoleKey.DownArrow || move.Key == ConsoleKey.S)
                {
                    // Chequea q no sea un muro u otro jugador
                    if (maze.maze[x + 1, y].GetType() != typeof(Wall) && maze.maze[x + 1, y].GetType() != typeof(PlayerBox))
                    {
                        // Chequea si es una trampa y activa el efecto
                        if (maze.maze[x + 1, y].GetType() == typeof(Trap))
                        {
                            Trap trap = new Trap();
                            trap = maze.maze[x + 1, y] as Trap;
                            trap.SetEffect(this);
                            if (trap.Type == Trap.TrapType.Portal)
                            {
                                maze.maze[x, y] = new EmptyBox();
                                trap.Teleport(maze, this, x + 1, y);
                                GameManager.PrintInfo(GameManager.PlayersList);
                                continue;
                            }
                        }

                        // Actualiza las casillas del laberinto
                        maze.maze[x + 1, y] = Box;
                        maze.maze[x, y] = new EmptyBox();
                        x++;
                        maze.PrintMaze();
                        speed--;
                        GameManager.PrintInfo(GameManager.PlayersList);
                    }
                }

                // Opcion para salir del juego
                else if (move.Key == ConsoleKey.Escape)
                {
                    GameManager.SetPauseMenu();
                }

                else if (move.Key == ConsoleKey.P) 
                {
                    ActivatePower(maze);
                }

                

                // Rompe el ciclo si se cumple la condicion de victoria
                if (maze.maze[maze.scale / 2, maze.scale / 2].GetType() != typeof(WinnerBox)) break;
            }
            // Vuelve su capacidad de movimiento a la original
            speed = o_speed;

            if (cooldown > 0) cooldown--;

        }

        // Activa la habilidad especial de la ficha
        private void ActivatePower(Maze maze)
        {
            // Solo se activa si se enfrio totalmente la habilidad
            if(cooldown == 0)
            {
                // Rompe un muro
                if (power == Power.Break)
                {
                    ConsoleKeyInfo block = Console.ReadKey(true);

                    // Rompe el muro superior (si no es un borde)
                    if (block.Key == ConsoleKey.UpArrow || block.Key == ConsoleKey.W)
                    {
                        if (maze.maze[x - 1, y].GetType() == typeof(Wall) && x - 1 != 0)
                        {
                            maze.maze[x - 1, y] = new EmptyBox();
                        }
                    }
                    // Rompe el muro Izquierdo (si no es un borde)
                    else if (block.Key == ConsoleKey.LeftArrow || block.Key == ConsoleKey.A)
                    {
                        if (maze.maze[x, y - 1].GetType() == typeof(Wall) && y - 1 != 0)
                        {
                            maze.maze[x, y - 1] = new EmptyBox();
                        }
                    }
                    // Rompe el muro inferior (si no es un borde)
                    else if (block.Key == ConsoleKey.DownArrow || block.Key == ConsoleKey.S)
                    {
                        if (maze.maze[x + 1, y].GetType() == typeof(Wall) && x + 1 != maze.scale - 1)
                        {
                            maze.maze[x + 1, y] = new EmptyBox();
                        }
                    }
                    // Rompe el muro derecho (si no es un borde)
                    else if (block.Key == ConsoleKey.RightArrow || block.Key == ConsoleKey.D) 
                    {
                        if (maze.maze[x, y + 1].GetType() == typeof(Wall) && y + 1 != maze.scale - 1)
                        {
                            maze.maze[x, y + 1] = new EmptyBox();
                        }
                    }
                    else ActivatePower(maze);

                    maze.PrintMaze();
                }
                // Salta una casilla
                if (power == Power.Jump)
                {
                    ConsoleKeyInfo block = Console.ReadKey(true);

                    //  Salta la casilla superior (si la siguiente esta vacia)
                    if (block.Key == ConsoleKey.UpArrow || block.Key == ConsoleKey.W)
                    {
                        if (x - 2 > 0 && maze.maze[x - 2, y].GetType() == typeof(EmptyBox))
                        {
                            maze.maze[x - 2, y] = Box;
                            maze.maze[x, y] = new EmptyBox();
                            x -= 2;
                        }
                    }
                    // Salta la casilla izquierda (si la siguiente esta vacia)
                    else if (block.Key == ConsoleKey.LeftArrow || block.Key == ConsoleKey.A)
                    {
                        if (y - 2 > 0 && maze.maze[x, y - 2].GetType() == typeof(EmptyBox))
                        {
                            maze.maze[x, y - 2] = Box;
                            maze.maze[x, y] = new EmptyBox();
                            y -= 2;
                        }
                    }
                    // Rompe el muro inferior (si no es un borde)
                    else if (block.Key == ConsoleKey.DownArrow || block.Key == ConsoleKey.S)
                    {
                        if (x + 2 < maze.scale - 1 && maze.maze[x + 2, y].GetType() == typeof(EmptyBox))
                        {
                            maze.maze[x + 2, y] = Box;
                            maze.maze[x, y] = new EmptyBox();
                            x += 2;
                        }
                    }
                    // Rompe el muro derecho (si no es un borde)
                    else if (block.Key == ConsoleKey.RightArrow || block.Key == ConsoleKey.D)
                    {
                        if (y + 2 < maze.scale - 1 && maze.maze[x, y + 2].GetType() == typeof(EmptyBox))
                        {
                            maze.maze[x, y + 2] = Box;
                            maze.maze[x, y] = new EmptyBox();
                            y += 2;
                        }
                    }
                    else ActivatePower(maze);

                    maze.PrintMaze();
                }
                // Aumenta en 10 las casillas q puede recorrer
                if (power == Power.Run)
                {
                    speed += 10;
                    maze.PrintMaze();
                }
                // Se mueve hasta q algo la detenga
                if (power == Power.Skate)
                {
                    ConsoleKeyInfo move = Console.ReadKey(true);

                    if (move.Key == ConsoleKey.UpArrow || move.Key == ConsoleKey.W)
                    {
                        while (maze.maze[x - 1, y].GetType() == typeof(EmptyBox))
                        {
                            maze.maze[x - 1, y] = Box;
                            maze.maze[x, y] = new EmptyBox();
                            x--;
                        }
                    }
                    else if (move.Key == ConsoleKey.LeftArrow || move.Key == ConsoleKey.A)
                    {
                        while (maze.maze[x, y - 1].GetType() == typeof(EmptyBox))
                        {
                            maze.maze[x, y - 1] = Box;
                            maze.maze[x, y] = new EmptyBox();
                            y--;
                        }
                    }
                    else if (move.Key == ConsoleKey.DownArrow || move.Key == ConsoleKey.S)
                    {
                        while (maze.maze[x + 1, y].GetType() == typeof(EmptyBox))
                        {
                            maze.maze[x + 1, y] = Box;
                            maze.maze[x, y] = new EmptyBox();
                            x++;
                        }
                    }
                    else if (move.Key == ConsoleKey.RightArrow || move.Key == ConsoleKey.D)
                    {
                        while (maze.maze[x, y + 1].GetType() == typeof(EmptyBox))
                        {
                            maze.maze[x, y + 1] = Box;
                            maze.maze[x, y] = new EmptyBox();
                            y++;
                        }
                    }
                    else ActivatePower(maze);

                    maze.PrintMaze();
                }
                // Se teletransporta a un lugar aleatorio del laberinto q este libre
                if (power == Power.Teleport)
                {
                    while (true)
                    {
                        int new_x = new Random().Next(maze.scale);
                        int new_y = new Random().Next(maze.scale);

                        if (maze.maze[new_x, new_y].GetType() == typeof(EmptyBox))
                        {
                            maze.maze[x, y] = new EmptyBox();
                            maze.maze[new_x, new_y] = Box;
                            x = new_x;
                            y = new_y;
                            maze.PrintMaze();
                            break;
                        }
                    }
                }

                cooldown = o_cooldown + 1;
                GameManager.PrintInfo(GameManager.PlayersList);
            }

        }

    }
    
    public enum Power { Break, Jump, Skate, Teleport, Run}
}
