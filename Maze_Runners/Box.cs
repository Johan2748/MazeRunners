using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace Maze_Runners
{
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
            AnsiConsole.Markup($"[[[on {m_color}]  [/]]] ");
        }


    }

    class EmptyBox : Box
    {
        public override void PrintBox()
        {
            Console.Write("     ");
        }

    }

    class Wall : Box
    {
        public Wall()
        {
            this.color = Color.DarkOrange3_1;
        }
    }

    class Tramp
    {

    }

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
            AnsiConsole.Markup($"[[[on {c_color}]{id}[/]]] ");
        }
    }























}
