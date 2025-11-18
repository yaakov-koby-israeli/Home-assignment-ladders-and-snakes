using System;

namespace Ladders_and_snakes_game.Utilities
{
    internal sealed class BoardInitializationException : Exception
    {
        public BoardInitializationException(string message)
            : base(message) { }

        public BoardInitializationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
