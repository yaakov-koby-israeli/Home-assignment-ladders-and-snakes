using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladders_and_snakes_game.Configuration;
using Ladders_and_snakes_game.Core;
using Ladders_and_snakes_game.Factory;
using Ladders_and_snakes_game.Players;
using Ladders_and_snakes_game.Utilities;

namespace Ladders_and_snakes_game.Game_Logic
{
    internal sealed class GameManager
    {
        private readonly int _playersNumber;
        private int _leadPlayerId = 1;
        private int _diceRes = 0;
        
        private readonly List<IPlayer> _playersList = new List<IPlayer>();

        private Board _gameBoard;

        private CellsFactory _cellsFactory;
        private Dice DiceOne { get; } = new Dice();
        private Dice DiceTwo { get; } = new Dice();

        // Events and delegates
        public delegate void TurnStartedHandler(int player);
        public event TurnStartedHandler OnTurnStarted;

        public delegate void RollDiceHandler(int sumOfDice , int currentPlayerId , int prevPos, int currentPlayerPosition);// _diceRes , currentPlayer.Id , prevPos , currentPlayer.Position
        public event RollDiceHandler OnRollDice;

        public delegate void OnGameOverHandler(int playerId);
        public event OnGameOverHandler OnGameOver;

        public event Action OnTurnFinished;

        public GameManager()
        {
            _playersNumber = GameSettings.Players;

            //  wrapping in loop and raise exception from InitCells. If failed after N attempts
            // rethrow the exception so it will be caught and printed by the UI
            WrapInitBoardAndInitCellInTryCatch();

            InitPlayers();
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

        private void WrapInitBoardAndInitCellInTryCatch()
        {
            int attempts = 0;
            while (true)
            {
                try
                {
                    InitBoard(GameSettings.Rows, GameSettings.Cols);
                    InitCells(GameSettings.Snakes, GameSettings.Ladders);
                    break;
                }
                catch (BoardInitializationException)
                {
                    attempts++;

                    if (attempts > GameSettings.MaxAttemptsToInitBoard)
                    {
                        throw;
                    }
                }
            }
        }

        private void InitBoard(int row,int cols)
        {
            _gameBoard = new Board(row, cols);
            _gameBoard.GetSnakesList().Clear();
            _gameBoard.GetLadderList().Clear();
        }

        private void InitCells(int snakesNumber , int laddersNumber)
        {
            _cellsFactory = new CellsFactory(snakesNumber, laddersNumber);

            _cellsFactory.InitGoldCells(_gameBoard);

            _cellsFactory.InitSnakes(_gameBoard);

            _cellsFactory.InitLadders(_gameBoard);

            _cellsFactory.InitEmptyCells(_gameBoard); 
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

            while (true)
            {
                var currentPlayer = _playersList[index];
                OnTurnStarted?.Invoke(currentPlayer.Id);

                // save to send to ui
                int prevPos = currentPlayer.Position;

                _diceRes = RollDices();
                
                currentPlayer.MovePlayer(_diceRes);

                CheckIfSpecialCellType(currentPlayer);

                UpdateLeaderId();

                OnTurnFinished?.Invoke(); // printing updated board in ui
                OnRollDice?.Invoke(_diceRes , currentPlayer.Id , prevPos , currentPlayer.Position); // printing dice result in ui
                
                if (IsGameOver())
                {
                    break;
                }

                // move to next player
                index = (index + 1) % _playersList.Count; // when it reaches end, go back to 0
            }
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
                    if (currentPlayer.Id != _leadPlayerId)
                    {
                        ActivateGoldCellSwitch(currentPlayer);
                    }
                    break;
            }
        }

        private void FindSnakeTailAndMovePlayerDown(IPlayer currentPlayer)
        {
            // Find the snake whose HEAD is at the player's current index

            var snake = _gameBoard.GetSnakesList()
                .FirstOrDefault(s => s.GetHeadCell() == _gameBoard.GetCells()[currentPlayer.Position]);

            if (snake != null)
            {
                snake.MovePlayerDown(currentPlayer);
            }
        }

        private void FindLadderTopAndMovePlayerUp(IPlayer currentPlayer)
        {
            // Find the ladder whose BOTTOM is at the player's current index
            var ladder = _gameBoard.GetLadderList()
                .FirstOrDefault(l => l.GetBottomCell() == _gameBoard.GetCells()[currentPlayer.Position]);

            if (ladder != null)
            {
                ladder.MovePlayerUp(currentPlayer);
            }
        }

        private void ActivateGoldCellSwitch(IPlayer currentPlayer)
        {
            IPlayer currentLeaderPlayer = _playersList.FirstOrDefault(p => p.Id == _leadPlayerId);
            Cell currentGoldCell = _gameBoard.GetCells()[currentPlayer.Position];

            if (currentGoldCell is GoldCell goldCell)
            {
                goldCell.OnLand(currentLeaderPlayer, currentPlayer);
            }
        }

        private void UpdateLeaderId()
        {
            IPlayer leader = _playersList
                .OrderByDescending(player => player.Position)
                .FirstOrDefault();

            if (leader != null)
                _leadPlayerId = leader.Id;
        }

        // func checks if any player position reached or exceeded Max position 
        private bool IsGameOver()
        {
            int lastIndex = _gameBoard.GetBoardSize() - 1; // added one to real size because index starts from 0
            
            foreach (IPlayer player in _playersList)
            {
                if(player.Position >= lastIndex)
                {
                    player.HasWon = true;
                    OnGameOver?.Invoke(player.Id);
                    return true;
                }
            }
            return false;
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
