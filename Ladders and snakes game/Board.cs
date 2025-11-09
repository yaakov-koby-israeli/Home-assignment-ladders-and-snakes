-using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladders_and_snakes_game
{
    internal class Board
    {
        private Cell[] _cells;

        private int _boardSize = 0;

        public Board(int boardSize)
        {
            SetBoardSize(boardSize);
            _cells = new Cell[boardSize + 1];
        }

        // create setBoardSize method for future validation if needed
        private void SetBoardSize(int boardSize)
        {
            _boardSize = boardSize;
        }

    }
}
