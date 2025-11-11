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

        private void GetParamsForSnakeAndLadders()
        {
            Console.WriteLine("Snakes And Ladders\n");
            Console.Write("Enter number of snakes: ");
            var snakesInput = Console.ReadLine();

            while (!UserInputValidation.SnakesInputValidation(ref snakesInput))
            {
                Console.Clear();
                Console.WriteLine("Snakes And Ladders\n");
                Console.WriteLine($"❌ Invalid input. Please Enter a number between 1 and {Configuration.GameSettings.MaxSnakes}.\n");
                snakesInput = Console.ReadLine();
            }

            Console.Clear();
            Console.WriteLine("Snakes And Ladders\n");
            Console.Write("Enter number of snakes: ");
            var laddersInput = Console.ReadLine();

            while (!UserInputValidation.LaddersInputValidation(ref laddersInput))
            {
                Console.Clear();
                Console.WriteLine("Snakes And Ladders\n");
                Console.WriteLine($"❌ Invalid input. Please Enter a number between 1 and {Configuration.GameSettings.MaxLadders}.\n");
                laddersInput = Console.ReadLine();
            }
            Console.Clear();

            //Convert to int before passing to InitComponents
            int snakes = int.Parse(snakesInput);
            int ladders = int.Parse(laddersInput);
            InitComponents(snakes, ladders);
        }

        private void InitComponents(int snakesAmount , int laddersAmount)
        {

        }

    }
}
