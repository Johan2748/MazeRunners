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
        public enum TrapType { Ice, Portal, SpeedPotion }

        public TrapType Type;

        public Trap(TrapType type)
        {
            this.Type = type;
            SetColor();
        }

        // EStablece el color caracteristico de la casilla
        private void SetColor()
        {
            if (Type == TrapType.Ice) { color = Color.Aqua; }
            if (Type == TrapType.Portal) { color = Color.Fuchsia; }
            if (Type == TrapType.SpeedPotion) { color = Color.LightGoldenrod1; }
        }

        public void SetEffect()
        {

        }


    }

    





























    
}
