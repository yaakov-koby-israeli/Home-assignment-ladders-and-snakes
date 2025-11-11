using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladders_and_snakes_game.Core;
using Ladders_and_snakes_game.Factory;
using Ladders_and_snakes_game.Players;
using Ladders_and_snakes_game.Configuration;

namespace Ladders_and_snakes_game.Game_Logic
{
    internal class GameManager
    {
        // TODO HOW CAN BE MORE GENERIC ?
        //private readonly int _maxSnakesNumber = 15;
        //private readonly int _maxLaddersNumber = 15;
        private readonly int _playersNumber;

        private readonly List<IPlayer> _playersList = new List<IPlayer>();

        private Board _gameBoard;

        private CellsFactory _cellsFactory;

        private Dice DiceOne { get; } = new Dice();

        private Dice DiceTwo { get; } = new Dice();

        public GameManager(int amountOfPlayers, int rows, int cols, int snakesAmount, int laddersAmount)
        {
            _playersNumber = amountOfPlayers;
            InitBoard(rows,cols);
            InitCells(snakesAmount, laddersAmount);
            InitPlayers();
        }

        public void InitPlayers() 
        {
            for (int i = 0; i < _playersNumber; i++)
            {
                Player newPlayer = new Player();
                _playersList.Add(newPlayer);
            }
        }

        public List<IPlayer> GetPlayersList()
        {
            return _playersList;
        }

        private void InitBoard(int row,int cols)
        {
            _gameBoard = new Board(row, cols);
        }

        private void InitCells(int snakesNumber , int laddersNumber)
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
            return DiceOne.RollTheDice() + DiceTwo.RollTheDice();
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

            // todo delegate and event driven for Game Over method !!!
            // check if player reached or exceeded position 100
            if (GameOver())
            {
                // todo need raise an event to finish the game !!!
            }

            CheckIfSpecialCellType( player);
        }

        private void CheckIfSpecialCellType(IPlayer currentPlayer)
        {
            enumCellType currentCellType = _gameBoard.GetCells()[currentPlayer.Position].GetCellType();
            switch (currentCellType)
            {
                case enumCellType.SnakeHead:
                    FindSnakeTailAndMovePlayerDown( currentPlayer);
                    break;

                case enumCellType.LadderTop:
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
                //currentPlayer.Position = snake.GetTailCell().GetIndex();
                snake.MovePlayerDown(currentPlayer);
            }
        }

        // func checks if any player position reached or exceeded position 100
        private bool GameOver()
        {
            bool isGameOver = false;
            int lastIndex = _gameBoard.GetBoardSize();

            foreach (IPlayer player in _playersList)
            {
                if(player.Position >= lastIndex)
                {
                    isGameOver = true;
                    player.HasWon = true;
                    break;
                }
            }
            return isGameOver;
        }

        public string GetUiToken(int index)
        {
            var p = _playersList.FirstOrDefault(pl => pl.Position == index);
            if (p != null) return $"P{p.Id}";

            var cell = _gameBoard.GetCells()[index];
            if (cell != null)
            {
                switch (cell.GetCellType())
                {
                    case enumCellType.SnakeHead:
                        return "SH";
                    case enumCellType.SnakeTail:
                        return "ST";
                    case enumCellType.LadderTop:
                        return "LT";
                    case enumCellType.LadderBottom:
                        return "LB";
                    case enumCellType.GoldenCell:
                        return "G";
                    default:
                        return index.ToString();
                }
            }

            return index.ToString();
        }
    }
}
