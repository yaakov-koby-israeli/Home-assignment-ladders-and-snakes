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
        private int _diceRes = 0;
        private bool isGameOver = false;

        private readonly List<IPlayer> _playersList = new List<IPlayer>();

        private Board _gameBoard;

        private CellsFactory _cellsFactory;
        private Dice DiceOne { get; } = new Dice();
        private Dice DiceTwo { get; } = new Dice();

        // Events and delegates
        public delegate void TurnStartedHandler(int player);
        public event TurnStartedHandler OnTurnStarted;

        public delegate void RollDiceHandler(int sumOfDice);
        public event RollDiceHandler OnRollDice;

        public delegate void OnGameOverHandler(int playerId);
        public event OnGameOverHandler OnGameOver;

        public event Action OnTurnFinished;
        

        public GameManager(int amountOfPlayers, int rows, int cols, int snakesAmount, int laddersAmount)
        {
            _playersNumber = amountOfPlayers;

            InitPlayers();
            InitBoard(rows,cols);
            InitCells(snakesAmount, laddersAmount);
        }

        // Init Methods
        public void InitPlayers() 
        {
            for (int i = 0; i < _playersNumber; i++)
            {
                Player newPlayer = new Player();
                _playersList.Add(newPlayer);
            }
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

            _cellsFactory.InitLadders(ref _gameBoard);

            _cellsFactory.InitEmptyCells(ref _gameBoard); 
        }
        
        // return sum of dices
        public int RollDices()
        {
            _diceRes = DiceOne.RollTheDice() + DiceTwo.RollTheDice();
            return _diceRes;
        }

        // Game Sequence Chain
        public void TurnManager()
        {
            int index = 0;

            while (! isGameOver)
            {
                var currentPlayer = _playersList[index];
                OnTurnStarted?.Invoke(currentPlayer.Id);

                PlayerOnTurn(currentPlayer);

                OnTurnFinished?.Invoke(); // printing updated board
                OnRollDice?.Invoke(_diceRes); // print dice result

                // move to next player
                index = (index + 1) % _playersList.Count; // when it reaches end, go back to 0
            }
        }

        private void PlayerOnTurn(IPlayer player)
        {
            _diceRes = RollDices();

            player.MovePlayer(_diceRes);

            CheckIfGameOver();

            CheckIfSpecialCellType(player);
        }

        private void CheckIfSpecialCellType(IPlayer currentPlayer)
        {
            enumCellType currentCellType = _gameBoard.GetCells()[currentPlayer.Position].GetCellType();
            switch (currentCellType)
            {
                case enumCellType.SnakeHead:
                    FindSnakeTailAndMovePlayerDown(currentPlayer);
                    break;

                case enumCellType.LadderBottom:
                    FindLadderTopAndMovePlayerUp(currentPlayer);
                    break;

                case enumCellType.GoldenCell:
                    // TODO implement golden cell effect !!!
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

        private void FindLadderTopAndMovePlayerUp(IPlayer currentPlayer)
        {
            // TODO LEARN THIS CODE !!! LINQ !!!
            // Find the ladder whose BOTTOM is at the player's current index
            var ladder = _gameBoard.GetLadderList()
                .FirstOrDefault(l => l.GetTopCell() == _gameBoard.GetCells()[currentPlayer.Position]);

            if (ladder != null)
            {
                //currentPlayer.Position = ladder.GetBottomCell().GetIndex();
                ladder.MovePlayerUp(currentPlayer);
            }
        }

        // func checks if any player position reached or exceeded Max position 
        private bool CheckIfGameOver()
        {
            bool isGameOver = false;
            int lastIndex = _gameBoard.GetBoardSize()-1; // we add one to real size because index starts from 0

            foreach (IPlayer player in _playersList)
            {
                if(player.Position >= lastIndex)
                {
                    isGameOver = true;
                    player.HasWon = true;
                    OnGameOver?.Invoke(player.Id);
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
