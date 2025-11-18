using System;
using Ladders_and_snakes_game.Configuration;
using Ladders_and_snakes_game.Game_Logic;
using Ladders_and_snakes_game.Utilities;

namespace Ladders_and_snakes_game.Front
{
    internal sealed class UserInterface
    {
        private GameManager _gameManager = null;
        private bool _isGameOver = false;

        public void StartGame()
        {
            while (!_isGameOver)
            {
                // loop to check if not able to build the board and trying again
                while (true)
                {
                    GetParamsForSnakeAndLadders();

                    try
                    {
                        InitComponents();
                        break;
                    }
                    catch (BoardInitializationException)
                    {
                        Console.Clear();
                        Console.WriteLine("Failed to built the game, please enter data again ): \n");
                    }
                }

                PrintBoard(GameSettings.Rows, GameSettings.Cols);

                SubscribeToEvents();
                
                _gameManager.TurnManager();

                HandleRestartOrExit();
            }
        }
        private void GetParamsForSnakeAndLadders()
        {
            Console.WriteLine("Snakes And Ladders\n"); 
            Console.Write("Enter number of snakes: ");
            var snakesInput = Console.ReadLine();

            while (!UserInputValidation.InputValidation(snakesInput, GameSettings.MaxSnakes))
            {
                Console.Clear();
                Console.WriteLine("Snakes And Ladders\n");
                Console.WriteLine($"Invalid input. Please Enter a Snake number between 1 and {GameSettings.MaxSnakes}.\n");
                snakesInput = Console.ReadLine();
            }

            Console.Clear();
            Console.WriteLine("Snakes And Ladders\n");
            Console.Write("Enter number of Ladders: ");
            var laddersInput = Console.ReadLine();

            while (!UserInputValidation.InputValidation(laddersInput, GameSettings.MaxLadders))
            {
                Console.Clear();
                Console.WriteLine("Snakes And Ladders\n");
                Console.WriteLine($"Invalid input. Please Enter a Ladders number between 1 and {GameSettings.MaxLadders}.\n");
                laddersInput = Console.ReadLine();
            }
            Console.Clear();

            //Convert to int  
            GameSettings.Snakes = int.Parse(snakesInput);
            GameSettings.Ladders = int.Parse(laddersInput);
        }

        private void InitComponents()
        {
            _gameManager = new GameManager();
        }

        private void SubscribeToEvents()
        {
            _gameManager.OnGameOver += OnGameOverHandler;
            _gameManager.OnTurnStarted += OnTurnStartedHandler;
            _gameManager.OnRollDice += OnRollDiceHandler;
            _gameManager.OnTurnFinished += OnTurnFinishedHandler;
        }

        private void UnsubscribeToEvents()
        {
            _gameManager.OnGameOver -= OnGameOverHandler;
            _gameManager.OnTurnStarted -= OnTurnStartedHandler;
            _gameManager.OnRollDice -= OnRollDiceHandler;
            _gameManager.OnTurnFinished -= OnTurnFinishedHandler;
        }

        private void OnRollDiceHandler(int sumOfDice, int currentPlayerId, int prevPos, int currentPlayerPosition)
        {
            Console.WriteLine($"Player {currentPlayerId }\nMove: {prevPos}->{currentPlayerPosition}");
            Console.WriteLine($"Dice: {sumOfDice}");
        }

        private void OnTurnStartedHandler(int playerNumber)
        {
            Console.WriteLine($"\nplayer {playerNumber} Press Space To Make your turn\n");
            WaitForSpaceKey();
        }

        private void WaitForSpaceKey()
        {
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(intercept: true).Key; // intercept = true hides the key from console
                if (key != ConsoleKey.Spacebar)
                {
                    Console.WriteLine("Please press SPACE to roll the dice.");
                }
            }
            while (key != ConsoleKey.Spacebar);
        }

        private void OnGameOverHandler(int id)
        {
            Console.WriteLine("Game Over!");
            Console.WriteLine($"Congrats ! player {id} Won ! \n");
            Console.WriteLine("Press any other key to continue.");
            Console.ReadKey(intercept: true);
        }

        private void HandleRestartOrExit()
        {
            Console.Clear();
            Console.WriteLine("Would you like to play again?");
            Console.WriteLine("Press Y to restart.");
            Console.WriteLine("Press any other key to exit.");

            // Read one key without showing it
            ConsoleKey key = Console.ReadKey(intercept: true).Key;

            if (key == ConsoleKey.Y)
            {
                Console.Clear();
                UnsubscribeToEvents();   // reset events before starting again
                // game will restart on next loop
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Thanks for playing!");

                // exit the entire game
                _isGameOver = true;      
            }
        }

        // func wraps PrintBoard function in order to activate it from delegate (delegate inside GameManager Class)
        private void OnTurnFinishedHandler()
        {
            PrintBoard(GameSettings.Rows, GameSettings.Cols);
        }

        // Next Three functions for printing the board in console
        private void PrintBoard(int rows , int cols)
        {
            Console.Clear();
            int cellWidth = 6; // adjust for wider cells

            // top border
            DrawBorder(cols, cellWidth);

            // We print from visual TOP row to BOTTOM row,
            // but compute numbers as if indexing rows from the BOTTOM.
            for (int k = 0; k < rows; k++)
            {
                int bottomRowIndex = rows - 1 - k;           // 0 = bottom row
                int rowStart = bottomRowIndex * cols + 1;     // first number in that row

                bool leftToRight = (bottomRowIndex % 2 == 0); // even rows from bottom go L->R

                if (leftToRight)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        int value = rowStart + c;
                        Console.Write("|" + Center(_gameManager.GetUiToken(value), cellWidth));
                    }
                }
                else
                {
                    for (int c = cols - 1; c >= 0; c--)
                    {
                        int value = rowStart + c;
                        Console.Write("|" + Center(_gameManager.GetUiToken(value), cellWidth));
                    }
                }
                Console.WriteLine("|");

                // border after the row
                DrawBorder(cols, cellWidth);
            }
        }

        private void DrawBorder(int cols, int cellWidth)
        {
            for (int c = 0; c < cols; c++)
                Console.Write("+" + new string('-', cellWidth));
            Console.WriteLine("+");
        }

        private string Center(string text, int width)
        {
            int padding = width - text.Length;
            int padLeft = text.Length + padding / 2;
            return text.PadLeft(padLeft).PadRight(width);
        }
    }
}
