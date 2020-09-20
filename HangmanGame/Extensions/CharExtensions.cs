namespace HangmanGame.Extensions
{
    public static class CharExtensions
    {
        /// <summary>
        ///     Returns a lowercase copy of the given char.
        /// </summary>
        public static char ToLower(this char @char)
        {
            return char.ToLower(@char);
        }

        /// <summary>
        ///     Matches 2 chars. Case insensitive
        /// </summary>
        public static bool Match(this char @char, char charToMatch)
        {
            return @char.ToLower() == charToMatch.ToLower();
        }
    }
}