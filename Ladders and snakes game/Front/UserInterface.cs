using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladders_and_snakes_game.Configuration;
using Ladders_and_snakes_game.Game_Logic;
using Ladders_and_snakes_game.Configuration;
using Ladders_and_snakes_game.Players;

namespace Ladders_and_snakes_game.Front
{
    internal class UserInterface
    {
        // TODO MAYBE USE DI OR singleton
        private GameManager _gameManager = null;
        
        public void StartGame()
        {
            GetParamsForSnakeAndLadders();
            PrintBoard(GameSettings.Rows, GameSettings.Cols);
        }

        private void GetParamsForSnakeAndLadders()
        {
            Console.WriteLine("Snakes And Ladders\n");
            Console.Write("Enter number of snakes: ");
            var snakesInput = Console.ReadLine();

            while (!UserInputValidation.SnakesInputValidation(ref snakesInput))
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

            while (!UserInputValidation.LaddersInputValidation(ref laddersInput))
            {
                Console.Clear();
                Console.WriteLine("Snakes And Ladders\n");
                Console.WriteLine($"Invalid input. Please Enter a Ladders number between 1 and {GameSettings.MaxLadders}.\n");
                laddersInput = Console.ReadLine();
            }
            Console.Clear();

            //Convert to int before passing to InitComponents
            GameSettings.Snakes = int.Parse(snakesInput);
            GameSettings.Ladders = int.Parse(laddersInput);

            InitComponents();
        }

        private void InitComponents()
        {
            _gameManager = new GameManager(GameSettings.Players,
                GameSettings.Rows, GameSettings.Cols, GameSettings.Snakes, GameSettings.Ladders);
        }

        private void PrintBoard(int rows , int cols)
        {
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
                        //Console.Write("|" + Center(value.ToString(), cellWidth));
                        Console.Write("|" + Center(_gameManager.GetUiToken(value), cellWidth));

                    }
                }
                else
                {
                    for (int c = cols - 1; c >= 0; c--)
                    {
                        int value = rowStart + c;
                        //Console.Write("|" + Center(value.ToString(), cellWidth));
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
