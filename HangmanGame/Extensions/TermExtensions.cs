namespace HangmanGame.Extensions
{
    public static class TermExtensions
    {  
        /// <summary>
        /// Inserts a Backspace after each character and returns the modified Term.
        /// </summary>
        /// <returns>modified string</returns>
        public static Term InsertBackspaces(this Term term)
        {
            return ((string) term).InsertBackspaces();
        }
    }
}
