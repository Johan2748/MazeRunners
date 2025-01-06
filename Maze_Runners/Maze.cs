using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace Maze_Runners
{
    class Maze
    {
        public Box[,] maze;
        public Box Box;

        public Maze(int scale)
        {
            maze = new Box[scale,scale];

            for (int i = 0; i < scale; i++)
            {
                for (int j = 0; j < scale; j++) 
                {
                    if (i == 0 || i == scale - 1 || j == 0 || j == scale - 1) maze[i, j] = new PlayerBox("Mr",Color.Red);
                    else maze[i, j] = new EmptyBox();
                }
            }

        }

        public void GenerateMaze()
        {
            for(int i = 0; i < maze.GetLength(0); i++)
            {
                for(int j = 0; j < maze.GetLength(1); j++)
                {
                    maze[i, j].PrintBox();
                }
                Console.WriteLine("\n");
            }
        }
    }
}
