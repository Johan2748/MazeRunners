using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace Maze_Runners
{
    class Player
    {
        public string Username { get; private set; }

        public Piece piece { get; private set; }

        public Color playerColor { get; private set; }

        public Player(string username, Piece piece)
        {
            Username = username;
            this.piece = piece;
            playerColor = piece.Box.color;
        }

        public Text SetInfo()
        {
            Text info = new Text(
                Username + "\n\n" +
                "Moves: " + piece.speed + "\n" +
                "Cooldown: " + piece.cooldown,
                new Style(playerColor)
                );

            return info;
        }



    }
}
