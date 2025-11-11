using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladders_and_snakes_game.Configuration;
using Ladders_and_snakes_game.Game_Logic;
using Ladders_and_snakes_game.Configuration;

namespace Ladders_and_snakes_game.Front
{
    internal class UserInterface
    {
        // TODO MAYBE USE DI OR singleton
        private GameManager _gameManager = null;
        
        public void StartGame()
        {
            
        }

        public void GetParamsForSnakeAndLadders()
        {
            Console.WriteLine("Snakes And Ladders\n");
            Console.Write("Enter number of snakes: ");
            var snakes = Console.ReadLine();

            while (!UserInputValidation.SnakesInputValidation(snakes))
            {
                Console.Clear();
                Console.WriteLine("Snakes And Ladders\n");
                Console.WriteLine($"❌ Invalid input. Please Enter a number between 1 and {Configuration.GameSettings.MaxSnakes}.\n");
                snakes = Console.ReadLine();
            }

            Console.Clear();
            Console.WriteLine("Snakes And Ladders\n");
            Console.Write("Enter number of snakes: ");
            var ladders = Console.ReadLine();

            while (!UserInputValidation.LaddersInputValidation(ladders))
            {
                Console.Clear();
                Console.WriteLine("Snakes And Ladders\n");
                Console.WriteLine($"❌ Invalid input. Please Enter a number between 1 and {Configuration.GameSettings.MaxLadders}.\n");
                snakes = Console.ReadLine();
            }
        }
    }
}
