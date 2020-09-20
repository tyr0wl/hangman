using System.Collections;

namespace HangmanGame.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        ///     Returns if the collection is Empty.
        /// </summary>
        public static bool IsEmpty(this ICollection list)
        {
            return list.Count == 0;
        }
    }
}