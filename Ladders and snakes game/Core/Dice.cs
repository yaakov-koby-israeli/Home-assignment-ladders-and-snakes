using Ladders_and_snakes_game.Utilities;

namespace Ladders_and_snakes_game.Core
{
    internal sealed class Dice
    {
        public Dice() { }
        public int RollTheDice()
        {
            // real return value between 1 and 6 
            return RandomProvider.Instance.Next(1, 7); 
        }
    }
}
