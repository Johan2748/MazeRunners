using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace Maze_Runners
{
    // Casilla padre
    class Box
    {
        // Color de la Casilla
        public Color color;

        public Box()
        {
            color = Color.Black;
        }

        public Box(Color color)
        {
            this.color = color;
        }

        // Imprime la Casilla en Pantalla
        public virtual void PrintBox()
        {
            // Parsea el color para q el markup lo pueda interpretar
            var m_color = color.ToMarkup();
            AnsiConsole.Markup($"[on {m_color}]  [/]");
        }
    }

    // Camino libre en el laberinto
    class EmptyBox : Box
    {
        public EmptyBox()
        {
            color = Color.Grey0;
        }
    }

    // Muro
    class Wall : Box
    {
        public Wall()
        {
            this.color = Color.DarkOrange3_1;
        }
    }

    // Casilla de victoria
    class WinnerBox : Box
    {
        public WinnerBox()
        {
            this.color = Color.Yellow1; 
        }
    }


    // Casilla para la ficha del jugador
    class PlayerBox : Box
    {
        private string id;

        public PlayerBox(string id, Color color)
        {
            this.color = color;
            this.id = id;
        }

        public override void PrintBox()
        {
            string c_color = color.ToMarkup();
            AnsiConsole.Markup($"[black on {c_color}]{id}[/]");
        }
    }


    // Trampas implementadas 
    class Trap : Box
    {
        // Tipo de trampa
        public enum TrapType { Ice, Portal, SpeedPotion, ShapeShifterPoison }

        public TrapType Type { get; private set; }

        public Trap() { }

        public Trap(TrapType type)
        {
            this.Type = type;
            SetColor();
        }

        // Establece el color caracteristico de la casilla
        private void SetColor()
        {
            if (Type == TrapType.Ice) color = Color.Aqua;
            if (Type == TrapType.Portal) color = Color.Fuchsia;
            if (Type == TrapType.SpeedPotion) color = Color.LightGoldenrod1;
            if (Type == TrapType.ShapeShifterPoison) color = Color.HotPink3_1;
        }

        // Acciona el efecto correspondiente a la trampa
        public void SetEffect(Piece piece)
        {
            // Si la trampa es hielo, termina el turno del jugador
            if (Type == TrapType.Ice)
            {
                piece.speed = 0;
            }
            // Si la trampa es velocidad le da mas movimientos a la pieza
            else if (Type == TrapType.SpeedPotion)
            {
                piece.speed += 5;
            }
            // Si la trampa es de cambia-formas le cambia el poder por otro al azar
            else if(Type == TrapType.ShapeShifterPoison)
            {
                List<Power> powers = new List<Power>() { Power.Break, Power.Jump, Power.Run, Power.Skate, Power.Teleport };
                powers.Remove(piece.power);
                piece.power = powers[new Random().Next(powers.Count)];
            }
        }

        // Efecto especial del Portal
        public void Teleport(Maze maze, Piece piece, int n, int m)
        {
            List<(int, int)> positions = new List<(int, int)>();

            // En la posicion del actual portal pone un camino libre, se asegura q no se incluya a la lista
            maze.maze[n, m] = new EmptyBox();

            for (int i = 0; i < maze.scale; i++)
            {
                for (int j = 0; j < maze.scale; j++)
                {
                    if (maze.maze[i, j].color == Color.Fuchsia)
                    {
                        // Crea y rellena una lista con las posiciones de los otros portales
                        positions.Add((i, j));
                    }
                }
            }
            // Obtiene la posicion de un portal aleatorio
            int pos = new Random().Next(positions.Count);
            // Pone en esa posicion del laberinto la ficha
            maze.maze[positions[pos].Item1, positions[pos].Item2] = piece.Box;
            // Actualiza las coordenadas de la ficha
            piece.x = positions[pos].Item1;
            piece.y = positions[pos].Item2;

            piece.speed--;
            maze.PrintMaze();
        }

    }

    





























    
}
