using System;
using System.Collections.Generic;
using System.Text;

namespace Maze_Runners
{
    class Player
    {
        string Username;

        public Piece piece;

        public Player(string username, Piece piece)
        {
            Username = username;
            this.piece = piece;
        }

    }
}
