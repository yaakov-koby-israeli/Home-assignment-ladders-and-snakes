namespace Ladders_and_snakes_game.Configuration
{
    internal static class GameSettings
    {
        // Default values 
        public static int MaxSnakes { get;} = 12;
        public static int MaxLadders { get; } = 12; 
        public static int MaxGold { get; } = 2;
        public static int MaxAttemptsToInitBoard { get; } = 5;
        public static int Rows { get; } = 10;
        public static int Cols { get; } = 10;
        public static int Snakes { get; set; } = 10;
        public static int Ladders { get; set; } = 10;
        public static int Players { get; set; } = 2;
        
    }
}
