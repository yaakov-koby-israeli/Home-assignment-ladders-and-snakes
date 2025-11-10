using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladders_and_snakes_game.Core
{
    internal class Board
    {
        private int _rows = 0;
        private int _cols = 0;

        private int _boardSize = 0;

        private Cell[] _cells;

        private readonly List<SnakeLink> _snakesList = new List<SnakeLink>();
        //private readonly List<LadderLink> _ladders = new List<LadderLink>();

        public Board(int rows , int cols)
        {
            _rows = rows;
            _cols = cols;
            
            SetBoardSize(rows * cols);

            _cells = new Cell[_boardSize];
        }

        // create setBoardSize method for future validation if needed
        private void SetBoardSize(int boardSize)
        {
            // +1 because all players starts from index 0;
            _boardSize = boardSize + 1 ;
        }

        public Cell[] GetCells()
        {
            return _cells;
        }

        public int GetRowsNumber()
        {
            return _rows;
        }

        public int GetColsNumber()
        {
            return _cols;
        }

        public int GetBoardSize()
        {
            return _boardSize;
        }

        public List<SnakeLink> GetSnakesList()
        {
            return _snakesList;
        }
    }
}
