using System.Linq;
using System.Text;

namespace HangmanGame.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Converts a string to a Char.
        /// </summary>
        public static char ToChar(this string stringObject)
        {
            return char.Parse(stringObject);
        }

        /// <summary>
        ///     Replaces a char at the specified index in a string object with the given new char
        ///     and returns the modified string.
        /// </summary>
        /// <returns>modified string</returns>
        public static string Replace(this string stringObject, int index, char newChar)
        {
            var stringBuilder = new StringBuilder(stringObject)
            {
                [index] = newChar
            };
            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Inserts a Backspace after each character and returns the modified string.
        /// </summary>
        /// <returns>modified string</returns>
        public static string InsertBackspaces(this string word)
        {
            var resultString = word.Aggregate(string.Empty, (current, @char) => current + (@char + " "));

            resultString = resultString.Substring(0, resultString.Length - 1);
            return resultString;
        }
    }
}