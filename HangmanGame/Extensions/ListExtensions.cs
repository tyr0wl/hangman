using System;
using System.Collections.Generic;
using System.IO;
using HangmanGame.Resources;

namespace HangmanGame.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        ///     Returns a random element of a List
        /// </summary>
        /// <typeparam name="T">Type of the objects stored in the List</typeparam>
        /// <param name="list">list to select from</param>
        /// <param name="random">random object that configures the algorithms of the selection.</param>
        /// <exception cref="ArgumentNullException">If one of the provided parameters is null</exception>
        /// <exception cref="InvalidDataException">
        ///     If <paramref name="list"/> is empty. A Random item can not be
        ///     selected from an empty list.
        /// </exception>
        public static T RandomItem<T>(this IList<T> list, Random random)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (list.Count == 0)
            {
                throw new InvalidDataException(ExceptionMessages.CanNotSelectRandomItem);
            }

            var index = random.Next(list.Count - 1);
            return list[index];
        }

        /// <summary>
        ///     Returns a random element of a List
        /// </summary>
        /// <typeparam name="T">Type of the objects stored in the List</typeparam>
        /// <param name="list">list to select from</param>
        /// <exception cref="ArgumentNullException">If one of the provided parameters is null</exception>
        /// <exception cref="InvalidDataException">
        ///     If The List is empty. A Random item can not be
        ///     selected from an empty list.
        /// </exception>
        public static T RandomItem<T>(this List<T> list)
        {
            return RandomItem(list, new Random());
        }
    }
}