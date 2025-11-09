using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladders_and_snakes_game
{
    internal class BoardFactory
    {
        private  int SnakesNumber { get; }
        private  int LaddersNumber { get; }

        public BoardFactory(int snakesNumber , int laddersNumber)
        {
            SnakesNumber = snakesNumber;
            LaddersNumber = laddersNumber;
        }

        public void InitSnakes(ref Board gameBoard)
        {
            
        }
    }
}
