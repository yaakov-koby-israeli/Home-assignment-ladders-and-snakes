using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladders_and_snakes_game.Players
{
    internal interface IPlayer
    {
        int Id { get; }

        int Position { get; set; }

        bool HasWon { get; set; }

        void MovePlayer(int diceResult);

    }
}
