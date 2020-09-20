using System.Collections.Generic;
using System.IO;

namespace HangmanGame
{
    /// <summary>
    ///     Reads the word list out of a stream and returns it.
    /// </summary>
    public interface IWordListReader
    {
        /// <summary>
        ///     Reads the word list out of a stream and returns it.
        /// </summary>
        List<string> GetWordList(Stream streamToRead);
    }
}