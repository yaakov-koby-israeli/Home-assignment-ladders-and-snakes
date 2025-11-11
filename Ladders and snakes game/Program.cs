using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladders_and_snakes_game.Front;

namespace Ladders_and_snakes_game
{
    internal class Program
    {
        public static void Main()
        { 
            UserInterface game = new UserInterface();
            game.StartGame();
        }
    }
}
