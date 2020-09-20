using System;

namespace HangmanGame.EventArgsObjects
{
    /// <summary>
    ///     Object for the Won and Lost Event of the Hangman object.
    /// </summary>
    public class HangmanEventArgs : EventArgs
    {
        public HangmanEventArgs(SearchTerm searchTerm, int attemptsUsed, int attempts)
        {
            SearchTerm = searchTerm;
            AttemptsUsed = attemptsUsed;
            Attempts = attempts;
        }

        public SearchTerm SearchTerm { get; }
        public int AttemptsUsed { get; }
        public int Attempts { get; }
    }
}