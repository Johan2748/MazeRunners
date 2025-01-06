using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace Maze_Runners
{
    class Piece
    {
        
        public int Speed { get; private set; }
        public int Cooldown { get; private set; }

        public Power Power;

        public PlayerBox box { get; private set; }

        

        public Piece(int speed,int cooldown, Power power, Color color, string id)
        {
            Speed = speed;
            Cooldown = cooldown;
            this.Power = power;
            box = new PlayerBox(id, color);
        }



    }
    public enum Power { }
}
