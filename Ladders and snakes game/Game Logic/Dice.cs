using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladders_and_snakes_game.Utilities;

namespace Ladders_and_snakes_game.Game_Logic
{
    internal class Dice
    {
        public Dice() { }
        public int RollTheDice()
        {
            // real return value between 1 and 6 
            return RandomProvider.Instance.Next(1, 7); 
        }
    }
}
