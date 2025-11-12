using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladders_and_snakes_game.Configuration
{
    internal static class GameSettings
    {
        // Default values (you can change them at runtime before starting the game)
        public static int MaxSnakes { get;} = 12;
        public static int MaxLadders { get; } = 12;
        public static int MaxGold { get; } = 2;
        public static int Rows { get; set; } = 10;
        public static int Cols { get; set; } = 10;
        public static int Snakes { get; set; } = 10;
        public static int Ladders { get; set; } = 10;
        public static int Players { get; set; } = 2;

    }
}
