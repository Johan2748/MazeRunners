using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace Maze_Runners
{
    class Piece
    {
        private int x;
        private int y;

        private int o_speed;
        private int speed;

        private int o_cooldown;
        private int cooldown;

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

                // Se mueve en la direccion marcada si esta disponible
                if (move.Key == ConsoleKey.UpArrow)
                {
                    if (maze.maze[x - 1, y].GetType() != typeof(Wall))
                    {
                        maze.maze[x - 1, y] = Box;
                        maze.maze[x, y] = new EmptyBox();
                        x--;
                        maze.PrintMaze();
                        speed--;
                    }
                }
                // Se mueve en la direccion marcada si esta disponible
                else if (move.Key == ConsoleKey.LeftArrow)
                {
                    if (maze.maze[x, y - 1].GetType() != typeof(Wall))
                    {
                        maze.maze[x, y - 1] = Box;
                        maze.maze[x, y] = new EmptyBox();
                        y--;
                        maze.PrintMaze();
                        speed--;
                    }
                }
                // Se mueve en la direccion marcada si esta disponible
                else if (move.Key == ConsoleKey.RightArrow)
                {
                    if (maze.maze[x, y + 1].GetType() != typeof(Wall))
                    {
                        maze.maze[x, y + 1] = Box;
                        maze.maze[x, y] = new EmptyBox();
                        y++;
                        maze.PrintMaze();
                        speed--;
                    }
                }
                // Se mueve en la direccion marcada si esta disponible
                else if (move.Key == ConsoleKey.DownArrow)
                {
                    if (maze.maze[x + 1, y].GetType() != typeof(Wall))
                    {
                        maze.maze[x + 1, y] = Box;
                        maze.maze[x, y] = new EmptyBox();
                        x++;
                        maze.PrintMaze();
                        speed--;
                    }
                }
                // Si asegura q solo se toquen las teclas de movimiento
                else Move(maze);

                // Rompe el ciclo si se cumple la condicion de victoria
                if (maze.maze[maze.scale / 2, maze.scale / 2].GetType() != typeof(WinnerBox)) break;
            }
            // Vuelve su capacidad de movimiento a la original
            speed = o_speed;
        }


    }
    public enum Power { Break, Jump, Skate, Teleport, Run}
}
