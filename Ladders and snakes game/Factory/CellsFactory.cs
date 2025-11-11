using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladders_and_snakes_game.Core;
using Ladders_and_snakes_game.Utilities;

namespace Ladders_and_snakes_game.Factory
{
    internal class CellsFactory
    {
        private int SnakesNumber { get; }
        private int LaddersNumber { get; }
        
        public CellsFactory(int snakesNumber , int laddersNumber)
        {
            SnakesNumber = snakesNumber;
            LaddersNumber = laddersNumber;
        }

        public void InitEmptyCells(ref Board gameBoard)
        {
            Cell[] cells = gameBoard.GetCells();

            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] == null)
                {
                    cells[i] = new EmptyCell(i,enumCellType.Empty);
                }
            }
        }

        public void InitSnakes(ref Board gameBoard)
        {
            for (int i = 0; i < SnakesNumber; i++)
            {
                int randomSnakeHeadPosition = GetRandomIndexForSnakeHead(ref gameBoard);

                //create snake head cell
                TopOrBottomCell snakeHead = new TopOrBottomCell(randomSnakeHeadPosition, enumCellType.SnakeHead);

                // assign snake head to the random position in the board
                gameBoard.GetCells()[randomSnakeHeadPosition] = snakeHead;

                // create snake tail cell 
                int randomSnakeTailPosition = GetRandomIndexForTailPosition(ref gameBoard, snakeHead.GetIndex());
                TopOrBottomCell snakeTail = new TopOrBottomCell(randomSnakeTailPosition, enumCellType.SnakeTail);

                // assign snake tail to the random position in the board
                gameBoard.GetCells()[randomSnakeTailPosition] = snakeTail;

                SnakeLink newSnake = new SnakeLink(snakeHead, snakeTail);

                // add the new snake to the snakes list
                gameBoard.GetSnakesList().Add(newSnake);
            }
        }

        private int GetRandomIndexForSnakeHead(ref Board gameBoard)
        {
            // todo CHANGED: avoid first row by using number of COLUMNS, not rows but why? 

            // to avoid placing snake head in first row example: 10*10 board , snake head min number = 11
            int snakeHeadMinNumber = gameBoard.GetColsNumber() + 1;

            // to avoid placing snake head in last cell
            int snakeHeadMaxNumber = gameBoard.GetBoardSize() - 1;

            int randomIndex;

            // ensure that the random index is not already occupied
            do
            {                                                    // 11        to        99
                randomIndex = RandomProvider.Instance.Next(snakeHeadMinNumber, snakeHeadMaxNumber); // exclusive upper bound
            }
            while (gameBoard.GetCells()[randomIndex]!= null);

            return randomIndex;
        }

        private int GetRandomIndexForTailPosition(ref Board gameBoard , int currentSnakeHeadPosition)
        {
            int snakeTailMinPosition = 1;

            int snakeTailMaxPosition = CalculateMaxTailPosition(ref gameBoard , currentSnakeHeadPosition);

            int randomIndex;

            // TODO What if there is no valid position for the tail ?

            do
            {                                                    // 1 to last index in allowed row
                randomIndex = RandomProvider.Instance.Next(snakeTailMinPosition, snakeTailMaxPosition + 1); // to include upper bound
            }
            while (gameBoard.GetCells()[randomIndex] != null);

            return randomIndex;

        }

        private int CalculateMaxTailPosition(ref Board gameBoard,int currentSnakeHeadPosition)
        {
            // todo why get cols size and not rows size ?

            int rowSize = gameBoard.GetColsNumber();            // square board => cols == rows

            int headRow = (currentSnakeHeadPosition - 1) / rowSize + 1;            // 1-based row

            int maxTailIndex = (headRow - 1) * rowSize;             // end of previous row

            return maxTailIndex;
        }

        
    }
}
