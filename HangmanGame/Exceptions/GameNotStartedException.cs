using System;

namespace HangmanGame.Exceptions
{
    public class GameNotStartedException : Exception
    {
        /// <inheritdoc />
        public GameNotStartedException()
        {
        }

        /// <inheritdoc />
        public GameNotStartedException(string message) : base(message)
        {
        }
    }
}