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

        private int _rows = 0;
        private int _cols = 0;

        public Board(int rows , int cols)
        {
            _rows = rows;
            _cols = cols;
            
            SetBoardSize(rows * cols);

            _cells = new Cell[_boardSize + 1];
        }

        // create setBoardSize method for future validation if needed
        private void SetBoardSize(int boardSize)
        {
            _boardSize = boardSize;
        }

    }
}
