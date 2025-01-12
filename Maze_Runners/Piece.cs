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

        private Power power;

        public PlayerBox Box { get; private set; }

        

        public Piece(int speed,int cooldown, Power power, Color color, string id)
        {
            o_speed = speed;
            this.speed = speed;
            o_cooldown = cooldown;
            this.cooldown = cooldown;
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
                if (move.Key == ConsoleKey.UpArrow)
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
                                continue;
                            }
                        }

                        // Actualiza las casillas del laberinto
                        maze.maze[x - 1, y] = Box;
                        maze.maze[x, y] = new EmptyBox();
                        x--;
                        maze.PrintMaze();
                        speed--;
                    }
                }
                // Se mueve hacia la izquierda si esta disponible
                else if (move.Key == ConsoleKey.LeftArrow)
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
                                continue;
                            }
                        }

                        // Actualiza las casillas del laberinto
                        maze.maze[x, y - 1] = Box;
                        maze.maze[x, y] = new EmptyBox();
                        y--;
                        maze.PrintMaze();
                        speed--;
                    }
                }
                // Se mueve hacia la derecha si esta disponible
                else if (move.Key == ConsoleKey.RightArrow)
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
                                continue;
                            }
                        }

                        // Actualiza las casillas del laberinto
                        maze.maze[x, y + 1] = Box;
                        maze.maze[x, y] = new EmptyBox();
                        y++;
                        maze.PrintMaze();
                        speed--;
                    }
                }
                // Se mueve hacia abajo si esta disponible
                else if (move.Key == ConsoleKey.DownArrow)
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
                                continue;
                            }
                        }
                        
                        // Actualiza las casillas del laberinto
                        maze.maze[x + 1, y] = Box;
                        maze.maze[x, y] = new EmptyBox();
                        x++;
                        maze.PrintMaze();
                        speed--;
                    }
                }

                // Opcion para salir del juego
                else if (move.Key == ConsoleKey.Escape)
                {
                    GameManager.SetPauseMenu();
                }

                // Rompe el ciclo si se cumple la condicion de victoria
                if (maze.maze[maze.scale / 2, maze.scale / 2].GetType() != typeof(WinnerBox)) break;
            }
            // Vuelve su capacidad de movimiento a la original
            speed = o_speed;
        }


        private void ActivatePower()
        {
            if (power == Power.Break)
            {
                
            }
            if (power == Power.Jump)
            {

            }
            if (power == Power.Run)
            {

            }
            if (power == Power.Skate)
            {

            }
            if (power == Power.Teleport)
            {

            }

        }









    }


    








    public enum Power { Break, Jump, Skate, Teleport, Run}
}
