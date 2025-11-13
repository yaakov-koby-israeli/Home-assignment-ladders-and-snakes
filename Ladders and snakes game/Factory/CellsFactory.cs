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
    internal sealed class CellsFactory
    {
        private int SnakesNumber { get; }
        private int LaddersNumber { get; }
        
        public CellsFactory(int snakesNumber , int laddersNumber)
        {
            SnakesNumber = snakesNumber;
            LaddersNumber = laddersNumber;
        }

        // Initialize Empty Cells
        public void InitEmptyCells(Board gameBoard)
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
        public void InitGoldCells(Board gameBoard)
        {
            int max = gameBoard.GetBoardSize() - 1 ;
            int min = 1;

            for (int i = 0; i < GameSettings.MaxGold; i++)
            {
                int currentGoldCellPosition = GetRandomIndexForGoldCell(max, min,  gameBoard);

                // assign gold cell to the random position in the board
                gameBoard.GetCells()[currentGoldCellPosition] = new GoldCell(currentGoldCellPosition, enumCellType.GoldenCell);
            }
        }

        int GetRandomIndexForGoldCell(int max , int min , Board gameBoard)
        {
            int randomIndex;
            do
            {
                randomIndex = RandomProvider.Instance.Next(min, max);// excluded max: in our case from 1 to 99
            }
            while (gameBoard.GetCells()[randomIndex] != null);
            return randomIndex;
        }


        // Initialize Snakes chain
        public void InitSnakes(Board gameBoard)
        {
            for (int i = 0; i < SnakesNumber; i++)
            {
                int randomSnakeHeadPosition = GetRandomIndexForSnakeHead( gameBoard);

                //create snake head cell    
                TopOrBottomCell snakeHead = new TopOrBottomCell(randomSnakeHeadPosition, enumCellType.SnakeHead);

                // assign snake head to the random position in the board
                gameBoard.GetCells()[randomSnakeHeadPosition] = snakeHead;

                // create snake tail cell 
                int randomSnakeTailPosition = GetRandomIndexForTailPosition( gameBoard, snakeHead.GetIndex());
                TopOrBottomCell snakeTail = new TopOrBottomCell(randomSnakeTailPosition, enumCellType.SnakeTail);

                // assign snake tail to the random position in the board
                gameBoard.GetCells()[randomSnakeTailPosition] = snakeTail;

                SnakeLink newSnake = new SnakeLink(snakeHead, snakeTail);

                // add the new snake to the snakes list
                gameBoard.GetSnakesList().Add(newSnake);
            }
        }

        private int GetRandomIndexForSnakeHead(Board gameBoard)
        {
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

        private int GetRandomIndexForTailPosition(Board gameBoard , int currentSnakeHeadPosition)
        {
            int snakeTailMinPosition = 1;

            int snakeTailMaxPosition = CalculateMaxTailPosition( gameBoard , currentSnakeHeadPosition);

            int randomIndex;

            // if there is no place to put the tail
            int maxAttempts = (snakeTailMaxPosition - snakeTailMinPosition + 1) * 2;
            int attempts = 0;

            do
            {                                                    // 1 to last index in allowed row
                randomIndex = RandomProvider.Instance.Next(snakeTailMinPosition, snakeTailMaxPosition + 1); // to include upper bound

                if (attempts++ >= maxAttempts)
                {
                    throw new BoardInitializationException($"No valid position found for snake tail (head at {currentSnakeHeadPosition}).");
                }
            }
            while (gameBoard.GetCells()[randomIndex] != null);

            return randomIndex;

        }

        private int CalculateMaxTailPosition(Board gameBoard,int currentSnakeHeadPosition)
        {
            int rowSize = gameBoard.GetColsNumber();            // square board => cols == rows

            int headRow = (currentSnakeHeadPosition - 1) / rowSize + 1;   // calc based row

            int maxTailIndex = (headRow - 1) * rowSize;             // end of previous row

            return maxTailIndex;
        }

        // Initialize Ladders chain

        public void InitLadders(Board gameBoard)
        {
            for (int i = 0; i < LaddersNumber; i++)
            {
                int randomLaddersBottomPosition = GetRandomIndexForLadderBottom(gameBoard);

                // create ladder bottom cell
                TopOrBottomCell ladderBottom = new TopOrBottomCell(randomLaddersBottomPosition, enumCellType.LadderBottom);

                // assign ladder bottom to the random position in the board
                gameBoard.GetCells()[randomLaddersBottomPosition] = ladderBottom;

                // create ladder top cell 
                int randomLadderTopPosition = GetRandomIndexForLadderTopPosition(gameBoard, ladderBottom.GetIndex());
                TopOrBottomCell ladderTop = new TopOrBottomCell(randomLadderTopPosition, enumCellType.LadderTop);

                // assign ladder top to the random position in the board
                gameBoard.GetCells()[randomLadderTopPosition] = ladderTop;

                LadderLink newLadder = new LadderLink(ladderBottom, ladderTop);
                    
                // add the new snake to the snakes list
                gameBoard.GetLadderList().Add(newLadder);
            }
        }

        private int GetRandomIndexForLadderBottom(Board gameBoard)
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

        private int GetRandomIndexForLadderTopPosition(Board gameBoard, int currentLadderBottomPosition)
        {
            int ladderTopMaxPosition = gameBoard.GetBoardSize()-1;

            int ladderTopMinPosition = CalculateMinLadderPosition(gameBoard, currentLadderBottomPosition);

            int randomIndex;

            // if there is no place to put the top
            int maxAttempts = (ladderTopMaxPosition - ladderTopMinPosition + 1) * 2;
            int attempts = 0;

            do
            {                                                    
                randomIndex = RandomProvider.Instance.Next(ladderTopMinPosition, ladderTopMaxPosition); // exclusive last cell
                if (attempts++ >= maxAttempts)
                {
                    throw new BoardInitializationException($"No valid position found for ladder top (bottom at {currentLadderBottomPosition}).");
                }
            }
            while (gameBoard.GetCells()[randomIndex] != null);

            return randomIndex;
        }
        private int CalculateMinLadderPosition(Board gameBoard, int currentLadderBottomPosition)
        {
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
