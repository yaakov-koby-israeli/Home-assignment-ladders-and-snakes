using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladders_and_snakes_game
{
    internal class Player : IPlayer
    {
        private static int _initID = 1;

        public int Id { get; private set; }

        // All players start at position 0
        public int Position { get; set; } = 0;

        public bool HasWon { get; set; } = false;

        public Player()
        {
            Id = _initID++;
        }
    }
}
