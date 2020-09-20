using System.Collections.Generic;
using System.IO;

namespace HangmanGame
{
    /// <summary>
    /// Reads the word list out of a stream and returns it.
    /// </summary>
    public static class WordListReader
    {

        /// <summary>
        /// Reads the word list out of a stream and returns it.
        /// </summary>
        /// <param name="streamToRead">stream that contains the word list.</param>
        /// <returns>Term list as <see cref="List{String}"/></returns>
        public static List<string> GetWordList(Stream streamToRead)
        {
            var wordList = new List<string>();
            var reader = new StreamReader(streamToRead);

            while (!reader.EndOfStream)
            {
                wordList.Add(reader.ReadLine());
            }

            reader.Close();
            return wordList;
        }

        /// <summary>
        /// Reads the wordList from the given path and returns it.
        /// </summary>
        /// <param name="pathToWordList">path to word list</param>
        /// <returns>Term list as <see cref="List{String}"/></returns>
        public static List<string> GetWordList(string pathToWordList)
        {
            var fileStream = new FileStream(pathToWordList, FileMode.Open, FileAccess.Read);
            return GetWordList(fileStream);
        }
    }
}
