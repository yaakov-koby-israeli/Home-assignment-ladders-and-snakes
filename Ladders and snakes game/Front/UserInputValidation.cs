namespace Ladders_and_snakes_game.Front
{
    internal sealed class UserInputValidation
    {
        public static bool InputValidation(string userInput , int maxValue)
        {
            bool isValid = true;

            if (!int.TryParse(userInput, out int userInputAsNumber))
            {
                isValid = false;
            }

            if (userInputAsNumber < 1 || userInputAsNumber > maxValue)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
