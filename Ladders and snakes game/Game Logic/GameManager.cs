using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladders_and_snakes_game.Core;
using Ladders_and_snakes_game.Factory;
using Ladders_and_snakes_game.Players;

namespace Ladders_and_snakes_game.Game_Logic
{
    internal class GameManager
    {
        // TODO HOW CAN BE MORE GENERIC ?
        private readonly int _maxSnakesNumber = 12;
        private readonly int _maxLaddersNumber = 12;
        private readonly int _playersNumber = 2;

        private readonly List<IPlayer> _playersList = new List<IPlayer>();

        private Board _gameBoard;

        private CellsFactory _cellsFactory;

        private Dice DiceOne { get; } = new Dice();

        private Dice DiceTow { get; } = new Dice();

        public GameManager() { }

        public void InitPlayers() 
        {
            for (int i = 0; i < _playersNumber; i++)
            {
                Player newPlayer = new Player();
                _playersList.Add(newPlayer);
            }
        }

        public void InitBoard(int row,int cols)
        {
            _gameBoard = new Board(row, cols);
        }

        public void InitCells(int snakesNumber , int laddersNumber)
        {
            _cellsFactory = new CellsFactory(snakesNumber, laddersNumber);

            // TODO init gold cells here !!!

            _cellsFactory.InitSnakes(ref _gameBoard);

            // TODO init ladders here !!!

            _cellsFactory.InitEmptyCells(ref _gameBoard);
        }

        // return sum of dices
        public int RollDices()
        {
            return DiceOne.RollTheDice() + DiceTow.RollTheDice();
        }

        public void TurnManager()
        {
            int index = 0;

            while (!GameOver())
            {
                var currentPlayer = _playersList[index];
                PlayerOnTurn( currentPlayer);

                // move to next player
                index = (index + 1) % _playersList.Count; // when it reaches end, go back to 0
            }
        }

        private void PlayerOnTurn(IPlayer player)
        {
            int diceResult = RollDices();
            player.MovePlayer(diceResult);

            CheckIfSpecialCellType( player);
        }

        private void CheckIfSpecialCellType(IPlayer currentPlayer)
        {
            enumCellType currentCellType = _gameBoard.GetCells()[currentPlayer.Position].GetCellType();
            switch (currentCellType)
            {
                case enumCellType.SnakeHead:
                    // TODO handle snake head logic
                    FindSnakeTailAndMovePlayerDown( currentPlayer);
                    break;
                case enumCellType.LadderHead:
                    // TODO handle ladder head logic
                    break;

            }
        }

        private void FindSnakeTailAndMovePlayerDown(IPlayer currentPlayer)
        {
            // TODO LEARN THIS CODE !!! LINQ !!!
            // Find the snake whose HEAD is at the player's current index

            var snake = _gameBoard.GetSnakesList()
                .FirstOrDefault(s => s.GetHeadCell() == _gameBoard.GetCells()[currentPlayer.Position]);

            if (snake != null)
            {
                currentPlayer.Position = snake.GetTailCell().GetIndex();
            }
        }

        // check if any player position reached or exceeded position 100
        private bool GameOver()
        {
            bool isGameOver = false;

            foreach (IPlayer player in _playersList)
            {
                if(player.Position>=100)
                {
                    isGameOver = true;
                    break;
                }
            }
            return isGameOver;
        }
    }
}
