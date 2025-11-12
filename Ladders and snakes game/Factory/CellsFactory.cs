using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladders_and_snakes_game.Configuration;
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

        // Initialize Empty Cells
        public void InitEmptyCells(ref Board gameBoard)
        {
            Cell[] cells = gameBoard.GetCells();

            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] == null)
                {
                    cells[i] = new EmptyCell(i, enumCellType.Empty);
                }
            }
        }

        // Initialize Gold Cells chain
        public void InitGoldCells(ref Board gameBoard)
        {
            int max = gameBoard.GetBoardSize() / 2 ;
            int min = 1;

            for (int i = 0; i < GameSettings.MaxGold; i++)
            {
                int currentGoldCellPosition = GetRandomIndexForGoldCell(max, min, ref gameBoard);

                // assign gold cell to the random position in the board
                gameBoard.GetCells()[currentGoldCellPosition] = new GoldCell(currentGoldCellPosition, enumCellType.GoldenCell);
            }
        }

        int GetRandomIndexForGoldCell(int max , int min ,ref Board gameBoard)
        {
            int randomIndex;
            do
            {
                randomIndex = RandomProvider.Instance.Next(min, max);
            }
            while (gameBoard.GetCells()[randomIndex] != null);
            return randomIndex;
        }


        // Initialize Snakes chain
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

        // Initialize Ladders chain

        public void InitLadders(ref Board gameBoard)
        {
            for (int i = 0; i < LaddersNumber; i++)
            {
                int randomLaddersBottomPosition = GetRandomIndexForLadderBottom(ref gameBoard);

                // create ladder bottom cell
                TopOrBottomCell ladderBottom = new TopOrBottomCell(randomLaddersBottomPosition, enumCellType.LadderBottom);

                // assign ladder bottom to the random position in the board
                gameBoard.GetCells()[randomLaddersBottomPosition] = ladderBottom;

                // create ladder top cell 
                int randomLadderTopPosition = GetRandomIndexForLadderTopPosition(ref gameBoard, ladderBottom.GetIndex());
                TopOrBottomCell ladderTop = new TopOrBottomCell(randomLadderTopPosition, enumCellType.LadderTop);

                // assign ladder top to the random position in the board
                gameBoard.GetCells()[randomLadderTopPosition] = ladderTop;

                LadderLink newLadder = new LadderLink(ladderBottom, ladderTop);

                // add the new snake to the snakes list
                gameBoard.GetLadderList().Add(newLadder);
            }
        }

        private int GetRandomIndexForLadderBottom(ref Board gameBoard)
        {
            int cols = gameBoard.GetColsNumber();
            int lastPlayable = gameBoard.GetBoardSize() - 1;

            int ladderBottomMinNumber = 1;
            int ladderBottomMaxNumber = lastPlayable - cols;
            
            int randomIndex;

            // ensure that the random index is not already occupied
            do
            {                                                    // 1        to        90
                randomIndex = RandomProvider.Instance.Next(ladderBottomMinNumber, ladderBottomMaxNumber +1 ); // inclusive upper bound
            }
            while (gameBoard.GetCells()[randomIndex] != null);

            return randomIndex;
        }

        private int GetRandomIndexForLadderTopPosition(ref Board gameBoard, int currentLadderBottomPosition)
        {
            int ladderTopMaxPosition = gameBoard.GetBoardSize()-1;

            int ladderTopMinPosition = CalculateMinLadderPosition(ref gameBoard, currentLadderBottomPosition);

            int randomIndex;

            // TODO What if there is no valid position for the tail ?

            do
            {                                                    
                randomIndex = RandomProvider.Instance.Next(ladderTopMinPosition, ladderTopMaxPosition); // exclusive last cell
            }
            while (gameBoard.GetCells()[randomIndex] != null);

            return randomIndex;
        }
        private int CalculateMinLadderPosition(ref Board gameBoard, int currentLadderBottomPosition)
        {
            // todo why get cols size and not rows size ?

            int cols = gameBoard.GetColsNumber();

            int currentRow = (currentLadderBottomPosition - 1) / cols;        // 0-based current row
            int nextRowStart = (currentRow + 1) * cols + 1;         // first index of next row

            // clamp to last playable just in case
            int lastPlayable = gameBoard.GetBoardSize() - 1;
            if (nextRowStart > lastPlayable) nextRowStart = lastPlayable;

            return nextRowStart;
        }
    }
}
