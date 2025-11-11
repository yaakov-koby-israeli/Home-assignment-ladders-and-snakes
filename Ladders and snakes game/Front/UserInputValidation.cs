using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Ladders_and_snakes_game.Front
{
    internal class UserInputValidation
    {
        public static bool SnakesInputValidation(ref string snakesNumber)
        {
            bool isValid = true;

            if (!int.TryParse(snakesNumber, out int snakes))
            {
                isValid = false;
            }
            if (snakes < 1 || snakes > Configuration.GameSettings.MaxSnakes)
            {
                isValid = false;
            }

            return isValid;
        }

        public static bool LaddersInputValidation(ref string laddersNumber)
        {
            bool isValid = true;

            if (!int.TryParse(laddersNumber, out int ladders))
            {
                isValid = false;
            }
            if (ladders < 1 || ladders > Configuration.GameSettings.MaxLadders)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
